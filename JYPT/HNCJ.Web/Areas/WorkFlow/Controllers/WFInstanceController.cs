using HNCJ.IBLL;
using HNCJ.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Activities;
using HNCJ.WorkFlow;
using HNCJ.Web.Controllers;
namespace HNCJ.Web.Areas.WorkFlow.Controllers
{
    public class WFInstanceController : AdminBaseController
    {
        //
        // GET: /WFInstance/
        public IWFInstanceService WFInstanceService { get; set; }
        public IWFTempService WFTempService { get; set; }
        public IUserInfoService UserInfoService { get; set; }
        public IWFStepService WFStepService { get; set; }
        public ActionResult Index()
        {
            return View();
        }
        #region 发起流程
        public ActionResult Add(int id)
        {
            ViewBag.Temp = WFTempService.GetEntity(u => u.ID == id).FirstOrDefault();
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(WFInstance instance,int flowTo) {
            
            //在工作流实例表中增加一条数据
            instance.DelFlag = true;
            instance.StartTime = DateTime.Now;//发起时间
            instance.StartBy = LoginUser.ID;//发起人
            instance.Level = 0;//紧急程度
            instance.Status = (short)HNCJ.Model.Enum.WFInstanceEnum.Proecessing;//流程状态
            instance.WFInstanceId = Guid.Empty;//工作流ID
            WFInstanceService.Add(instance);
            string type=WFTempService.GetEntity(u => u.ID == instance.WFTempID).FirstOrDefault().ActivityType;
            
            //第三点：在步骤表里面添加两条步骤。一个当前已经处理完成步骤。
            WFStep startStep = new WFStep();
            startStep.WFInstanceID = instance.ID;
            startStep.SubTime = DateTime.Now;
            startStep.StepName = "提交审批表单";
            startStep.IsEndStep = false;
            startStep.IsStartStep = true;
            startStep.ParentStepId = 0;
            startStep.ProcessBy = 0;
            startStep.ProcessComment = "提交申请";
            startStep.ProcessResult = "通过";
            startStep.ProcessTime = DateTime.Now;
            startStep.StepStatus = (short)HNCJ.Model.Enum.WFStepEnum.Proecessed;
            WFStepService.Add(startStep);
            //第二个步骤：下一步谁审批的步骤
            WFStep nextStep = new WFStep();
            nextStep.WFInstanceID = instance.ID;
            nextStep.SubTime = DateTime.Now;
            nextStep.StepName = "";
            nextStep.IsEndStep = false;
            nextStep.IsStartStep = false;
            nextStep.ProcessBy = flowTo;
            nextStep.ProcessComment = string.Empty;
            nextStep.ProcessResult = "";
            nextStep.ParentStepId = startStep.ID;
            nextStep.StepStatus = (short)HNCJ.Model.Enum.WFStepEnum.UnProecess;
            WFStepService.Add(nextStep);
           
            //第二点：启动工作流
            var wfApp = WorkflowApplicationHelper.CreateWorkflowApp(Common.WorkFlow.GetActivities(type), null);
            instance.WFInstanceId = wfApp.Id;
            WFInstanceService.Update(instance);
            return Json(new { status=1,msg="成功"});
        }
        #endregion

        #region 我的审批流程
        public ActionResult ShowMyCheck()
        {
            ViewData.Model = WFInstanceService.GetEntity(u => u.StartBy == LoginUser.ID).ToList();
            return View();
        }
        //显示待审批的流程
        public ActionResult ShowMyUnChecked()  
        {
            var runEnum = (short)HNCJ.Model.Enum.WFStepEnum.UnProecess;
            var InstanceEnum = (short)HNCJ.Model.Enum.WFInstanceEnum.Proecessing;
            //拿到当前用户未处理的步骤
            var steps = WFStepService.GetEntity(u => u.StepStatus == runEnum && u.ProcessBy == LoginUser.ID).ToList();
            //通过我没有处理的步骤拿到流程的实例
            var instanceIds = (from u in steps  select u.WFInstanceID).Distinct().ToList();

            ViewData.Model = WFInstanceService.GetEntity(s => instanceIds.Contains(s.ID) && s.Status == InstanceEnum).ToList();
            return View();
        }
        //显示已审批的流程
        public ActionResult ShowMyUnCheck()
        {
            
            var InstanceEnum = (short)HNCJ.Model.Enum.WFInstanceEnum.Proecessed;
            //拿到当前用户已处理的步骤
            var steps = WFStepService.GetEntity(u =>u.ProcessBy == LoginUser.ID).ToList();
            //通过我没有处理的步骤拿到流程的实例
            var instanceIds = (from u in steps
                               select u.WFInstanceID).Distinct().ToList();

            ViewData.Model = WFInstanceService.GetEntity(s => instanceIds.Contains(s.ID) && s.Status == InstanceEnum).ToList();
            return View();
        } 
        #endregion
        #region 详细
        public ActionResult Details(int id) {
            var model = WFInstanceService.GetEntity(u => u.ID == id).FirstOrDefault();
            ViewData.Model=model;
            return View();
        }
        #endregion

        #region 审批
        public ActionResult ShowCheck(int id) {
            ViewData.Model = WFInstanceService.GetEntity(u=>u.ID==id).FirstOrDefault();
            return View();
        }
        public ActionResult DoCheck(int ID, bool isPass, string Comment, string ProcessResult, int flowTo)
        {
            //更新当前步骤
            var Instance = WFInstanceService.GetEntity(u => u.ID == ID).FirstOrDefault();
            var step = Instance.WFStep.Where(u => u.StepStatus == 2 && u.ProcessResult == "").FirstOrDefault();
            step.ProcessResult = ProcessResult;
            step.StepStatus = (short)HNCJ.Model.Enum.WFStepEnum.Proecessed;
            step.ProcessTime = DateTime.Now;
            step.ProcessComment = Comment;
            WFStepService.Update(step);

            //初始化下一个步骤
            WFStep nextStep = new WFStep();
            nextStep.IsEndStep = false;
            nextStep.IsStartStep = false;
            nextStep.ProcessBy = isPass ? flowTo : 0;//下一个步骤处理人
            nextStep.SubTime = DateTime.Now;
            nextStep.ProcessResult = string.Empty;
            nextStep.StepStatus = (short)HNCJ.Model.Enum.WFStepEnum.UnProecess;
            nextStep.StepName = string.Empty;
            nextStep.ParentStepId = step.ID;
            nextStep.WFInstanceID = step.WFInstanceID;
            nextStep.ProcessComment = string.Empty;
            WFStepService.Add(nextStep);

            //让书签继续往下执行
            var Value = isPass ? 1 : 0;
            HNCJ.WorkFlow.WorkflowApplicationHelper.ResumeBookMark(Common.WorkFlow.GetActivities(step.WFInstance.WFTemp.ActivityType), step.WFInstance.WFInstanceId, step.StepName, isPass);
            return Json(new { status = 1, errorMsg = "操作成功！！" });
        }
        #endregion
    }
}
