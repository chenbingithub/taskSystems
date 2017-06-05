using HNCJ.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HNCJ.IBLL
{
    public partial interface IOrderInfoService : IBaseService<OrderInfo>
    {
        IQueryable<OrderInfo> LoagPageData(BaseParam queryParam);
        void AddOrderInfoDetails(int order_id, int goods_id, int goods_count, Decimal goods_price);
        void AddOrderInfoDetails(int order_id, List<int> cartIdList);
    }
}
