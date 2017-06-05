using HNCJ.IBLL;
using HNCJ.Model;
using HNCJ.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HNCJ.Web.Controllers
{
    [MyActionFilter(IsRoleAction=false,IsAdmin=false)]
    public class AddressInfoController : BaseController
    {
        //
        // GET: /AddressInfo/
        public IAddressInfoService AddressInfoService { get; set; }
        public IUserInfoService UserInfoService { get; set; }
        #region 收货地址首页
        public ActionResult Index()
        {
            var data=AddressInfoService.GetEntity(u => u.DelFlag == true && u.UserInfoID == LoginUser.ID&&u.IsDefault==true).ToList();
            var data1=AddressInfoService.GetEntity(u => u.DelFlag == true && u.UserInfoID == LoginUser.ID&&u.IsDefault==false).OrderByDescending(u=>u.RegTime).ToList();
            data.AddRange(data1);
            ViewData.Model = data;
            return View();
        } 
        #endregion
        #region 添加新地址
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(AddressInfo addressInfo)
        {
            string code = Session["code"] as string;
            Session["code"] = null;
            string vcode = Request["vCode"] as string;
            if (string.IsNullOrEmpty(vcode))
            {
                return Json(new { status = "no", msg = "请输入验证码！" },JsonRequestBehavior.AllowGet);
            }
            if (string.IsNullOrEmpty(code) || !code.Equals(vcode))
            {
                return Json(new { status = "no", msg = "验证码不正确！" }, JsonRequestBehavior.AllowGet);
            }
            if (ModelState.IsValid)
            {
                addressInfo.RegTime = DateTime.Now;
                addressInfo.ModfiedOn = DateTime.Now;
                addressInfo.DelFlag = true;
                addressInfo.UserInfoID = LoginUser.ID;

                AddressInfoService.Add(addressInfo);
                return Json(new { status = "ok", msg = "添加新地址成功！" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { status = "no", msg = "出错了！" }, JsonRequestBehavior.AllowGet);
        } 
        #endregion

        #region 设置默认
        public ActionResult Default(int id = 0)
        {
            AddressInfo addressinfo = AddressInfoService.GetEntity(u => u.DelFlag == true && u.ID == id).FirstOrDefault();
            if (addressinfo == null)
            {
                return Json(new { status = "no", msg = "订单不存在！" }, JsonRequestBehavior.AllowGet);
            }
            AddressInfoService.UpdateIsDefault(id);
            addressinfo.IsDefault = true;
            addressinfo.ModfiedOn = DateTime.Now;
            AddressInfoService.Update(addressinfo);
            return Json(new { status = "ok", msg = "默认收货地址设置成功！" }, JsonRequestBehavior.AllowGet);
        } 
        #endregion
        #region 删除地址信息
        public ActionResult Delete(string Id)
        {
            string[] ids = Id.Split(',');
            if (ids.Length == 0) return Json(new { status = "no", msg = "请选择要删除的！" }, JsonRequestBehavior.AllowGet);
            List<int> idList = new List<int>();
            foreach (var item in ids) {
                idList.Add(int.Parse(item));
            }
            AddressInfoService.DeleteListByLogical(idList);
            return  Json(new {status="ok",msg="删除收货地址成功！" },JsonRequestBehavior.AllowGet); 
        }
        #endregion

        #region 修改地址信息
        public ActionResult Edit(int id=0)
        {
            ViewData.Model = AddressInfoService.GetEntity(u => u.ID == id).FirstOrDefault();
            return View();
        }
        [HttpPost]
        public ActionResult Edit(AddressInfo AddressInfo)
        {
            AddressInfo.ModfiedOn = DateTime.Now;
            AddressInfoService.Update(AddressInfo);
            return Json(new { status = "ok", msg = "修改收货地址成功！" }, JsonRequestBehavior.AllowGet); 
        }
        #endregion
    }
}
