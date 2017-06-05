


using HNCJ.DalFactory;
using HNCJ.IBLL;
using HNCJ.IDAL;
using HNCJ.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HNCJ.BLL
{
		
	public partial class ActionInfoService:BaseService<ActionInfo>,IActionInfoService
	{
		public override void SetCurrentDal()
			{
				CurretDal = DbSession.ActionInfoDal;
			}
	}

		
	public partial class ActionItemService:BaseService<ActionItem>,IActionItemService
	{
		public override void SetCurrentDal()
			{
				CurretDal = DbSession.ActionItemDal;
			}
	}

		
	public partial class AddressInfoService:BaseService<AddressInfo>,IAddressInfoService
	{
		public override void SetCurrentDal()
			{
				CurretDal = DbSession.AddressInfoDal;
			}
	}

		
	public partial class CartInfoService:BaseService<CartInfo>,ICartInfoService
	{
		public override void SetCurrentDal()
			{
				CurretDal = DbSession.CartInfoDal;
			}
	}

		
	public partial class CommentService:BaseService<Comment>,ICommentService
	{
		public override void SetCurrentDal()
			{
				CurretDal = DbSession.CommentDal;
			}
	}

		
	public partial class DealsService:BaseService<Deals>,IDealsService
	{
		public override void SetCurrentDal()
			{
				CurretDal = DbSession.DealsDal;
			}
	}

		
	public partial class DeptService:BaseService<Dept>,IDeptService
	{
		public override void SetCurrentDal()
			{
				CurretDal = DbSession.DeptDal;
			}
	}

		
	public partial class FileInfoService:BaseService<FileInfo>,IFileInfoService
	{
		public override void SetCurrentDal()
			{
				CurretDal = DbSession.FileInfoDal;
			}
	}

		
	public partial class FocusPictureService:BaseService<FocusPicture>,IFocusPictureService
	{
		public override void SetCurrentDal()
			{
				CurretDal = DbSession.FocusPictureDal;
			}
	}

		
	public partial class FreightService:BaseService<Freight>,IFreightService
	{
		public override void SetCurrentDal()
			{
				CurretDal = DbSession.FreightDal;
			}
	}

		
	public partial class GoodsInfoService:BaseService<GoodsInfo>,IGoodsInfoService
	{
		public override void SetCurrentDal()
			{
				CurretDal = DbSession.GoodsInfoDal;
			}
	}

		
	public partial class GoodsTypeService:BaseService<GoodsType>,IGoodsTypeService
	{
		public override void SetCurrentDal()
			{
				CurretDal = DbSession.GoodsTypeDal;
			}
	}

		
	public partial class LinksInfoService:BaseService<LinksInfo>,ILinksInfoService
	{
		public override void SetCurrentDal()
			{
				CurretDal = DbSession.LinksInfoDal;
			}
	}

		
	public partial class LoginLogService:BaseService<LoginLog>,ILoginLogService
	{
		public override void SetCurrentDal()
			{
				CurretDal = DbSession.LoginLogDal;
			}
	}

		
	public partial class OrderDetailInfoService:BaseService<OrderDetailInfo>,IOrderDetailInfoService
	{
		public override void SetCurrentDal()
			{
				CurretDal = DbSession.OrderDetailInfoDal;
			}
	}

		
	public partial class OrderInfoService:BaseService<OrderInfo>,IOrderInfoService
	{
		public override void SetCurrentDal()
			{
				CurretDal = DbSession.OrderInfoDal;
			}
	}

		
	public partial class PaymentService:BaseService<Payment>,IPaymentService
	{
		public override void SetCurrentDal()
			{
				CurretDal = DbSession.PaymentDal;
			}
	}

		
	public partial class RoleInfoService:BaseService<RoleInfo>,IRoleInfoService
	{
		public override void SetCurrentDal()
			{
				CurretDal = DbSession.RoleInfoDal;
			}
	}

		
	public partial class SysLogService:BaseService<SysLog>,ISysLogService
	{
		public override void SetCurrentDal()
			{
				CurretDal = DbSession.SysLogDal;
			}
	}

		
	public partial class UserInfoService:BaseService<UserInfo>,IUserInfoService
	{
		public override void SetCurrentDal()
			{
				CurretDal = DbSession.UserInfoDal;
			}
	}

		
	public partial class WFInstanceService:BaseService<WFInstance>,IWFInstanceService
	{
		public override void SetCurrentDal()
			{
				CurretDal = DbSession.WFInstanceDal;
			}
	}

		
	public partial class WFStepService:BaseService<WFStep>,IWFStepService
	{
		public override void SetCurrentDal()
			{
				CurretDal = DbSession.WFStepDal;
			}
	}

		
	public partial class WFTempService:BaseService<WFTemp>,IWFTempService
	{
		public override void SetCurrentDal()
			{
				CurretDal = DbSession.WFTempDal;
			}
	}

	
}