using HNCJ.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HNCJ.IBLL
{
    public partial interface IRoleInfoService:IBaseService<RoleInfo>
    {
        IQueryable<RoleInfo> LoagPageData(BaseParam queryParam);
        bool SetPermission(int roleId, List<int> actionIds);
        bool SetPermissionItem(int roleId, List<int> actionIds);
    }
}
