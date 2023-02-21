using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using SportsStore.Models.ViewModels;
using System.Collections.Generic;

namespace SportsStore.Infrastructure
{
    /// <summary>
    /// 页面跳转帮助类
    /// </summary>
    [HtmlTargetElement("div", Attributes = "page-model")]
    public class PageLinkTagHelper : TagHelper
    {
        //Url帮助工厂
        private IUrlHelperFactory urlHerperFactory;
        public PageLinkTagHelper(IUrlHelperFactory helperFactory)
        {
            this.urlHerperFactory = helperFactory;
        }

        /// <summary>
        /// 视图上下文对象 html属性未绑定
        /// </summary>
        [ViewContext, HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }

        //PagingInfo对象
        public PagingInfo PageModel { get; set; }

        /// <summary>
        /// 页面Action方法名
        /// </summary>
        public string PageAction { get; set; }

        [HtmlAttributeName(DictionaryAttributePrefix = "page-url-")]
        public Dictionary<string, object> PageUrlValues { get; set; } = new Dictionary<string, object>();
        public bool PageClassesEnabled { get; set; } = false;
        public string PageClass { get; set; }
        public string PageClassNormal { get; set; }
        public string PageClassSelected { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            //创建Url帮助对象
            IUrlHelper urlHelper = urlHerperFactory.GetUrlHelper(ViewContext);
            //创建标签div
            TagBuilder result = new TagBuilder("div");
            //根据数据的总页数进行循环
            for (int i = 0; i < PageModel.TotalPages; i++)
            {
                //创建a标签
                var tag = new TagBuilder("a");

                //设置a标签的href属性
                //tag.Attributes["href"] = urlHelper.Action(PageAction, new { productPage = (i + 1) });

                //设置a标签的href属性
                //href="PageAction?productPage=(i + 1)"
                PageUrlValues["productPage"] = (i + 1);//当前页
                tag.Attributes["href"] = urlHelper.Action(PageAction, PageUrlValues);

                if (PageClassesEnabled)
                {
                    tag.AddCssClass(PageClass);
                    tag.AddCssClass((i + 1) == PageModel.CurrentPage ? PageClassSelected : PageClassNormal);
                }
                //给a标签添加内容
                tag.InnerHtml.Append((i + 1).ToString());
                //将a标签放入div标签中
                result.InnerHtml.AppendHtml(tag);
            }
            output.Content.AppendHtml(result.InnerHtml);
        }
    }
}
