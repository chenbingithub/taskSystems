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
    
    public partial class Dept
    {
        public Dept()
        {
            this.DelFlag = true;
            this.ParentId = -1;
            this.UserInfo = new HashSet<UserInfo>();
        }
    
        public int ID { get; set; }
        public string deptno { get; set; }
        public string deptname { get; set; }
        public string ParentName { get; set; }
        public string deptmanager { get; set; }
        public Nullable<int> deptmanager_id { get; set; }
        public string telphone { get; set; }
        public string remark { get; set; }
        public Nullable<bool> DelFlag { get; set; }
        public Nullable<System.DateTime> RegTime { get; set; }
        public Nullable<System.DateTime> ModfiedOn { get; set; }
        public Nullable<int> ParentId { get; set; }
    
        public virtual ICollection<UserInfo> UserInfo { get; set; }
    }
}
