using HNCJ.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HNCJ.IBLL
{
    public partial interface ILinksInfoService : IBaseService<LinksInfo>
    {
        IQueryable<LinksInfo> LoagPageData(BaseParam queryParam);

    }
}
