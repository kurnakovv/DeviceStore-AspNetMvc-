using DeviceStore.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace DeviceStore.WebUI.HtmlHelpers
{
    public static class PagingHelper
    {
        public static MvcHtmlString GeneratesHtmlForPageLinks(this HtmlHelper html,
                                              PagingInfo pagingInfo,
                                              Func<int, string> pageUrl)
        {
            StringBuilder result = new StringBuilder();
            for(int i = 1; i <= pagingInfo.TotalPages; i++)
            {
                TagBuilder tagBuilder = new TagBuilder("a");
                tagBuilder.MergeAttribute("href", pageUrl(i));
                tagBuilder.InnerHtml = i.ToString();
                if(i == pagingInfo.CurrentPage)
                {
                    tagBuilder.AddCssClass("selected");
                    tagBuilder.AddCssClass("btn btn-warning");
                }
                tagBuilder.AddCssClass("btn btn-default");
                result.Append(tagBuilder.ToString());                
            }

            return MvcHtmlString.Create(result.ToString());
        }
    }
}