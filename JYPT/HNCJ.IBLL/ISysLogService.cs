using HNCJ.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HNCJ.IBLL
{
    public partial interface ISysLogService : IBaseService<SysLog>
    {
        IQueryable<SysLog> LoagPageData(BaseParam queryParam);
        void Insert(UserInfo userinfo, string operation);
    }
}
