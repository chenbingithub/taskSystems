using HNCJ.Common;
using HNCJ.IBLL;
using HNCJ.Model;
using HNCJ.Web.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Enum = HNCJ.Model.Enum;

namespace HNCJ.Web.Controllers
{
    public class UserInfoController : BaseController
    {
        //
        // GET: /UserInfo/
        public IUserInfoService UserInfoService { get; set; }
       
        public IOrderInfoService OrderInfoService { get; set; }
        public IGoodsTypeService GoodsTypeService { get; set; }
        public IGoodsInfoService GoodsInfoService { get; set; }
        public IAddressInfoService AddressInfoService { get; set; }
        public ICartInfoService CartInfoService { get; set; }
        public IOrderDetailInfoService OrderDetailInfoService { get; set; }
        public ILoginLogService LoginLogService { get; set; }
        
        public ActionResult Index()
        {
            return View();
        }
        #region 获取头像
        [MyActionFilter(IsAdmin = false)]
        public ActionResult GetAvatarName()
        {
            var model = UserInfoService.GetModel(u => u.DelFlag == true && u.ID == LoginUser.ID);
            if (string.IsNullOrEmpty(model.Avatar)) model.Avatar = "/Content/jcrop/user-avatar.png";
            var data = new { name = model.UserName, path = model.Avatar, nick = model.NickName, amount = model.Amount ?? 0,point=model.Point??0};
            return Json(data);
        } 
        #endregion

        #region 判断是否登陆
        [MyActionFilter(IsCheckUserLogin = false)]   
        public ActionResult Check_login()
        {
            HttpCookie cookie = Request.Cookies["userLoginId"];
            int? count = 0;
            if (cookie == null)
            {
                return Json(new { status = "no", msg = "", count = count }, JsonRequestBehavior.AllowGet);
            }

            string userGuid = cookie.Value;
            if (Common.Cache.CacheHelper.GetCache(userGuid) == null)
            {
                return Json(new { status = "no", msg = "", count = count }, JsonRequestBehavior.AllowGet);
            }
            var list = CartInfoService.GetEntity(u => u.DelFlag == true && u.UserInfoID == LoginUser.ID && u.Status == 1).ToList();
            count = list.Aggregate(count, (current, item) => current + item.GoodsNum);

            return Json(new { status = "ok", msg = "",count=count }, JsonRequestBehavior.AllowGet);
        } 
        #endregion

        #region 注销操作
        [MyActionFilter(IsCheckUserLogin = false)]   
        public ActionResult Exits()
        {
            HttpCookie cookie = Request.Cookies["userLoginId"];
            if (cookie != null)
            {
                string userGuid = cookie.Value;
                Common.Cache.CacheHelper.SetCache(userGuid, "", DateTime.Now.AddMonths(-1));
                cookie.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(cookie);
            }
          
            return RedirectToAction("Login", "UserLogin");

        }
        [MyActionFilter(IsCheckUserLogin = false)] 
        public ActionResult AdminExits()
        {
            HttpCookie cookie = Request.Cookies["userLoginId"];
           
            if (cookie != null)
            {
                string userGuid = cookie.Value;
                Common.Cache.CacheHelper.SetCache(userGuid, "", DateTime.Now.AddMonths(-1));
                cookie.Value = null;
                cookie.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(cookie);
            }
            return RedirectToAction("AdminLogin", "UserLogin");

        } 
        #endregion

