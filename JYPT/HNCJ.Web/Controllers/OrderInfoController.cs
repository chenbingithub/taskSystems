using HNCJ.Common;
using HNCJ.IBLL;
using HNCJ.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using HNCJ.Web.Models; 

namespace HNCJ.Web.Controllers
{
    public class OrderInfoController : BaseController
    {
        //
        // GET: /OrderInfo/
        public IOrderInfoService OrderInfoService { get; set; }
        public IGoodsInfoService GoodsInfoService { get; set; }
        public ICartInfoService CartInfoService { get; set; }
        public IPaymentService PaymentService { get; set; }
        public IDealsService DealsService { get; set; }
        public IFreightService FreightService { get; set; }
        public IAddressInfoService AddressInfoService { get; set; }
        public IOrderDetailInfoService OrderDetailInfoService { get; set; }
        public IUserInfoService UserInfoService { get; set; }
        
        public ActionResult Index()
        {
            return View();
        }
        
        #region 获取全部订单信息
        public ActionResult GetAllOrderInfos()
        {
            int pageSize = int.Parse(Request["rows"] ?? "10");
            int pageIndex = int.Parse(Request["page"] ?? "1");

            int itemid = int.Parse(Request["Itemid"] ?? "0");
            string keyString = Request["KeyString"];
            BaseParam param = new BaseParam()
            {
                PageSize = pageSize,
                PageIndex = pageIndex,
                Total = 0,
                Itemid = itemid,
                KeyString = keyString
            };
            var pagedata = OrderInfoService.LoagPageData(param);
            var tmp = pagedata.Select(u => new { u.ID, u.OrderName, UserName = u.UserInfo.UserName, u.Money, count =u.OrderDetailInfo.Count, u.ModfiedOn, u.RegTime });
            int total = param.Total;
            var data = new { total = total, rows = tmp.ToList() };
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion

        

        #region 删除订单信息
        public ActionResult Delete(string ids)
        {
            if (string.IsNullOrEmpty(ids))
            {
                return Json(new { status = 0, msg = "请选中要删除的数据" });
            }
            string[] strIds = ids.Split(',');
            List<int> idList = strIds.Select(int.Parse).ToList();
            if (OrderInfoService.DeleteListByLogical(idList)>0)
            {
                return Json(new { status = 1, msg = "删除成功" });
            }
            else
            {
                return Json(new { status = 0, msg = "系统忙，请稍后重试" });
            }
            
        }
        #endregion

        #region 订单详细信息
        public ActionResult Details(int id)
        {
            ViewData.Model = OrderInfoService.GetEntity(u => u.ID == id).FirstOrDefault();
            return View();
        }
        
        #endregion

        #region 加入购物车
        [MyActionFilter(IsCheckUserLogin=false)]
        public ActionResult AddCart(int goods_id, int goods_count,decimal goods_price)
        {
            if (LoginUser == null) return Json(new { status = "no", msg = "请登录" }, JsonRequestBehavior.AllowGet);
            var status = (int)HNCJ.Model.Enum.ShopCardEnum.WaitPayment;
            var data=CartInfoService.GetEntity(u => u.GoodsInfoID == goods_id && u.Status == status && u.UserInfoID == LoginUser.ID&&u.DelFlag==true).FirstOrDefault();
            if (data != null)
            {
                data.GoodsNum += goods_count;
                data.GoodsPrice = goods_price;
                CartInfoService.Update(data);
            }
            else
            {
                CartInfo cart = new CartInfo();
                cart.RegTime = DateTime.Now;
                cart.ModfiedOn = DateTime.Now;
                cart.DelFlag = true;
                cart.GoodsInfoID = goods_id;
                cart.GoodsNum = goods_count;
                cart.UserInfoID = LoginUser.ID;
                cart.Status = status;

                cart.GoodsPrice = goods_price;
                CartInfoService.Add(cart);
            }
            var list = CartInfoService.GetEntity(u => u.UserInfoID == LoginUser.ID && u.Status == status&&u.DelFlag==true).ToList();
            decimal? zoo = 0;
            int? length = 0;
            foreach (var item in list) {
                zoo += item.GoodsPrice*item.GoodsNum;
                length += item.GoodsNum;
            }
            

            return Json(new { status = "ok", msg = "添加成功", quantity = length, amount = zoo }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 取消订单
        public ActionResult Cancel(int id=0) {
            var model=OrderInfoService.GetEntity(u => u.DelFlag == true && u.ID == id).FirstOrDefault();
            var status =(int)HNCJ.Model.Enum.OrderInfoEnum.Error;
            model.Status = status;
            model.ModfiedOn = DateTime.Now;
            OrderInfoService.Update(model);
            return Json(new {status="ok",msg="取消订单成功！" },JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 清空购物车
        public ActionResult DeleteCart(int clear = 0, int cart_id=0, int goods_id=0)
        {
            List<int> list = new List<int>();
            if (clear == 1) {
               var data=CartInfoService.GetEntity(u => u.DelFlag == true && u.UserInfoID == LoginUser.ID).Select(u => u.ID).ToList();
               list.AddRange(data);
            }else{
               var data= CartInfoService.GetEntity(u => u.ID == cart_id && u.GoodsInfoID == goods_id).Select(u => u.ID).ToList();
               list.AddRange(data);
            }
            CartInfoService.DeleteListByLogical(list);
            return Json(new { status="ok",msg="" }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 修改购物车的数量
        public ActionResult updateCart(int quantity = 0, int cart_id = 0, int goods_id = 0)
        {
            var data=CartInfoService.GetEntity(u => u.DelFlag == true && u.ID == cart_id).FirstOrDefault();
            var goods = GoodsInfoService.GetEntity(u => u.DelFlag == true && u.ID == goods_id).FirstOrDefault();
            if (data == null) return Json(new { status = "no", msg = "没有找到该订单！" }, JsonRequestBehavior.AllowGet);
            data.GoodsNum = quantity;           
            CartInfoService.Update(data);
            return Json(new { status = "ok", msg = "" }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 结算中心

        [MyActionFilter(IsAdmin = false)]
        public ActionResult shop(int goods_id = 0, int goods_count =0)
        {
            ViewData.Model = GoodsInfoService.GetEntity(u => u.DelFlag == true && u.ID == goods_id).FirstOrDefault();
            var Fre = FreightService.GetEntity(u => u.DelFlag == true).ToList();
            ViewData["freight_id"] = (from u in Fre
                                      select new SelectListItem { Selected = false, Text = u.FreightName, Value = u.ID.ToString() }).ToList();
            var datas = AddressInfoService.GetEntity(u => u.DelFlag == true && u.UserInfoID == LoginUser.ID && u.IsDefault == true).ToList();
            var datas1 = AddressInfoService.GetEntity(u => u.DelFlag == true && u.UserInfoID == LoginUser.ID && u.IsDefault == false).OrderByDescending(u => u.RegTime).ToList();
            datas.AddRange(datas1);
            ViewBag.AddressInfo = datas;
            ViewBag.Count = goods_count;
            return View(); ;
        }
       
        [MyActionFilter(IsAdmin = false)]
        public ActionResult shopping(string ids) {
            string[] idList = ids.Split(',');
            List<CartInfo> list = new List<CartInfo>();
            foreach (var item in idList) {
                var id=int.Parse(item);
                var data=CartInfoService.GetEntity(u=>u.ID==id).FirstOrDefault();
                list.Add(data);
            }
            ViewData.Model = list;
            var Fre = FreightService.GetEntity(u => u.DelFlag == true).ToList();
            ViewData["freight_id"] = (from u in Fre
                                     select new SelectListItem { Selected = false, Text = u.FreightName, Value = u.ID.ToString() }).ToList();
            var datas = AddressInfoService.GetEntity(u => u.DelFlag == true && u.UserInfoID == LoginUser.ID && u.IsDefault == true).ToList();
            var datas1 = AddressInfoService.GetEntity(u => u.DelFlag == true && u.UserInfoID == LoginUser.ID && u.IsDefault == false).OrderByDescending(u=>u.RegTime).ToList();
            datas.AddRange(datas1);
            ViewBag.AddressInfo = datas;
            return View();
        }
        #endregion

        #region 提交订单
        public ActionResult submitOrderInfo(int address_id, int freight_id, string content, string cart_id)
        {
            
            var fre = FreightService.GetEntity(u => u.ID == freight_id).FirstOrDefault();
            var address = AddressInfoService.GetEntity(u => u.ID == address_id).FirstOrDefault();
            OrderInfo orderInfo = new OrderInfo();
            orderInfo.Address = Utais.ObjectToString(address);
            orderInfo.OrderName = Common.Utils.GetOrderNumber();
            orderInfo.RegTime = DateTime.Now;
            orderInfo.Status = (int)Model.Enum.OrderInfoEnum.WaitPayment;
            orderInfo.Content = content;
            orderInfo.UserInfoID = LoginUser.ID;
            orderInfo.Money = 0;
            orderInfo = OrderInfoService.Add(orderInfo);
            List<int> cartIdList = Common.Utils.GetStrList(cart_id);
            OrderInfoService.AddOrderInfoDetails(orderInfo.ID, cartIdList);
            return Json(new { status = "ok", msg = "添加成功",order_id=orderInfo.ID }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult submitorders(int address_id, int freight_id, string content,int goods_id,int quantity,decimal goods_price)
        {
            var fre = FreightService.GetEntity(u => u.ID == freight_id).FirstOrDefault();
            var address = AddressInfoService.GetEntity(u => u.ID == address_id).FirstOrDefault();
            OrderInfo orderInfo = new OrderInfo();
            orderInfo.Address = Utais.ObjectToString(address);
            orderInfo.OrderName = Common.Utils.GetOrderNumber();
            orderInfo.RegTime = DateTime.Now;
            orderInfo.Status = (int)Model.Enum.OrderInfoEnum.WaitPayment;
            orderInfo.Content = content;
            orderInfo.UserInfoID = LoginUser.ID;
            orderInfo.Money = 0;
            orderInfo = OrderInfoService.Add(orderInfo);
            OrderInfoService.AddOrderInfoDetails(orderInfo.ID, goods_id, quantity, goods_price);
            return Json(new { status = "ok", msg = "添加成功", order_id = orderInfo.ID }, JsonRequestBehavior.AllowGet);
        }
        
        #endregion


        #region 支付界面
        [MyActionFilter(IsAdmin = false)]
        public ActionResult payment(int order_id=0)
        {
            ViewData.Model = OrderInfoService.GetEntity(u=>u.ID==order_id&&u.DelFlag==true).FirstOrDefault();
            var payment = PaymentService.GetEntity(u => u.DelFlag == true).ToList();
            ViewData["PaymentID"] = (from a in payment
                                      select new SelectListItem() { Selected = false, Text = a.PaymentName, Value = a.ID.ToString() });
            return View();
        }

        public ActionResult paymentsubmit(Deals deals) {
            deals.DelFlag = true;
            deals.RegTime = DateTime.Now;
            deals=DealsService.Add(deals);
            var payment=PaymentService.GetEntity(u => u.ID == deals.PaymentID).FirstOrDefault();
            if (payment == null) return Redirect("/Error.html"); ;
            var url = payment.APIURL + "?id=" + deals.ID;
            return Json(new { status = 1, msg = "选择支付", url = url });
        }
        public ActionResult Paymentsuccess(int id)
        {
            var deals=DealsService.GetEntity(u => u.ID == id).FirstOrDefault();
            var orderinfo=OrderInfoService.GetEntity(u => u.OrderName == deals.pay_order_no).FirstOrDefault();
            if (orderinfo != null)
            {
                orderinfo.Status = (int)HNCJ.Model.Enum.OrderInfoEnum.WaitSend;
                OrderInfoService.Update(orderinfo);
            }
            return View();
        }

        public ActionResult DeliveryPay(int id)
        {
            var url = "/orderinfo/paymentsuccess" + "?id=" + id;
            return Json(new { status = 1, msg = "支付成功", url = url });
        }

        public ActionResult Onlinepayment(int id)
        {
            var status=(int)HNCJ.Model.Enum.OrderInfoEnum.WaitPayment;
            var deals = DealsService.GetModel(u => u.ID == id);
            var orderinfo = OrderInfoService.GetModel(u => u.OrderName == deals.pay_order_no && u.Status == status);
            if (orderinfo != null)
            {
                var userinfo=UserInfoService.GetModel(u => u.ID == LoginUser.ID);
                var amount = userinfo.Amount ?? 0;
                if (orderinfo.Money > amount)
                {
                    return Json(new{status=0,msg="余额不足，请充值后再支付"});
                }
                else
                {
                    userinfo.Amount = amount - orderinfo.Money;
                    UserInfoService.Update(userinfo);
                    orderinfo.Status = (int)HNCJ.Model.Enum.OrderInfoEnum.WaitSend;
                    OrderInfoService.Update(orderinfo);
                }
                
            }
            var url = "/orderinfo/paymentsuccess" + "?id=" + id;
            return Json(new { status = 1, msg = "支付成功", url = url });
        }

        #endregion

        #region Json转换为集合
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
        public class shopgoods
        {
            public int quantity { get; set; }
            public int cart_id { get; set; }
            public int goods_id { get; set; }
        }
        #endregion
    }
}
