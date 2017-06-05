using HNCJ.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HNCJ.IBLL
{
    public partial interface IAddressInfoService : IBaseService<AddressInfo>
    {
        void UpdateIsDefault(int id = 0);
    }
}
