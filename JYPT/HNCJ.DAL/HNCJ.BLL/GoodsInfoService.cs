using HNCJ.IBLL;
using HNCJ.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HNCJ.BLL
{
    public partial class GoodsInfoService : BaseService<GoodsInfo>, IGoodsInfoService
    {


        public IQueryable<GoodsInfo> LoagPageData(BaseParam queryParam)
        {
            var temp = DbSession.GoodsInfoDal.GetEntity(u => u.DelFlag == true);
            if (!string.IsNullOrEmpty(queryParam.KeyString))
            {
                switch (queryParam.Itemid)
                {
                    case 1: temp = temp.Where(u => u.GoodsName.Contains(queryParam.KeyString)).AsQueryable(); break;
                    case 2: temp = temp.Where(u => u.GoodsType.TypeName.Contains(queryParam.KeyString)).AsQueryable(); break;
                    default: temp = temp.Where(u => u.GoodsName.Contains(queryParam.KeyString)).AsQueryable(); break;
                }

            }
            queryParam.Total = temp.Count();

            return temp.OrderBy(u => u.ID).Skip(queryParam.PageSize * (queryParam.PageIndex - 1)).Take(queryParam.PageSize).AsQueryable();

        }
        public IQueryable<GoodsInfo> Seach(BaseParam queryParam)
        {
            var temp = DbSession.GoodsInfoDal.GetEntity(u => u.DelFlag == true&&u.Status==(int)HNCJ.Model.Enum.GoodsInfoEnum.UpShelves);
            if (queryParam.Itemid != 0)
            {
                temp = temp.Where(u => u.GoodsTypeID == queryParam.Itemid).AsQueryable();
            }
            if (!string.IsNullOrEmpty(queryParam.SchName))
            {
                temp = temp.Where(u => u.GoodsName.Contains(queryParam.SchName)).AsQueryable();
            }
            
            queryParam.Total = temp.Count();

            return temp.OrderBy(u => u.ID).Skip(queryParam.PageSize * (queryParam.PageIndex - 1)).Take(queryParam.PageSize).AsQueryable();

        }

        public bool DoReview(List<int> ids)
        {
            ids.ForEach(x =>
            {
                var goods=GetModel(g => g.ID == x);
                goods.DelFlag = true;
                var price=goods.GoodsPrice ?? 0;
                var userinfo=DbSession.UserInfoDal.GetModel(u => u.ID == goods.UserInfoID);
                var amount=userinfo.Amount ?? 0;
                userinfo.Amount = amount + price;
                Update(goods);
                DbSession.UserInfoDal.Update(userinfo);
            });
           var id= DbSession.SaveChanges();
            return  true;
        }
    }
}
