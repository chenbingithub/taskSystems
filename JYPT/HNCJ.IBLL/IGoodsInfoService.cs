using HNCJ.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HNCJ.IBLL
{
    public partial interface IGoodsInfoService : IBaseService<GoodsInfo>
    {
        IQueryable<GoodsInfo> LoagPageData(BaseParam queryParam);
        IQueryable<GoodsInfo> Seach(BaseParam queryParam);
        bool DoReview(List<int> ids);
    }
}
