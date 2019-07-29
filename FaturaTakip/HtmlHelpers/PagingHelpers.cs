using FaturaTakip.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace FaturaTakip.HtmlHelpers
{
    public static class PagingHelpers
    {
        public static MvcHtmlString PageLinks(this HtmlHelper helper, PagingInfo pagingInfo, Func<int, string> pageUrl)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 1; i <= pagingInfo.TotalPages; i++)
            {
                TagBuilder tagLink = new TagBuilder("li");
                tagLink.AddCssClass(i == pagingInfo.CurrentPage ? "page-item active " : "page-item");
                TagBuilder tag = new TagBuilder("a");
                tag.MergeAttribute("href", pageUrl(i));
                tag.InnerHtml = i.ToString();
                tag.AddCssClass("page-link");
                tagLink.InnerHtml = tag.ToString();
                builder.Append(tagLink.ToString());
            }
            return MvcHtmlString.Create(builder.ToString());
        }
    }
}