        #region 详细信息
        [MyActionFilter(IsAdmin=false)]
        public ActionResult Details()
        {
            ViewBag.New =
                LoginLogService.GetEntity(u => u.UserInfoID == this.LoginUser.ID)
                    .OrderByDescending(r => r.LoginTime).Take(2).ToList();
                   
            return View(UserInfoService.GetModel(this.LoginUser.ID));
        }
        [MyActionFilter(IsAdmin = false)]
        public ActionResult MyDetails()
        {
            
            return View(UserInfoService.GetModel(u => u.ID == this.LoginUser.ID));
        }
        [MyActionFilter(IsAdmin = false)]
        [HttpPost]
        public ActionResult MyDetails(UserInfo userInfo)
        {
            var user = UserInfoService.GetModel(u => u.ID == this.LoginUser.ID);
            user.NickName = userInfo.NickName;
            user.Sex = userInfo.Sex;
            user.Birthday = userInfo.Birthday;
            user.Telphone = userInfo.Telphone;
            user.Moblie = userInfo.Moblie;
            user.Email = userInfo.Email;
            user.Address = userInfo.Address;
            user.qq = userInfo.qq;
            user.area = userInfo.area;
            if (UserInfoService.Update(user))
            {
                return Json(new { status = 1, msg = "个人资料修改成功！" });
            }
            else {
                return Json(new { status = 0, msg = "系统忙，请稍后重试" });
            }
           
           
        }
        public ActionResult adminDetails()
        {
            return View(UserInfoService.GetModel(u => u.ID == this.LoginUser.ID));
        }
       
        #endregion

        #region 修改密码
        [MyActionFilter(IsAdmin = false)]
        public ActionResult ModifyPwd() {
            return View(UserInfoService.GetModel(u => u.ID == this.LoginUser.ID));
        }
        [HttpPost]
        public ActionResult ModifyPwd(string oldpwd,string newpwd1,string newpwd2)
        {
            if (!newpwd1.Equals(newpwd2)) {
                return Json(new { status =0, msg = "两次密码不一致！" });
            }
            if (UserInfoService.UpdatePwd(LoginUser.ID, oldpwd, newpwd1)) {
                return Json(new { status =1, msg = "密码修改成功！" });
            }
            return Json(new { status =0, msg = "出错了，请重试！" });
        }
        #endregion

        #region 设置图像
        [MyActionFilter(IsAdmin = false)]
        public ActionResult SetAvatar()
        {
            return View(UserInfoService.GetModel(u => u.DelFlag == true && u.ID == LoginUser.ID));
        }
        #endregion

