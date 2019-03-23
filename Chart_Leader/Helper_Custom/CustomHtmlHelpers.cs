using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Chart_Leader.Helper_Custom
{
    public static class CustomHtmlHelpers
    {
        public static IHtmlString Image(this HtmlHelper helper, string src, string alter, object htmlAttributes)
        {
            // Create tag builder
            var builder = new TagBuilder("img");
            // Add attributes
          if(src!=null)  builder.MergeAttribute("src", VirtualPathUtility.ToAbsolute(src));
            //builder.MergeAttribute("src", src);
            builder.MergeAttribute("alt", alter);
            if (htmlAttributes != null)
                builder.MergeAttributes(new RouteValueDictionary(htmlAttributes));
            // Render tag
            return new MvcHtmlString(builder.ToString(TagRenderMode.SelfClosing));
        }
    }
}