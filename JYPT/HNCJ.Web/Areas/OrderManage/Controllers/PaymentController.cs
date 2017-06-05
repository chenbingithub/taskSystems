using HNCJ.IBLL;
using HNCJ.Model;
using HNCJ.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HNCJ.Common;

namespace HNCJ.Web.Areas.OrderManage.Controllers
{
    public class PaymentController : AdminBaseController
    {
        //
        // GET: /Payment/
        public IPaymentService PaymentService { get; set; }
        public ActionResult Index()
        {
            return View();
        }
        #region 获取全部信息
        public ActionResult GetAllPayments()
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
            var pagedata = PaymentService.LoagPageData(param);
            var tmp = pagedata.Select(u => new { u.ID, u.PaymentName,u.APIURL,u.ModfiedOn, u.RegTime });
            int total = param.Total;
            var data = new { total = total, rows = tmp.ToList() };
            return Json(data);
        }
        #endregion
        #region 添加
        public ActionResult Add()
        {
            return View(new Payment { RegTime = DateTime.Now, ModfiedOn = DateTime.Now, DelFlag = true });
        }

        #endregion

        public ActionResult Delete(string ids)
        {
            if (string.IsNullOrEmpty(ids))
            {
                return Json(new { status = 0, msg = "请选中要删除的数据！" });
            }
            string[] strIds = ids.Split(',');
            List<int> idList = strIds.Select(int.Parse).ToList();
            if (PaymentService.DeleteList(idList))
            {
                DoSetManage.SysLogService.Insert(LoginUser, "删除支付类型");
                return Json(new { status = 1, msg = "删除成功！" });
            }
            else
            {
                return Json(new { status = 0, msg = "系统忙，请稍后重试" });
            }
        }

        public ActionResult Edit(int id)
        {
            return View("Add", PaymentService.GetModel(u => u.ID == id));
        }
        [HttpPost]
        public ActionResult Save(Payment Payment)
        {
            if (ModelState.IsValid)
            {
                if (Payment.ID == 0)
                {
                    if (PaymentService.Insert(Payment))
                    {
                        DoSetManage.SysLogService.Insert(LoginUser, "添加支付类型");
                        return Json(new { status = 1, msg = "添加成功" });
                    }
                    else
                    {
                        return Json(new { status = 0, msg = "系统忙，请稍后重试" });
                    }
                }
                else
                {
                    if (PaymentService.Update(Payment))
                    {
                        DoSetManage.SysLogService.Insert(LoginUser, "编辑支付类型");
                        return Json(new { status = 1, msg = "修改成功" });
                    }
                    else
                    {
                        return Json(new { status = 0, msg = "系统忙，请稍后重试" });
                    }
                }
            }
            else
            {
                return Json(new { status = 0, msg = "请输入合法的字段" });
            }
        }

