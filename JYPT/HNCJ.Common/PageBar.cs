using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HNCJ.Common
{
    public class PageBar
    {
        public static string GetPageBars(int pageIndex, int pageCount, int key,string value)
        {
            if (pageCount == 1)
            {
                return string.Empty;
            }
            int start = pageIndex - 5;//起始位置要求显示10个数字页码
            start = start < 1 ? 1 : start;
            int end = start + 9; //终止位置
            end = end > pageCount ? pageCount : end;
            StringBuilder sb = new StringBuilder();
            for (int i = start; i <= end; i++)
            {
                if (i == pageIndex)
                {
                    sb.Append(i);
                }
                else
                {
                    sb.Append(string.Format("<a href='?pageIndex={0}&typeId={1}&value={2}'>{0}</a>", i, key, value));
                }
            }
            return sb.ToString();
        }
        public static string GetPageBar(int pageIndex, int pageCount)
        {
            if (pageCount == 1)
            {
                return string.Empty;
            }
            int start = pageIndex - 5;//起始位置要求显示10个数字页码
            start = start < 1 ? 1 : start;
            int end = start + 9; //终止位置
            end = end > pageCount ? pageCount : end;
            StringBuilder sb = new StringBuilder();
            for (int i = start; i <= end; i++)
            {
                if (i == pageIndex)
                {
                    sb.Append(i);
                }
                else
                {
                    sb.Append(string.Format("<a href='?pageIndex={0}'>{0}</a>", i));
                }
            }
            return sb.ToString();
        }
    }
}