        #region 裁剪图片
        public ActionResult CropSaveAs(string hideFileName, int hideWidth, int hideHeight, int hideX1, int hideY1)
        {
            string name = System.Web.HttpContext.Current.Server.MapPath(hideFileName);
            if (string.IsNullOrEmpty(name))
            {
                return Json(new { status =0, msg = "请选择一张图片！" });
            }
            string path = "";
            string imagepath = "";
            try
            {
                System.Drawing.Image imgPhoto = System.Drawing.Image.FromFile(name);
                Bitmap bmPhoto = new Bitmap(180, 180, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                //创建画笔并按照数据把指定的部分画到画板 上
                Graphics gbmPhoto = Graphics.FromImage(bmPhoto);
                gbmPhoto.DrawImage(imgPhoto, new Rectangle(0, 0, 180, 180), hideX1, hideY1, hideWidth, hideHeight, GraphicsUnit.Pixel);
                imagepath = "/Content/Images/Avatar/" + Guid.NewGuid().ToString() + ".jpg";
                path = Request.MapPath(imagepath);
                bmPhoto.Save(path, System.Drawing.Imaging.ImageFormat.Jpeg);
                //施放所有资源
                imgPhoto.Dispose();
                gbmPhoto.Dispose();
                bmPhoto.Dispose();
                if (System.IO.File.Exists(name))
                {
                    System.IO.File.Delete(name);
                }
            }
            catch (Exception e)
            {
                Common.LogHelper.WriteLog(e.ToString());
                if (System.IO.File.Exists(name))
                {
                    System.IO.File.Delete(name);
                }
                return Json(new { status =0, msg = "出错了，上传失败，请重试！" });
            }
            if (System.IO.File.Exists(path))
            {
                if (UserInfoService.SetAvatar(LoginUser.ID, imagepath))
                    return Json(new { status =1, msg = "头像上传成功！" });
            }
            return Json(new { status =0, msg = "出错了，上传失败，请重试！" });

        }
        #endregion

        #region 我的订单
        [MyActionFilter(IsAdmin = false)]
        public ActionResult MyOrderInfo() {
            int pageSize = int.Parse(Request["Size"] ?? "5");
            int pageIndex = int.Parse(Request["pageIndex"] ?? "1");
            var stutas = (int?)Model.Enum.OrderInfoEnum.Error;
            var data = UserInfoService.GetModel(u => u.DelFlag == true && u.ID == LoginUser.ID);
            var list = data.OrderInfo.Where(u => u.DelFlag == true && u.Status < stutas).ToList();
            ViewData.Model = list.OrderByDescending(u => u.RegTime).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            ViewBag.pageIndex = pageIndex;
            ViewBag.Size = Convert.ToInt32(Math.Ceiling((double)list.Count / pageSize));
            return View();
        }
        #endregion
        #region 订单详细
        [MyActionFilter(IsAdmin = false)]
        public ActionResult DetailsOrderInfo(int id = 0)
        {
            var model = OrderInfoService.GetModel(u => u.DelFlag == true && u.ID == id);            
            ViewData.Model =OrderDetailInfoService.GetEntity(u=>u.OrderInfoID==model.ID).ToList();
            ViewBag.OrderInfo = model;
            return View();
        }
        #endregion
        #region 关闭交易
        [MyActionFilter(IsAdmin = false)]
        public ActionResult CloseOrder()
        {
            int pageSize = int.Parse(Request["Size"] ?? "5");
            int pageIndex = int.Parse(Request["pageIndex"] ?? "1");
            var stutas = (int?)Model.Enum.OrderInfoEnum.Error;
            var data = UserInfoService.GetModel(u => u.DelFlag == true && u.ID == LoginUser.ID);
            var list = data.OrderInfo.Where(u => u.DelFlag == true && u.Status == stutas).ToList();
            ViewData.Model = list.OrderByDescending(u => u.RegTime).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            ViewBag.pageIndex = pageIndex;
            ViewBag.Size = Convert.ToInt32(Math.Ceiling((double)list.Count / pageSize));

            return View();
        }
        #endregion
        #region 删除订单
        public ActionResult DeleteOrderInfo(int id=0) {
            List<int> idList = new List<int>();
            idList.Add(id);
            OrderInfoService.DeleteListByLogical(idList);
            return RedirectToAction("MyOrderInfo");
        }
        #endregion

        #region 我的发布
        [MyActionFilter(IsAdmin = false)]
        public ActionResult MyRelease()
        {
            int pageSize = int.Parse(Request["Size"] ?? "10");
            int pageIndex = int.Parse(Request["pageIndex"] ?? "1");
            var status=(int)HNCJ.Model.Enum.GoodsInfoEnum.UpShelves;
            var data = UserInfoService.GetModel(u => u.DelFlag == true && u.ID == LoginUser.ID);
            var list = data.GoodsInfo.Where(u => u.DelFlag == true && u.Status == status).ToList();
            ViewData.Model = list.Skip((pageIndex - 1) * pageSize).Take(pageSize).OrderByDescending(u=>u.RegTime).ToList();
            ViewBag.pageIndex=pageIndex;
            ViewBag.Size = Convert.ToInt32(Math.Ceiling((double)list.Count / pageSize));
            return View();
        }
        #endregion
        #region 发布的详细
        [MyActionFilter(IsAdmin = false)]
        public ActionResult DetealsRelease(int id = 0) {
            return View(GoodsInfoService.GetModel(u => u.ID == id));
        }
        #endregion
        #region 下架列表
        [MyActionFilter(IsAdmin = false)]
        public ActionResult CloseRelease()
        {
            int pageSize = int.Parse(Request["Size"] ?? "10");
            int pageIndex = int.Parse(Request["pageIndex"] ?? "1");
            var status=(int)HNCJ.Model.Enum.GoodsInfoEnum.DownShelves;
            var data = UserInfoService.GetModel(u => u.DelFlag == true && u.ID == LoginUser.ID);
            var list = data.GoodsInfo.Where(u => u.DelFlag == true && u.Status == status).ToList();
            ViewData.Model = list.Skip((pageIndex - 1) * pageSize).Take(pageSize).OrderByDescending(u => u.RegTime).ToList();
            ViewBag.pageIndex = pageIndex;
            ViewBag.Size = Convert.ToInt32(Math.Ceiling((double)list.Count / pageSize));
            return View();
        }
        #endregion
        #region 编辑发布
        [MyActionFilter(IsAdmin = false)]
        public ActionResult EditRelease(int id = 0)
        {
            var model = GoodsInfoService.GetModel(u => u.ID == id);
            ViewBag.GoodsTypeList = GoodsTypeService.GetEntity(u => u.NodeID == 0).ToList();
            ViewData.Model = model;
            return View();
        }
        [HttpPost]
        public ActionResult EditRelease(GoodsInfo goods)
        {
            goods.ModfiedOn = DateTime.Now;
            if (GoodsInfoService.Update(goods))
            {
                return Json(new { status = 1, msg = "成功" });
            }
            else {
                return Json(new { status = 0, msg = "系统忙，请稍后重试" });
            }
        }
        #endregion
        #region 删除我的发布
        /// <summary>
        /// 把json数据转换为list集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public class JsonBinder<T> : IModelBinder
        {
            public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
            {
                //从请求中获取提交的参数数据   
                var json = controllerContext.HttpContext.Request.Form[bindingContext.ModelName] as string;
                //提交参数是对象   
                if (json.StartsWith("{") && json.EndsWith("}"))
                {
                    JObject jsonBody = JObject.Parse(json);
                    JsonSerializer js = new JsonSerializer();
                    object obj = js.Deserialize(jsonBody.CreateReader(), typeof(T));
                    return obj;
                }
                //提交参数是数组   
                if (json.StartsWith("[") && json.EndsWith("]"))
                {
                    IList<T> list = new List<T>();
                    JArray jsonRsp = JArray.Parse(json);
                    if (jsonRsp != null)
                    {
                        for (int i = 0; i < jsonRsp.Count; i++)
                        {
                            JsonSerializer js = new JsonSerializer();
                            try
                            {
                                object obj = js.Deserialize(jsonRsp[i].CreateReader(), typeof(T));
                                list.Add((T)obj);
                            }
                            catch (Exception e)
                            {
                                throw e;
                            }
                        }
                    }
                    return list;
                }
                return null;
            }
        }
        public class goods {
            public int goods_id { get; set; }

        }
        public ActionResult DeleteRelease([ModelBinder(typeof(JsonBinder<goods>))]List<goods> list,int isbool=0)
        {
            var status = (int)HNCJ.Model.Enum.GoodsInfoEnum.DownShelves;
            if (isbool == 1) {
                status=(int)HNCJ.Model.Enum.GoodsInfoEnum.UpShelves;
            }else if(isbool==2){
                status = (int)HNCJ.Model.Enum.GoodsInfoEnum.Delete;
            }
            List<int> idList = new List<int>();
            foreach (var item in list) {
                var data = GoodsInfoService.GetModel(u => u.ID == item.goods_id);
               data.Status = status;
               GoodsInfoService.Update(data);
            }
            return Json(new { status = "ok", msg = "操作成功！" }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        public ActionResult ConfirmOrder()
        {
           var goodsId=GoodsInfoService.GetEntity(u => u.UserInfoID == this.LoginUser.ID).Select(u=>u.ID);
           var orderId = OrderDetailInfoService.GetEntity(u => goodsId.Contains(u.GoodsInfoID)).Select(o => o.OrderInfoID);
            var orderList=OrderInfoService.GetEntity(r => orderId.Contains(r.ID)&&r.Status==(int)Enum.OrderInfoEnum.WaitSend).OrderByDescending(u => u.RegTime).ToList();
            ViewData.Model = orderList;
            return View();
        }

        public ActionResult UpdateGoodsNum(int goodsId,int quantity)
        {
            var goods=GoodsInfoService.GetModel(u => u.ID == goodsId);
            goods.GoodsNum = quantity;
            GoodsInfoService.Update(goods);
            return Json(new{status=1,msg="成功"});
        }

        public ActionResult Shipments(int orderid)
        {
            var order=OrderInfoService.GetModel(r=>r.ID==orderid);
            var orderdetails = order.OrderDetailInfo;
            if (orderdetails.Count > 1)
            {
                var userid = orderdetails.Select(u => u.GoodsInfo.UserInfoID).Distinct();
                if (userid.Count() > 1)
                {
                    return Json(new{status=0,msg="此订单来自不同的卖家，请关闭交易"});
                }
            }
            order.Status = (int) Enum.OrderInfoEnum.WaitDelivery;
            order.ModfiedOn = DateTime.Now;
            order.DeliverTime = DateTime.Now;
            if (OrderInfoService.Update(order))
            {
                return Json(new {status=1,msg="发货成功" });
            }
            else
            {
                return Json(new { status = 0, msg = "系统忙，请稍后重试" });
            }
            
        }
        public ActionResult Close(int orderid)
        {
            var order = OrderInfoService.GetModel(r => r.ID == orderid);
            order.Status = (int)Enum.OrderInfoEnum.Error;
            if (OrderInfoService.Update(order))
            {
                return Json(new { status = 1, msg = "关闭交易成功" });
            }
            else
            {
                return Json(new { status = 0, msg = "系统忙，请稍后重试" });
            }
        }

        public ActionResult Finish(int orderid)
        {
            var order = OrderInfoService.GetModel(r => r.ID == orderid);
            order.Status = (int)Enum.OrderInfoEnum.ReceivedGoods;
            order.SuccessTime = DateTime.Now;
            if (OrderInfoService.Update(order))
            {
                return Json(new { status = 1, msg = "完成交易成功" });
            }
            else
            {
                return Json(new { status = 0, msg = "系统忙，请稍后重试" });
            }
        }

        #region 我的购物车
        [MyActionFilter(IsAdmin = false)]
        public ActionResult MyCart()
        {
            ViewData.Model = CartInfoService.GetEntity(u => u.UserInfoID == LoginUser.ID && u.DelFlag == true&&u.Status==1).ToList();
            return View();
        }
        #endregion
        #region 删除购物车中的商品
        public ActionResult DeleteCart(int id = 0)
        {
            List<int> idList = new List<int> {id};
            CartInfoService.DeleteListByLogical(idList);
            return RedirectToAction("MyCart");
        }
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <returns></returns>
        public ActionResult DeleteCartInfo() {
            List<int> idList = (from key in Request.Form.AllKeys where key.StartsWith("chk_") select int.Parse(key.Replace("chk_", ""))).ToList();
            CartInfoService.DeleteListByLogical(idList);
            return Content("ok");
        }
        #endregion

        #region 注册操作
        [MyActionFilter(IsCheckUserLogin = false)]       
        public ActionResult Register()
        {
            return View();
        }
        [MyActionFilter(IsCheckUserLogin=false)]
        [HttpPost]
        public ActionResult Register(UserInfo userInfo)
        {
            if (UserInfoService.Exits(userInfo.UserName)) return Json(new{status=0,msg="用户名已存在"});
            if (ModelState.IsValid)
            {
                userInfo.RegTime = DateTime.Now;
                userInfo.ModfiedOn = DateTime.Now;
                userInfo.DelFlag = true;
                userInfo.salt = Utils.GetCheckCode(6); //获得6位的salt加密字符串
                userInfo.UserPwd = DESEncrypt.Encrypt(userInfo.UserPwd, userInfo.salt);
                if (UserInfoService.Insert(userInfo))
                {
                    return Json(new { status = 0, msg = "注册失败" });
                }
                string userLoginId = Guid.NewGuid().ToString();
                Common.Cache.CacheHelper.AddCache(userLoginId, userInfo, DateTime.Now.AddMinutes(20));
                //往客户端写入cookie
                Utils.WriteCookie("userLoginId", userLoginId, 14400);
                return Json(new { status = 1, msg = "注册成功" });
            }
            else {
                return Json(new { status = 0, msg = "请输入合法的数据" });
            }
        }
        [MyActionFilter(IsCheckUserLogin = false)]
        public ActionResult IsExits(string name)
        {
          return Json(UserInfoService.Exits(name));
        }
        #endregion

        
        
    }
}
