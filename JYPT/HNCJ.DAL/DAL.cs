using HNCJ.IDAL;
using HNCJ.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace HNCJ.DAL
{
		
	public partial class ActionInfoDal:BaseDal<ActionInfo>,IActionInfoDal
	{
		public override bool Update(ActionInfo entity)
			{
				try
				{
					var oldentity = db.Set<ActionInfo>().Single(p => p.ID == entity.ID);
					db.Entry(oldentity).CurrentValues.SetValues(entity);
				}
				catch(Exception e){
					throw e;
				}
				return true ;
			}
	}

		
	public partial class ActionItemDal:BaseDal<ActionItem>,IActionItemDal
	{
		public override bool Update(ActionItem entity)
			{
				try
				{
					var oldentity = db.Set<ActionItem>().Single(p => p.ID == entity.ID);
					db.Entry(oldentity).CurrentValues.SetValues(entity);
				}
				catch(Exception e){
					throw e;
				}
				return true ;
			}
	}

		
	public partial class AddressInfoDal:BaseDal<AddressInfo>,IAddressInfoDal
	{
		public override bool Update(AddressInfo entity)
			{
				try
				{
					var oldentity = db.Set<AddressInfo>().Single(p => p.ID == entity.ID);
					db.Entry(oldentity).CurrentValues.SetValues(entity);
				}
				catch(Exception e){
					throw e;
				}
				return true ;
			}
	}

		
	public partial class CartInfoDal:BaseDal<CartInfo>,ICartInfoDal
	{
		public override bool Update(CartInfo entity)
			{
				try
				{
					var oldentity = db.Set<CartInfo>().Single(p => p.ID == entity.ID);
					db.Entry(oldentity).CurrentValues.SetValues(entity);
				}
				catch(Exception e){
					throw e;
				}
				return true ;
			}
	}

		
	public partial class CommentDal:BaseDal<Comment>,ICommentDal
	{
		public override bool Update(Comment entity)
			{
				try
				{
					var oldentity = db.Set<Comment>().Single(p => p.ID == entity.ID);
					db.Entry(oldentity).CurrentValues.SetValues(entity);
				}
				catch(Exception e){
					throw e;
				}
				return true ;
			}
	}

		
	public partial class DealsDal:BaseDal<Deals>,IDealsDal
	{
		public override bool Update(Deals entity)
			{
				try
				{
					var oldentity = db.Set<Deals>().Single(p => p.ID == entity.ID);
					db.Entry(oldentity).CurrentValues.SetValues(entity);
				}
				catch(Exception e){
					throw e;
				}
				return true ;
			}
	}

		
	public partial class DeptDal:BaseDal<Dept>,IDeptDal
	{
		public override bool Update(Dept entity)
			{
				try
				{
					var oldentity = db.Set<Dept>().Single(p => p.ID == entity.ID);
					db.Entry(oldentity).CurrentValues.SetValues(entity);
				}
				catch(Exception e){
					throw e;
				}
				return true ;
			}
	}

		
	public partial class FileInfoDal:BaseDal<FileInfo>,IFileInfoDal
	{
		public override bool Update(FileInfo entity)
			{
				try
				{
					var oldentity = db.Set<FileInfo>().Single(p => p.ID == entity.ID);
					db.Entry(oldentity).CurrentValues.SetValues(entity);
				}
				catch(Exception e){
					throw e;
				}
				return true ;
			}
	}

		
	public partial class FocusPictureDal:BaseDal<FocusPicture>,IFocusPictureDal
	{
		public override bool Update(FocusPicture entity)
			{
				try
				{
					var oldentity = db.Set<FocusPicture>().Single(p => p.ID == entity.ID);
					db.Entry(oldentity).CurrentValues.SetValues(entity);
				}
				catch(Exception e){
					throw e;
				}
				return true ;
			}
	}

		
	public partial class FreightDal:BaseDal<Freight>,IFreightDal
	{
		public override bool Update(Freight entity)
			{
				try
				{
					var oldentity = db.Set<Freight>().Single(p => p.ID == entity.ID);
					db.Entry(oldentity).CurrentValues.SetValues(entity);
				}
				catch(Exception e){
					throw e;
				}
				return true ;
			}
	}

		
	public partial class GoodsInfoDal:BaseDal<GoodsInfo>,IGoodsInfoDal
	{
		public override bool Update(GoodsInfo entity)
			{
				try
				{
					var oldentity = db.Set<GoodsInfo>().Single(p => p.ID == entity.ID);
					db.Entry(oldentity).CurrentValues.SetValues(entity);
				}
				catch(Exception e){
					throw e;
				}
				return true ;
			}
	}

		
	public partial class GoodsTypeDal:BaseDal<GoodsType>,IGoodsTypeDal
	{
		public override bool Update(GoodsType entity)
			{
				try
				{
					var oldentity = db.Set<GoodsType>().Single(p => p.ID == entity.ID);
					db.Entry(oldentity).CurrentValues.SetValues(entity);
				}
				catch(Exception e){
					throw e;
				}
				return true ;
			}
	}

		
	public partial class LinksInfoDal:BaseDal<LinksInfo>,ILinksInfoDal
	{
		public override bool Update(LinksInfo entity)
			{
				try
				{
					var oldentity = db.Set<LinksInfo>().Single(p => p.ID == entity.ID);
					db.Entry(oldentity).CurrentValues.SetValues(entity);
				}
				catch(Exception e){
					throw e;
				}
				return true ;
			}
	}

		
	public partial class LoginLogDal:BaseDal<LoginLog>,ILoginLogDal
	{
		public override bool Update(LoginLog entity)
			{
				try
				{
					var oldentity = db.Set<LoginLog>().Single(p => p.ID == entity.ID);
					db.Entry(oldentity).CurrentValues.SetValues(entity);
				}
				catch(Exception e){
					throw e;
				}
				return true ;
			}
	}

		
	public partial class OrderDetailInfoDal:BaseDal<OrderDetailInfo>,IOrderDetailInfoDal
	{
		public override bool Update(OrderDetailInfo entity)
			{
				try
				{
					var oldentity = db.Set<OrderDetailInfo>().Single(p => p.ID == entity.ID);
					db.Entry(oldentity).CurrentValues.SetValues(entity);
				}
				catch(Exception e){
					throw e;
				}
				return true ;
			}
	}

		
	public partial class OrderInfoDal:BaseDal<OrderInfo>,IOrderInfoDal
	{
		public override bool Update(OrderInfo entity)
			{
				try
				{
					var oldentity = db.Set<OrderInfo>().Single(p => p.ID == entity.ID);
					db.Entry(oldentity).CurrentValues.SetValues(entity);
				}
				catch(Exception e){
					throw e;
				}
				return true ;
			}
	}

		
	public partial class PaymentDal:BaseDal<Payment>,IPaymentDal
	{
		public override bool Update(Payment entity)
			{
				try
				{
					var oldentity = db.Set<Payment>().Single(p => p.ID == entity.ID);
					db.Entry(oldentity).CurrentValues.SetValues(entity);
				}
				catch(Exception e){
					throw e;
				}
				return true ;
			}
	}

		
	public partial class RoleInfoDal:BaseDal<RoleInfo>,IRoleInfoDal
	{
		public override bool Update(RoleInfo entity)
			{
				try
				{
					var oldentity = db.Set<RoleInfo>().Single(p => p.ID == entity.ID);
					db.Entry(oldentity).CurrentValues.SetValues(entity);
				}
				catch(Exception e){
					throw e;
				}
				return true ;
			}
	}

		
	public partial class SysLogDal:BaseDal<SysLog>,ISysLogDal
	{
		public override bool Update(SysLog entity)
			{
				try
				{
					var oldentity = db.Set<SysLog>().Single(p => p.ID == entity.ID);
					db.Entry(oldentity).CurrentValues.SetValues(entity);
				}
				catch(Exception e){
					throw e;
				}
				return true ;
			}
	}

		
	public partial class UserInfoDal:BaseDal<UserInfo>,IUserInfoDal
	{
		public override bool Update(UserInfo entity)
			{
				try
				{
					var oldentity = db.Set<UserInfo>().Single(p => p.ID == entity.ID);
					db.Entry(oldentity).CurrentValues.SetValues(entity);
				}
				catch(Exception e){
					throw e;
				}
				return true ;
			}
	}

		
	public partial class WFInstanceDal:BaseDal<WFInstance>,IWFInstanceDal
	{
		public override bool Update(WFInstance entity)
			{
				try
				{
					var oldentity = db.Set<WFInstance>().Single(p => p.ID == entity.ID);
					db.Entry(oldentity).CurrentValues.SetValues(entity);
				}
				catch(Exception e){
					throw e;
				}
				return true ;
			}
	}

		
	public partial class WFStepDal:BaseDal<WFStep>,IWFStepDal
	{
		public override bool Update(WFStep entity)
			{
				try
				{
					var oldentity = db.Set<WFStep>().Single(p => p.ID == entity.ID);
					db.Entry(oldentity).CurrentValues.SetValues(entity);
				}
				catch(Exception e){
					throw e;
				}
				return true ;
			}
	}

		
	public partial class WFTempDal:BaseDal<WFTemp>,IWFTempDal
	{
		public override bool Update(WFTemp entity)
			{
				try
				{
					var oldentity = db.Set<WFTemp>().Single(p => p.ID == entity.ID);
					db.Entry(oldentity).CurrentValues.SetValues(entity);
				}
				catch(Exception e){
					throw e;
				}
				return true ;
			}
	}

	
}