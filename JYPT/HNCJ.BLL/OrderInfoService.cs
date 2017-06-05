using HNCJ.IBLL;
using HNCJ.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HNCJ.BLL
{
    public partial class OrderInfoService : BaseService<OrderInfo>, IOrderInfoService
    {
        public IQueryable<OrderInfo> LoagPageData(BaseParam queryParam)
        {
            var temp = DbSession.OrderInfoDal.GetEntity(u => u.DelFlag == true);
            if (!string.IsNullOrEmpty(queryParam.KeyString))
            {
                switch (queryParam.Itemid)
                {
                    case 1: temp = temp.Where(u => u.OrderName.Contains(queryParam.KeyString)).AsQueryable(); break;
                    default: temp = temp.Where(u => u.OrderName.Contains(queryParam.KeyString)).AsQueryable(); break;
                }

            }
            queryParam.Total = temp.Count();

            return temp.OrderByDescending(u => u.RegTime).Skip(queryParam.PageSize * (queryParam.PageIndex - 1)).Take(queryParam.PageSize).AsQueryable();
        }




        public void AddOrderInfoDetails(int order_id, int goods_id, int goods_count, decimal goods_price)
        {
            OrderDetailInfo model = new OrderDetailInfo();
            model.GoodsInfoID = goods_id;
            model.GoodsNum = goods_count;
            model.GoodsPrice = goods_price;
            model.DelFlag = true;
            model.OrderInfoID = order_id;            
            model.RegTime = DateTime.Now;
            DbSession.OrderDetailInfoDal.Add(model);
            var orderinfo=DbSession.OrderInfoDal.GetEntity(u => u.ID == order_id).FirstOrDefault();
            orderinfo.Money += goods_count * goods_price;
            DbSession.OrderInfoDal.Update(orderinfo);
            DbSession.SaveChanges();
        }


        public void AddOrderInfoDetails(int order_id, List<int> cartIdList)
        {
            OrderDetailInfo model = null;
            Decimal? allmoney = 0;
            foreach(var item in cartIdList){
               var cart= DbSession.CartInfoDal.GetEntity(u => u.ID == item).FirstOrDefault();
               model = new OrderDetailInfo();
               model.GoodsInfoID = cart.GoodsInfoID;
               model.GoodsNum = cart.GoodsNum;
               model.GoodsPrice =cart.GoodsPrice;
               model.DelFlag = true;
               model.OrderInfoID = order_id;
               model.RegTime = DateTime.Now;
               DbSession.OrderDetailInfoDal.Add(model);
               cart.Status = (int)HNCJ.Model.Enum.ShopCardEnum.FinishPayment;
               DbSession.CartInfoDal.Update(cart);
               allmoney += cart.GoodsNum * cart.GoodsPrice;
            }
            var orderinfo = DbSession.OrderInfoDal.GetEntity(u => u.ID == order_id).FirstOrDefault();
            orderinfo.Money += allmoney;
            DbSession.OrderInfoDal.Update(orderinfo);
            DbSession.SaveChanges();
            
        }
    }
}
