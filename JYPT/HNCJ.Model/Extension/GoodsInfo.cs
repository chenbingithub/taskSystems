using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace HNCJ.Model
{
    //让UserInfo共享了UserInfoValidate元数据
    [MetadataType(typeof(GoodsInfoValidate))]
    [Serializable]
    public partial class GoodsInfo
    {
    }

    public partial class GoodsInfoValidate
    {
        [StringLength(64)]
        [Required]
        public string GoodsName { get; set; }
        [Range(0,double.MaxValue)]
        [Required]
        public decimal? GoodsPrice { get; set; }
        [StringLength(256)]
        public string GoodsRemark { get; set; }
        [Range(0, double.MaxValue)]
        [Required]
        public int? GoodsNum { get; set; }
        [StringLength(256)]
        [Required]
        public string ImageMenu { get; set; }
        [StringLength(64)]
        [Required]
        public string GoodsAddress { get; set; }
        [Required]
        public bool? DelFlag { get; set; }
        [Required]
        public int? Status { get; set; }
        [Range(0, double.MaxValue)]
        [Required]
        public int Point { get; set; }
    }
}
