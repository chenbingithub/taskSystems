using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HNCJ.IDAL
{
    public interface IDbSession
    {
		IActionInfoDal ActionInfoDal { get; }
	

		IActionItemDal ActionItemDal { get; }
	

		IAddressInfoDal AddressInfoDal { get; }
	

		ICartInfoDal CartInfoDal { get; }
	

		ICommentDal CommentDal { get; }
	

		IDealsDal DealsDal { get; }
	

		IDeptDal DeptDal { get; }
	

		IFileInfoDal FileInfoDal { get; }
	

		IFocusPictureDal FocusPictureDal { get; }
	

		IFreightDal FreightDal { get; }
	

		IGoodsInfoDal GoodsInfoDal { get; }
	

		IGoodsTypeDal GoodsTypeDal { get; }
	

		ILinksInfoDal LinksInfoDal { get; }
	

		ILoginLogDal LoginLogDal { get; }
	

		IOrderDetailInfoDal OrderDetailInfoDal { get; }
	

		IOrderInfoDal OrderInfoDal { get; }
	

		IPaymentDal PaymentDal { get; }
	

		IRoleInfoDal RoleInfoDal { get; }
	

		ISysLogDal SysLogDal { get; }
	

		IUserInfoDal UserInfoDal { get; }
	

		IWFInstanceDal WFInstanceDal { get; }
	

		IWFStepDal WFStepDal { get; }
	

		IWFTempDal WFTempDal { get; }
	

		int SaveChanges();
	}
}