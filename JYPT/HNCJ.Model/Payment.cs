//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace HNCJ.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Payment
    {
        public Payment()
        {
            this.DelFlag = true;
            this.Deals = new HashSet<Deals>();
        }
    
        public int ID { get; set; }
        public string PaymentName { get; set; }
        public Nullable<bool> DelFlag { get; set; }
        public Nullable<System.DateTime> RegTime { get; set; }
        public Nullable<System.DateTime> ModfiedOn { get; set; }
        public string APIURL { get; set; }
    
        public virtual ICollection<Deals> Deals { get; set; }
    }
}