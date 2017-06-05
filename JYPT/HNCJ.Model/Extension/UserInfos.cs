using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace HNCJ.Model
{
    //让UserInfo共享了UserInfoValidate元数据
    [MetadataType(typeof(UserInfoValidate))]
    [Serializable]
    public partial class UserInfo
    {
        public string RoleName { get; set; }
    }
    
    [Serializable]
    public partial class Dept { 
    
    }
    public partial class UserInfoValidate
    {
        [StringLength(16,ErrorMessage="来自伙伴类的元数据")]
        [Required(ErrorMessage="*****")]
        public string UserName { get; set; }
        public string salt { get; set; }
        [Required(ErrorMessage = "*****")]
        public string UserPwd { get; set; }
        public string Avatar { get; set; }
        public string NickName { get; set; }
    }
}
