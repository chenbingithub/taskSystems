using HNCJ.IBLL;
using HNCJ.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HNCJ.BLL
{
    public partial class AddressInfoService : BaseService<AddressInfo>, IAddressInfoService
    {
        public void UpdateIsDefault(int id=0) {
           AddressInfo actio=DbSession.AddressInfoDal.GetEntity(u =>u.DelFlag==true&&u.IsDefault==true).FirstOrDefault();
           if (actio == null) return;
           if (actio.ID == id) return;
           actio.IsDefault = false;
           DbSession.AddressInfoDal.Update(actio);
           DbSession.SaveChanges();
        }
    }
}
