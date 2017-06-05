using HNCJ.IBLL;
using HNCJ.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HNCJ.BLL
{
    public partial class GoodsTypeService : BaseService<GoodsType>, IGoodsTypeService
    {


        public IQueryable<GoodsType> LoagPageData(BaseParam queryParam)
        {
            var temp = DbSession.GoodsTypeDal.GetEntity(u => u.DelFlag == true);
            if (!string.IsNullOrEmpty(queryParam.KeyString))
            {
                switch (queryParam.Itemid)
                {
                    case 1: temp = temp.Where(u => u.TypeName.Contains(queryParam.KeyString)).AsQueryable(); break;
                    case 2: temp = temp.Where(u => u.English.Contains(queryParam.KeyString)).AsQueryable(); break;
                    default: temp = temp.Where(u => u.TypeName.Contains(queryParam.KeyString)).AsQueryable(); break;
                }

            }

            queryParam.Total = temp.Count();

            return temp.OrderByDescending(u => u.RegTime).Skip(queryParam.PageSize * (queryParam.PageIndex - 1)).Take(queryParam.PageSize).AsQueryable();
 
        }
        public List<GoodsList> GetGoodsList(int size)
        {
            List<GoodsList> list = new List<GoodsList>();
            GoodsList model=null;
            var type=DbSession.GoodsTypeDal.GetEntity(u=>u.DelFlag==true&&u.NodeID==0).ToList();
            foreach (var item in type) {
                
                model=new GoodsList();
                var tlist = DbSession.GoodsTypeDal.GetEntity(u => u.DelFlag == true && u.NodeID == item.ID).ToList();
                if (tlist.Count != 0)
                {
                    model.typelist = tlist;
                }
                model.ID = item.ID;
                model.TypeName = item.TypeName;
                model.Url = item.Url;
                model.list = DbSession.GoodsInfoDal.GetEntity(u => u.GoodsTypeID == item.ID && u.DelFlag == true&&u.Status==(int)HNCJ.Model.Enum.GoodsInfoEnum.UpShelves).OrderByDescending(u => u.RegTime).Take(size).ToList();
                list.Add(model);
            }
            return list;
        }

    }
}
