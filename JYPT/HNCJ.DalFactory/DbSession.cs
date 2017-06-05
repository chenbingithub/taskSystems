using HNCJ.DAL;
using HNCJ.IDAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
namespace HNCJ.DalFactory
{
    public class DbSession:IDbSession
    {
		
		public IActionInfoDal ActionInfoDal 
        { 
            get{
                return StaticDalFactory.GetActionInfoDal();
            } 
        }
        
 
	
		
		public IActionItemDal ActionItemDal 
        { 
            get{
                return StaticDalFactory.GetActionItemDal();
            } 
        }
        
 
	
		
		public IAddressInfoDal AddressInfoDal 
        { 
            get{
                return StaticDalFactory.GetAddressInfoDal();
            } 
        }
        
 
	
		
		public ICartInfoDal CartInfoDal 
        { 
            get{
                return StaticDalFactory.GetCartInfoDal();
            } 
        }
        
 
	
		
		public ICommentDal CommentDal 
        { 
            get{
                return StaticDalFactory.GetCommentDal();
            } 
        }
        
 
	
		
		public IDealsDal DealsDal 
        { 
            get{
                return StaticDalFactory.GetDealsDal();
            } 
        }
        
 
	
		
		public IDeptDal DeptDal 
        { 
            get{
                return StaticDalFactory.GetDeptDal();
            } 
        }
        
 
	
		
		public IFileInfoDal FileInfoDal 
        { 
            get{
                return StaticDalFactory.GetFileInfoDal();
            } 
        }
        
 
	
		
		public IFocusPictureDal FocusPictureDal 
        { 
            get{
                return StaticDalFactory.GetFocusPictureDal();
            } 
        }
        
 
	
		
		public IFreightDal FreightDal 
        { 
            get{
                return StaticDalFactory.GetFreightDal();
            } 
        }
        
 
	
		
		public IGoodsInfoDal GoodsInfoDal 
        { 
            get{
                return StaticDalFactory.GetGoodsInfoDal();
            } 
        }
        
 
	
		
		public IGoodsTypeDal GoodsTypeDal 
        { 
            get{
                return StaticDalFactory.GetGoodsTypeDal();
            } 
        }
        
 
	
		
		public ILinksInfoDal LinksInfoDal 
        { 
            get{
                return StaticDalFactory.GetLinksInfoDal();
            } 
        }
        
 
	
		
		public ILoginLogDal LoginLogDal 
        { 
            get{
                return StaticDalFactory.GetLoginLogDal();
            } 
        }
        
 
	
		
		public IOrderDetailInfoDal OrderDetailInfoDal 
        { 
            get{
                return StaticDalFactory.GetOrderDetailInfoDal();
            } 
        }
        
 
	
		
		public IOrderInfoDal OrderInfoDal 
        { 
            get{
                return StaticDalFactory.GetOrderInfoDal();
            } 
        }
        
 
	
		
		public IPaymentDal PaymentDal 
        { 
            get{
                return StaticDalFactory.GetPaymentDal();
            } 
        }
        
 
	
		
		public IRoleInfoDal RoleInfoDal 
        { 
            get{
                return StaticDalFactory.GetRoleInfoDal();
            } 
        }
        
 
	
		
		public ISysLogDal SysLogDal 
        { 
            get{
                return StaticDalFactory.GetSysLogDal();
            } 
        }
        
 
	
		
		public IUserInfoDal UserInfoDal 
        { 
            get{
                return StaticDalFactory.GetUserInfoDal();
            } 
        }
        
 
	
		
		public IWFInstanceDal WFInstanceDal 
        { 
            get{
                return StaticDalFactory.GetWFInstanceDal();
            } 
        }
        
 
	
		
		public IWFStepDal WFStepDal 
        { 
            get{
                return StaticDalFactory.GetWFStepDal();
            } 
        }
        
 
	
		
		public IWFTempDal WFTempDal 
        { 
            get{
                return StaticDalFactory.GetWFTempDal();
            } 
        }
        
 
	
		/// <summary>
        /// 向数据库提交的方法
        /// </summary>
        /// <returns></returns>
        public int SaveChanges(){
          return DbContextFactory.GetCurrentDbContext().SaveChanges();
        }
	}

}