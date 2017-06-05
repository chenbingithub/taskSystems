using HNCJ.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HNCJ.IBLL
{
    public partial interface IPaymentService : IBaseService<Payment>
    {
        IQueryable<Payment> LoagPageData(BaseParam queryParam);

    }
}
