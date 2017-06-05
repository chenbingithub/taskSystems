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
    
    public partial class RoleInfo
    {
        public RoleInfo()
        {
            this.State = true;
            this.DelFlag = true;
            this.UserInfo = new HashSet<UserInfo>();
            this.ActionInfo = new HashSet<ActionInfo>();
            this.ActionItem = new HashSet<ActionItem>();
        }
    
        public int ID { get; set; }
        public string RoleName { get; set; }
        public string RoleNo { get; set; }
        public Nullable<bool> State { get; set; }
        public string Description { get; set; }
        public Nullable<bool> DelFlag { get; set; }
        public Nullable<System.DateTime> RegTime { get; set; }
        public Nullable<System.DateTime> ModfiedOn { get; set; }
    
        public virtual ICollection<UserInfo> UserInfo { get; set; }
        public virtual ICollection<ActionInfo> ActionInfo { get; set; }
        public virtual ICollection<ActionItem> ActionItem { get; set; }
    }
}