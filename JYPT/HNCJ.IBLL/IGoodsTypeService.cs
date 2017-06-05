using HNCJ.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HNCJ.IBLL
{
    public partial interface IGoodsTypeService : IBaseService<GoodsType>
    {
        IQueryable<GoodsType> LoagPageData(BaseParam queryParam);
        List<GoodsList> GetGoodsList(int size);
    }
}
