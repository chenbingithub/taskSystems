using HNCJ.IBLL;
using HNCJ.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HNCJ.BLL
{
    public partial class FreightService : BaseService<Freight>, IFreightService
    {
        public IQueryable<Freight> LoagPageData(BaseParam queryParam)
        {
            var temp = DbSession.FreightDal.GetEntity(u => u.DelFlag == true);
            if (!string.IsNullOrEmpty(queryParam.KeyString))
            {
                switch (queryParam.Itemid)
                {
                    case 1: temp = temp.Where(u => u.FreightName.Contains(queryParam.KeyString)).AsQueryable(); break;
                    default: temp = temp.Where(u => u.FreightName.Contains(queryParam.KeyString)).AsQueryable(); break;
                }

            }
            queryParam.Total = temp.Count();

            return temp.OrderBy(u => u.ID).Skip(queryParam.PageSize * (queryParam.PageIndex - 1)).Take(queryParam.PageSize).AsQueryable();
        }


       
    }
}
