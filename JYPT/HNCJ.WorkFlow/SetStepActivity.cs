using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using Spring.Context;
using Spring.Context.Support;
using HNCJ.IBLL;


namespace HNCJ.WorkFlow
{

    public sealed class SetStepActivity : CodeActivity
    {
        // 定义一个字符串类型的活动输入参数
        public InArgument<string> StepName { get; set; }
        public InArgument<bool> IsEnd { get; set; }

        // 如果活动返回值，则从 CodeActivity<TResult>
        // 派生并从 Execute 方法返回该值。
        protected override void Execute(CodeActivityContext context)
        {
             //获取 Text 输入参数的运行时值
            string text = context.GetValue(this.StepName);
            bool end = context.GetValue(this.IsEnd);
            Guid insId = context.WorkflowInstanceId;
            IApplicationContext ctx = ContextRegistry.GetContext();
            IWFInstanceService WFInstanceService = ctx.GetObject("WFInstanceService") as IWFInstanceService;
            var WFStepService = ctx.GetObject("WFStepService") as IWFStepService;
            var inst = WFInstanceService.GetEntity(u=>u.WFInstanceId==insId).FirstOrDefault();
            if (inst == null) {
                Common.LogHelper.WriteLog("inst is null");
            }
            var stepStatus = (short)HNCJ.Model.Enum.WFStepEnum.UnProecess;
            var step = inst.WFStep.Where(s=>s.StepStatus==stepStatus).FirstOrDefault();
            Common.LogHelper.WriteLog("SetSetpActivity:"+text);
            step.StepName = text;
            step.IsEndStep = end;
           
            if (end) {
                step.ProcessTime = DateTime.Now;
                step.ProcessResult = "审批结束";
                inst.Status = (short)HNCJ.Model.Enum.WFInstanceEnum.Proecessed;
                WFInstanceService.Update(inst);
            }
            WFStepService.Update(step);
        }
    }
}
