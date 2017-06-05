using HNCJ.IBLL;
using HNCJ.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HNCJ.BLL
{
    public partial class ActionInfoService : BaseService<ActionInfo>, IActionInfoService
    {
        public IQueryable<ActionInfo> LoagPageData(BaseParam queryParam)
        {
            var temp = DbSession.ActionInfoDal.GetEntity(u => u.DelFlag == true);
            if (!string.IsNullOrEmpty(queryParam.KeyString))
            {
                switch (queryParam.Itemid)
                {
                    case 1: temp = temp.Where(u => u.ActionName.Contains(queryParam.KeyString)).AsQueryable(); break;
                    case 2: temp = temp.Where(u => u.Url.Contains(queryParam.KeyString)).AsQueryable(); break;
                    case 3: temp = temp.Where(u => u.HttpMethd.Contains(queryParam.KeyString)).AsQueryable(); break;
                    case 4: bool ddf = "是".Contains(queryParam.KeyString); var ff = ddf ? true : false; temp = temp.Where(u => u.IsMenu == ff).AsQueryable(); break;
                    default: temp = temp.Where(u => u.ActionName.Contains(queryParam.KeyString)).AsQueryable(); break;
                }

            }

            queryParam.Total = temp.Count();

            return temp.OrderBy(u => u.ID).Skip(queryParam.PageSize * (queryParam.PageIndex - 1)).Take(queryParam.PageSize).AsQueryable();
        }


        public bool SetRole(int userId, List<int> roleIds)
        {
            var action = DbSession.ActionInfoDal.GetEntity(u => u.ID == userId).FirstOrDefault();
            action.RoleInfo.Clear();
            var allRoles = DbSession.RoleInfoDal.GetEntity(r => roleIds.Contains(r.ID));
            foreach (var roleInfo in allRoles)
            {
                action.RoleInfo.Add(roleInfo);
            }
            DbSession.SaveChanges();
            return true;
        }
        public ActionInfo Add(string url, string http) {
           ActionInfo actio=DbSession.ActionInfoDal.GetEntity(u => u.Url==url && u.HttpMethd == http).FirstOrDefault();
           ActionInfo action = new ActionInfo();
            if (actio == null) {
               
               action.Url = url;
               action.HttpMethd = http;
               action.DelFlag = true;
               action.RegTime = DateTime.Now;
               action.ModfiedOn = DateTime.Now;
               action.IsMenu = false;
               action.ActionName = "修改";
               DbSession.ActionInfoDal.Add(action);
               DbSession.SaveChanges();
           }
            return action;
        }
        public ActionInfo Add(string url, string http,ActionInfo data)
        {
            ActionInfo actio = DbSession.ActionInfoDal.GetEntity(u => u.Url == url && u.HttpMethd == http).FirstOrDefault();
            ActionInfo action = new ActionInfo();
            if (actio == null)
            {
                action.Url = url;
                action.HttpMethd = http;
                action.DelFlag = true;
                action.RegTime = DateTime.Now;
                action.ModfiedOn = DateTime.Now;
                action.IsMenu = false;
                action.ParentId = data.ID;
                action.ParentName = data.ActionName;
                action.ActionName = ToString(url);
                DbSession.ActionInfoDal.Add(action);
                DbSession.SaveChanges();
            }
            return action;
        }
        private string ToString(string str) {
            var dd = str.Split('/');
            string tt = "";
            if (dd.Length > 3)
            {
                tt = dd[3];
            }
            else
            {
                tt = dd[2];
            }
            string obj="";
            switch (tt)
            {
                case "add": obj = "添加"; break;
                case "edit": obj = "编辑"; break;
                case "save": obj = "保存"; break;
                case "getallactioninfos": obj = "获取全部数据"; break;
                default: obj = "默认"; break;
            }
            return obj;
        }

        public override bool Update(ActionInfo entity)
        {
            if (entity.ID == entity.ParentId) return false;
            var model=GetModel(a => a.ID == entity.ID);
            var listdept = DbSession.ActionInfoDal.GetEntity(u => u.ParentId == entity.ID).ToList();
            if (listdept.Count!=0&&!model.ActionName.Equals(entity.ActionName))
            {  
                    listdept.ForEach(x =>
                    {
                        x.ParentName = entity.ActionName;
                        DbSession.ActionInfoDal.Update(x);
                    });
            }

            return base.Update(entity);
        }
    }
}
