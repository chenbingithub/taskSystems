using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HNCJ.Model
{
    public class BaseParam
    {
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public int Total { get; set; }
        public int Itemid { get; set; }
        public string KeyString { get; set; }
        public string SchName { get; set; }

    }
}
