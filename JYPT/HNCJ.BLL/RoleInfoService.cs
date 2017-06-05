using HNCJ.IBLL;
using HNCJ.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HNCJ.BLL
{
    public partial class RoleInfoService : BaseService<RoleInfo>, IRoleInfoService
    {
        public IQueryable<RoleInfo> LoagPageData(BaseParam queryParam)
        {
            var temp = DbSession.RoleInfoDal.GetEntity(u => u.DelFlag == true);
            if (!string.IsNullOrEmpty(queryParam.KeyString))
            {
                switch (queryParam.Itemid)
                {
                    case 1: temp = temp.Where(u => u.RoleName.Contains(queryParam.KeyString)).AsQueryable(); break;
                    case 2: temp = temp.Where(u => u.Description.Contains(queryParam.KeyString)).AsQueryable(); break;
                    default: temp = temp.Where(u => u.RoleName.Contains(queryParam.KeyString)).AsQueryable(); break;
                }

            }

            queryParam.Total = temp.Count();

            return temp.OrderBy(u => u.ID).Skip(queryParam.PageSize * (queryParam.PageIndex - 1)).Take(queryParam.PageSize).AsQueryable();
        }
        #region 设置权限
        public bool SetPermission(int roleId, List<int> actionIds)
        {
            var role = DbSession.RoleInfoDal.GetModel(u => u.ID == roleId);
            role.ActionInfo.Clear();
            var allActions = DbSession.ActionInfoDal.GetEntity(r => actionIds.Contains(r.ID));
            foreach (var actionInfo in allActions)
            {
                role.ActionInfo.Add(actionInfo);
            }

            return DbSession.SaveChanges() > 0;
        } 
        public bool SetPermissionItem(int roleId, List<int> actionIds)
        {
            var role = DbSession.RoleInfoDal.GetModel(u => u.ID == roleId);
            role.ActionItem.Clear();
            var allActions = DbSession.ActionItemDal.GetEntity(r => actionIds.Contains(r.ID));
            foreach (var actionInfo in allActions)
            {
                role.ActionItem.Add(actionInfo);
            }

            return DbSession.SaveChanges() > 0;
        } 
        #endregion
        
    }
}