        private string img(string strcmd, string strSub, string strSubinfo, string strid, string strMoney, string strUser, string strNum)
        {
            string strsellerEmail = "341081@qq.com";          //卖家支付宝帐号
            string strAc = "";        　　　　//卖家支付宝安全校验码
            string INTERFACE_URL = "https://www.alipay.com/payto:";
            string strCmd = strcmd;           //命令字
            string strSubject = strSub;       //商品名
            string strBody = strSubinfo;      //商品描述
            string strOrder_no = strid;       //商户订单号
            string strPrice = strMoney;       //商品单价 0.01～50000.00
            string rurl = "http://";          //商品展示网址
            string strType = "2";             //type支付类型    1：商品购买2：服务购买3：网络拍卖4：捐赠
            string strNumber = strNum;        //购买数量
            string strTransport = "3";        //发货方式        1：平邮2：快递3：虚拟物品
            string strOrdinary_fee = "";      //平邮运费
            string strExpress_fee = "";       //快递运费
            string strReadOnly = "true";      //交易信息是否只读
            string strBuyer_msg = "";         //买家给卖家的留言

            string strBuyer = "";             //买家EMAIL
            string strBuyer_name = strUser;   //买家姓名
            string strBuyer_address = "";     //买家地址
            string strBuyer_zipcode = "";     //买家邮编
            string strBuyer_tel = "";         //买家电话号码
            string strBuyer_mobile = "";      //买家手机号码
            string strPartner = "";           //合作伙伴ID    保留字段

            return CreatUrl(strsellerEmail, strAc, INTERFACE_URL, strCmd, strSubject, strBody,
                strOrder_no, strPrice, rurl, strType, strNumber, strTransport,
                strOrdinary_fee, strExpress_fee, strReadOnly, strBuyer_msg, strBuyer,
                strBuyer_name, strBuyer_address, strBuyer_zipcode, strBuyer_tel,
                strBuyer_mobile, strPartner);
        }
        private string CreatUrl(
        string strsellerEmail,
        string strAc,
        string INTERFACE_URL,
        string strCmd,
        string strSubject,
        string strBody,
        string strOrder_no,
        string strPrice,
        string rurl,
        string strType,
        string strNumber,
        string strTransport,
        string strOrdinary_fee,
        string strExpress_fee,
        string strReadOnly,
        string strBuyer_msg,
        string strBuyer,
        string strBuyer_name,
        string strBuyer_address,
        string strBuyer_zipcode,
        string strBuyer_tel,
        string strBuyer_mobile,
        string strPartner)
        {   //以下参数值不能留空

            string str2CreateAc = "";
            str2CreateAc += "cmd" + strCmd + "subject" + strSubject;
            str2CreateAc += "body" + strBody;
            str2CreateAc += "order_no" + strOrder_no;
            str2CreateAc += "price" + strPrice;
            str2CreateAc += "url" + rurl;
            str2CreateAc += "type" + strType;
            str2CreateAc += "number" + strNumber;
            str2CreateAc += "transport" + strTransport;
            str2CreateAc += "ordinary_fee" + strOrdinary_fee;
            str2CreateAc += "express_fee" + strExpress_fee;
            str2CreateAc += "readonly" + strReadOnly;
            str2CreateAc += "buyer_msg" + strBuyer_msg;
            str2CreateAc += "seller" + strsellerEmail;
            str2CreateAc += "buyer" + strBuyer;
            str2CreateAc += "buyer_name" + strBuyer_name;
            str2CreateAc += "buyer_address" + strBuyer_address;
            str2CreateAc += "buyer_zipcode" + strBuyer_zipcode;
            str2CreateAc += "buyer_tel" + strBuyer_tel;
            str2CreateAc += "buyer_mobile" + strBuyer_mobile;
            str2CreateAc += "partner" + strPartner;
            str2CreateAc += strAc;


            string acCode = GetMD5(str2CreateAc);

            string parameter = "";

            parameter += INTERFACE_URL + strsellerEmail + "?cmd=" + strCmd;
            parameter += "&subject=" + Server.UrlEncode(strSubject);
            parameter += "&body=" + Server.UrlEncode(strBody);
            parameter += "&order_no=" + strOrder_no;
            parameter += "&url=" + rurl;
            parameter += "&price=" + strPrice;
            parameter += "&type=" + strType;
            parameter += "&number=" + strNumber;
            parameter += "&transport=" + strTransport;
            parameter += "&ordinary_fee=" + strOrdinary_fee;
            parameter += "&express_fee=" + strExpress_fee;
            parameter += "&readonly=" + strReadOnly;
            parameter += "&buyer_msg=" + strBuyer_msg;
            parameter += "&buyer=" + strBuyer;
            parameter += "&buyer_name=" + Server.UrlEncode(strBuyer_name);
            parameter += "&buyer_address=" + strBuyer_address;
            parameter += "&buyer_zipcode=" + strBuyer_zipcode;
            parameter += "&buyer_tel=" + strBuyer_tel;
            parameter += "&buyer_mobile=" + strBuyer_mobile;
            parameter += "&partner=" + strPartner;
            parameter += "&ac=" + acCode;

            return parameter;
        }
         private static string GetMD5(string s)
        {
            System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] t = md5.ComputeHash(System.Text.Encoding.GetEncoding("gb2312").GetBytes(s));
            System.Text.StringBuilder sb = new System.Text.StringBuilder(32);
            for (int i = 0; i < t.Length; i++)
            {
                sb.Append(t[i].ToString("x").PadLeft(2, '0'));
            }
            return sb.ToString();
        }


    }
}
