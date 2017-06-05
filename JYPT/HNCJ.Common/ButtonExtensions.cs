using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using HNCJ.Common;

namespace System
{

    public static class ButtonExtensions
    {
       
        /// <summary>
        /// 根据权限编码判断按钮是否显隐 chenbin 2017-5-16
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="code">权限编码</param>
        /// <param name="id"></param>
        /// <param name="href"></param>
        /// <param name="text">文字</param>
        /// <param name="htmlAttributes">html属性</param>
        /// <returns></returns>
        public static MvcHtmlString PermissionButton(this HtmlHelper helper, string code, string id, string href, string text, IDictionary<string, object> htmlAttributes)
        {
            StringBuilder sb = new StringBuilder();
            if (DoSetManage.IsIsAuthorized(code))
            {
                sb.AppendFormat("<a id=\"{0}\" href=\"{1}\"", id, href);
                foreach (var item in htmlAttributes)
                {
                    sb.AppendFormat(" {0}=\"{1}\"", item.Key, item.Value);
                }
                sb.AppendFormat(" >{0}</a>", text);
            }
            return new MvcHtmlString(sb.ToString());
        }
    }
}
