using HNCJ.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HNCJ.IBLL
{
    public partial interface IFreightService : IBaseService<Freight>
    {
        IQueryable<Freight> LoagPageData(BaseParam queryParam);

    }
}
