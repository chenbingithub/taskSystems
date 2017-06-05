using HNCJ.DAL;
using HNCJ.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace HNCJ.DalFactory
{
    public class StaticDalFactory
    {
       public static string assemblyName = System.Configuration.ConfigurationManager.AppSettings["DalAssemblyName"];
      
		 public static IActionInfoDal GetActionInfoDal()
        {
            return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".ActionInfoDal") as IActionInfoDal;
        }
	
	
		 public static IActionItemDal GetActionItemDal()
        {
            return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".ActionItemDal") as IActionItemDal;
        }
	
	
		 public static IAddressInfoDal GetAddressInfoDal()
        {
            return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".AddressInfoDal") as IAddressInfoDal;
        }
	
	
		 public static ICartInfoDal GetCartInfoDal()
        {
            return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".CartInfoDal") as ICartInfoDal;
        }
	
	
		 public static ICommentDal GetCommentDal()
        {
            return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".CommentDal") as ICommentDal;
        }
	
	
		 public static IDealsDal GetDealsDal()
        {
            return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".DealsDal") as IDealsDal;
        }
	
	
		 public static IDeptDal GetDeptDal()
        {
            return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".DeptDal") as IDeptDal;
        }
	
	
		 public static IFileInfoDal GetFileInfoDal()
        {
            return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".FileInfoDal") as IFileInfoDal;
        }
	
	
		 public static IFocusPictureDal GetFocusPictureDal()
        {
            return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".FocusPictureDal") as IFocusPictureDal;
        }
	
	
		 public static IFreightDal GetFreightDal()
        {
            return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".FreightDal") as IFreightDal;
        }
	
	
		 public static IGoodsInfoDal GetGoodsInfoDal()
        {
            return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".GoodsInfoDal") as IGoodsInfoDal;
        }
	
	
		 public static IGoodsTypeDal GetGoodsTypeDal()
        {
            return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".GoodsTypeDal") as IGoodsTypeDal;
        }
	
	
		 public static ILinksInfoDal GetLinksInfoDal()
        {
            return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".LinksInfoDal") as ILinksInfoDal;
        }
	
	
		 public static ILoginLogDal GetLoginLogDal()
        {
            return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".LoginLogDal") as ILoginLogDal;
        }
	
	
		 public static IOrderDetailInfoDal GetOrderDetailInfoDal()
        {
            return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".OrderDetailInfoDal") as IOrderDetailInfoDal;
        }
	
	
		 public static IOrderInfoDal GetOrderInfoDal()
        {
            return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".OrderInfoDal") as IOrderInfoDal;
        }
	
	
		 public static IPaymentDal GetPaymentDal()
        {
            return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".PaymentDal") as IPaymentDal;
        }
	
	
		 public static IRoleInfoDal GetRoleInfoDal()
        {
            return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".RoleInfoDal") as IRoleInfoDal;
        }
	
	
		 public static ISysLogDal GetSysLogDal()
        {
            return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".SysLogDal") as ISysLogDal;
        }
	
	
		 public static IUserInfoDal GetUserInfoDal()
        {
            return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".UserInfoDal") as IUserInfoDal;
        }
	
	
		 public static IWFInstanceDal GetWFInstanceDal()
        {
            return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".WFInstanceDal") as IWFInstanceDal;
        }
	
	
		 public static IWFStepDal GetWFStepDal()
        {
            return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".WFStepDal") as IWFStepDal;
        }
	
	
		 public static IWFTempDal GetWFTempDal()
        {
            return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".WFTempDal") as IWFTempDal;
        }
	
	
		
	}

}