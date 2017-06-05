using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HNCJ.BLL;
using HNCJ.Common;
using HNCJ.IBLL;
using HNCJ.Model;
using HNCJ.Web.Controllers;
using HNCJ.Web.Models;
using System.Text;
namespace HNCJ.Web.Areas.GoodsManage.Controllers
{
    [MyActionFilter(IsCheckUserLogin = false)]
    public class CommentController : AdminBaseController
    {
        //
 
        public ICommentService CommentService { get; set; }
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult GetList(int goodsId)
        {
           var commentList= CommentService.GetEntity(u => u.GoodsInfoID == goodsId).OrderByDescending(u=>u.CreateTime).ToList();
            StringBuilder sb = new StringBuilder();
            commentList.ForEach(x =>
            {
                sb.AppendFormat("<li><div class='avatar'><img src={0}></div>", x.UserImg);
                sb.AppendFormat("<div class='inner'><p>{0}</p>", x.Content);
                sb.AppendFormat("<div class='meta'><span class='blue'>{0}</span>", x.UserName);
                sb.AppendFormat("<span class='time'>{0}</span></div></div></li>", x.CreateTime);
           

            });
            return Json(sb.ToString());
        }

        #region 添加
        public ActionResult Add()
        {
            Comment entity = new Comment() { 
              CreateTime=DateTime.Now
            };
            return View(entity);
        }

        public ActionResult Save(Comment entity)
        {

            if (ModelState.IsValid)
            {
                if (entity.ID == 0)
                {
                    if (LoginUser != null)
                    {
                        entity.UserName = LoginUser.UserName;
                        entity.UserImg = LoginUser.Avatar;
                    }
                    else
                    {
                        entity.UserName = "匿名用户";
                        entity.UserImg = "/Content/templates/main/images/user-avatar.png";
                    }
                    entity.CreateTime = DateTime.Now;
                    if (CommentService.Insert(entity))
                    {
                        DoSetManage.SysLogService.Insert(LoginUser, "添加评论");
                        return Json(new { status = 1, msg = "添加成功" });
                    }
                    else
                    {
                        return Json(new { status = 0, msg = "系统忙，请稍后重试" });
                    }
                }
                else
                {
                    if (CommentService.Update(entity))
                    {
                        DoSetManage.SysLogService.Insert(LoginUser, "编辑评论");
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
        #endregion

        #region 删除信息
        public ActionResult Delete(string ids)
        {
            if (string.IsNullOrEmpty(ids))
            {
                return Json(new { status = 0, msg = "请选中要删除的数据！" });
            }
            string[] strIds = ids.Split(',');
            List<int> idList = strIds.Select(int.Parse).ToList();
            if (CommentService.DeleteList(idList))
            {
                DoSetManage.SysLogService.Insert(LoginUser, "删除商品");
                return Json(new { status = 1, msg = "删除成功！" });

            }
            else
            {
                return Json(new { status = 0, msg = "系统忙，请稍后重试" });

            }
        }
        #endregion
        
        #region 修改信息
        public ActionResult Edit(int id=0)
        {
            return View("Add",CommentService.GetModel(u => u.ID == id));
        }
       
        #endregion


    }
}
