using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HNCJ.Model
{
    public class GoodsList
    {
        public int ID { get; set; }
        public string TypeName { get; set; }
        public string Url { get; set; }
        public List<GoodsInfo> list { get; set; }
        public List<GoodsType> typelist { get; set; }
    }
}
