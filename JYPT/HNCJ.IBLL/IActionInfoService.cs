using HNCJ.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HNCJ.IBLL
{
    public partial interface IActionInfoService : IBaseService<ActionInfo>
    {
        IQueryable<ActionInfo> LoagPageData(BaseParam queryParam);
        bool SetRole(int userId, List<int> roleIds);
        ActionInfo Add(string url, string http);
        ActionInfo Add(string url, string http, ActionInfo data);
    }
}
