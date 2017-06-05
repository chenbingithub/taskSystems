using HNCJ.Common;
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
    [MyActionFilter(IsCheckUserLogin=false)]
    public class UserLoginController : Controller
    {
        //
        // GET: /UserLogin/
        public IUserInfoService UserInfoService { get; set; }
        public ILoginLogService LoginLogService { get; set; }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult adminLogin()
        {
            return View();
        }
        /// <summary>
        /// 生成验证码
        /// </summary>
        /// <returns>返回一张图片</returns>
        public ActionResult ShowVcode()
        {
            Common.ValidateCode validateCode = new Common.ValidateCode();
            string code=validateCode.Str(6);
            Session["code"] = code;
            byte[] image = validateCode.CreateValidateGraphic(code);
            return File(image,@"image/jpeg");
        }
        /// <summary>
        /// 管理员登录请求
        /// </summary>
        /// <returns></returns>
        public ActionResult adminProcessLogin()
        {
            string code = Session["code"] as string;
            Session["code"] = null;
            string vcode = Request["vCode"] as string;
            if (string.IsNullOrEmpty(code))
            {
                return Content("no:验证码错误！");
            }
            if (string.IsNullOrEmpty(vcode))
            {
                return Content("no:验证码为空！");
            }
            if (!code.ToLower().Equals(vcode.ToLower()))
            {
                return Content("no:验证码错误！");
            }
            string name = Request["LoginCode"];
            string pwd = Request["LoginPwd"];
            UserInfo userInfo = UserInfoService.GetModel(name,pwd,true);
            if (userInfo == null) {
                return Content("no:用户名或密码错误！");
            }
            //立即分配一个标志Guid，把标志作为memcache存储数据的key，把用户对象放到memcache.把Guid写到客户端cookie里面去。
            string userLoginId = Guid.NewGuid().ToString();
            Common.Cache.CacheHelper.AddCache(userLoginId, userInfo, DateTime.Now.AddMinutes(20));
            DoSetManage.SetUserInfo(userInfo);
            //往客户端写入cookie
            Utils.WriteCookie("userLoginId", userLoginId, 30);
            LoginLog log = new LoginLog
            {
                DelFlag = true,
                LoginTime = DateTime.Now,
                IP = Common.Utils.GetIp(),
                UserInfoID = userInfo.ID,
                UserName = userInfo.UserName
            };
            LoginLogService.Add(log);
            return Content("ok:成功！");
           
        }
        /// <summary>
        /// 用户登录请求
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ProcessLogin(string UserName, string Password)
        {
            if (string.IsNullOrEmpty(UserName)||string.IsNullOrEmpty(Password))
            {
                return Json(new { status = "no", msg = "用户名或密码为空" });
            }
            var entity=UserInfoService.GetEntity(u => u.UserName == UserName).SingleOrDefault();
            if (entity == null) return Json(new { status = "no", msg = "用户名不存在" });
            UserInfo userInfo = UserInfoService.GetModel(UserName, Password, true);
            //立即分配一个标志Guid，把标志作为memcache存储数据的key，把用户对象放到memcache.把Guid写到客户端cookie里面去。
          if (userInfo == null) {
              return Json(new { status = "no", msg = "密码错误" });
          }
            string userLoginId = Guid.NewGuid().ToString();
            Common.Cache.CacheHelper.AddCache(userLoginId, userInfo, DateTime.Now.AddMinutes(20));
            DoSetManage.SetUserInfo(userInfo);
            //往客户端写入cookie
            Utils.WriteCookie("userLoginId", userLoginId,14400);
            LoginLog log = new LoginLog
            {
                DelFlag = true,
                LoginTime = DateTime.Now,
                IP = Common.Utils.GetIp(),
                UserInfoID = userInfo.ID,
                UserName = userInfo.UserName
            };
            LoginLogService.Add(log);
            return Json(new { status = "ok", msg = "登录成功" });
 
        }

        public ActionResult repassword()
        {
            return View();
        }

        public ActionResult SendSMS(string username,string telphone)
        {
            Common.ValidateCode validateCode = new Common.ValidateCode();
            string SMS = validateCode.CreateValidateCode(6);
            Utils.WriteCookie("smscode", SMS,30);
            if (!string.IsNullOrEmpty(telphone) && !string.IsNullOrEmpty(username))
            {
                var model = UserInfoService.GetModel(u => u.UserName == username.Trim());
                if (model != null)
                {
                    if (model.Telphone.Equals(telphone))
                    {
                        Utais.ReturnCode(telphone, SMS);
                        return Json(new { status = 1, msg = "" });
                    }
                    else
                    {
                        return Json(new {status = 0, msg = "预留手机号不正确"});
                    }
                }
                else
                {
                    return Json(new {status = 0, msg = "用户名不存在"});
                }
            }
            else
            {
                return Json(new {status = 0, msg = "用户名和手机号不能为空"});
            }
        }

        public ActionResult Submit(string username, string telphone, string txtCode, string Password, string qPassword)
        {
            if (!string.IsNullOrEmpty(Password) && !string.IsNullOrEmpty(qPassword))
            {
                if (!Password.Equals(qPassword))
                {
                    return Json(new { status = 0, msg = "两次密码不一致" });

                }  
            }
            else
            {
                return Json(new { status = 0, msg = "密码不能为空" });
            }
            string code = Utils.GetCookie("smscode");
            if (!string.IsNullOrEmpty(txtCode) && !string.IsNullOrEmpty(code))
            {
                if (txtCode.Equals(code))
                {
                    
                        if (UserInfoService.UpdatePwd(username,Password))
                        {
                            return Json(new { status = 1, msg = "密码重置成功",url="/userlogin/login" });
                        }else
                        {
                            return Json(new { status = 0, msg = "密码重置失败" });

                        }

                }
                else
                {
                    return Json(new { status = 0, msg = "验证码不正确" });
                }
            }
            else
            {
                return Json(new { status = 0, msg = "验证码为空" });

            }

        }
    }
}
