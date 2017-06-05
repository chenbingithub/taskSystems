using HNCJ.IBLL;
using HNCJ.Model;
using HNCJ.Web.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HNCJ.Web.Controllers
{
    [MyActionFilter(IsCheckUserLogin = false)]
    public class HomePageController : BaseController
    {
        //
        // GET: /HomePage/
        public IGoodsInfoService GoodsInfoService { get; set; }
        public IGoodsTypeService GoodsTypeService { get; set; }
        public IFocusPictureService FocusPictureService { get; set; }
        public ILinksInfoService LinksInfoService { get; set; }
        public ActionResult Index()
        {
              ViewData.Model = GoodsTypeService.GetEntity(u => u.DelFlag == true&&u.NodeID==0).ToList();
            ViewData["Focus"] = FocusPictureService.GetEntity(u => u.DelFlag == true).ToList();
            ViewBag.GoodsList = GoodsTypeService.GetGoodsList(5);
            return View();
        }
        public ActionResult Details(int id)
        {
            var model=GoodsInfoService.GetModel(u => u.ID == id);
            model.Point += 1;
            GoodsInfoService.Update(model);
            ViewBag.UserInfo = model.UserInfo;
            ViewBag.GoodsList1 = GoodsInfoService.GetEntity(g => g.GoodsTypeID == model.GoodsTypeID).OrderByDescending(r => r.ModfiedOn).Take(4);
            ViewBag.GoodsList= GoodsInfoService.GetList().OrderByDescending(r => r.Point).Take(8);
            return View(model);
        }
        public ActionResult List(int typeId=0,string value="")
        {
            ViewData.Model = GoodsTypeService.GetEntity(u => u.DelFlag == true && u.NodeID == 0).ToList();
            ViewData["Focus"] = FocusPictureService.GetEntity(u => u.DelFlag == true).ToList();
            int pageSize = int.Parse(Request["row"] ?? "10");
            int pageIndex = int.Parse(Request["pageIndex"] ?? "1");
           

            BaseParam param = new BaseParam()
            {
                PageSize = pageSize,
                PageIndex = pageIndex,
                Total = 0,
                SchName = value,
                Itemid = typeId
            };
            var data = GoodsInfoService.Seach(param).ToList();
            pageSize = Convert.ToInt32(Math.Ceiling((double)param.Total / pageSize)); ;
            ViewBag.Goods = data;
            ViewBag.pageIndex = pageIndex;
            ViewBag.Size = pageSize;
            ViewBag.Value = value;
            ViewBag.typeId = typeId;
            return View();
        }
        #region 辅助方法
        public ActionResult Links()
        {
            var data = LinksInfoService.GetEntity(u => u.DelFlag == true).OrderByDescending(u => u.ID).Select(u => new { u.ID, u.Url, u.Title }).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ShowVcode()
        {
            Common.ValidateCode validateCode = new Common.ValidateCode();
            string code = validateCode.CreateValidateCode(4);
            Session["code"] = code;
            byte[] image = validateCode.CreateValidateGraphic(code);
            return File(image, @"image/jpeg");
        }
        public ActionResult Linksman()
        {
            return View();
        } 
        #endregion

        #region 发布信息
        [MyActionFilter(IsAdmin=false)]
        public ActionResult Message()
        {
            ViewBag.GoodstypeList = GoodsTypeService.GetEntity(u => u.DelFlag == true).ToList();
            GoodsInfo entity = new GoodsInfo() { 
                RegTime = DateTime.Now,
                ModfiedOn = DateTime.Now,
                DelFlag = false,
                Status = (int)HNCJ.Model.Enum.GoodsInfoEnum.UpShelves,
                UserInfoID = this.LoginUser.ID,
                GoodsNum=1
            };

            return View(entity);
        }
        #region//上传图片
        public ActionResult Upload()
        {

            HttpFileCollection postFile = System.Web.HttpContext.Current.Request.Files;//接收文件
            if (postFile == null)
            {
                return Content("no:请选择上传文件!");
            }
            else
            {
                string fileName = Path.GetFileName(postFile[0].FileName);//文件名称
                string fileExt = Path.GetExtension(fileName);//文件的扩展名
                if (fileExt == ".jpg" || fileExt == ".png")
                {
                    string dir = "/Content/Goods/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + DateTime.Now.Day + "/";
                    Directory.CreateDirectory(Path.GetDirectoryName(Request.MapPath(dir)));//创建文件夹
                    string newfileName = Guid.NewGuid().ToString();//新的文件名
                    string fileDir = dir + newfileName + fileExt;//完整的路径
                    postFile[0].SaveAs(Server.MapPath(fileDir));//保存文件
                    return Content("ok:" + fileDir);
                }
                else
                {
                    return Content("no:格式不正确!");
                }

            }

        }
        #endregion
        #endregion

    }
}
