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
    
    public partial class LinksInfo
    {
        public LinksInfo()
        {
            this.DelFlag = true;
        }
    
        public int ID { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public Nullable<bool> DelFlag { get; set; }
        public Nullable<System.DateTime> RegTime { get; set; }
        public Nullable<System.DateTime> ModfiedOn { get; set; }
    }
}
