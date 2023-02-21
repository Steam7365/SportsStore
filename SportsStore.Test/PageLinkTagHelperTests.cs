using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Moq;
using SportsStore.Infrastructure;
using SportsStore.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace SportsStore.Test
{
public class PageLinkTagHelperTests
{
    [Fact]
    public void Can_Generate_Page_Links()
    {
        //自定义路径数据
        var urlHepler = new Mock<IUrlHelper>();
        urlHepler.SetupSequence(x => x.Action(It.IsAny<UrlActionContext>()))
            .Returns("Test/Page1")
            .Returns("Test/Page2")
            .Returns("Test/Page3");
        
        //用url工厂存储路径数据
        var urlHelperFactory = new Mock<IUrlHelperFactory>();
        urlHelperFactory.Setup(f => f.GetUrlHelper(It.IsAny<ActionContext>()))
            .Returns(urlHepler.Object);

        //初始化PageLinkTagHelper类
        PageLinkTagHelper helper = new PageLinkTagHelper(urlHelperFactory.Object)
        {
            PageModel = new PagingInfo
            {
                CurrentPage = 2,
                TotalItems = 28,
                ItemsPerPage = 10,
            },
            PageAction = "Test"
        };

        TagHelperContext ctx = new TagHelperContext(
            new TagHelperAttributeList(),
            new Dictionary<object, object>(),
            ""
            );
        var content = new Mock<TagHelperContent>();
        TagHelperOutput output = new TagHelperOutput("div",
                new TagHelperAttributeList(),
                (cache, encoder) => Task.FromResult(content.Object)
            );

        helper.Process(ctx, output);

        Assert.Equal(
            @"<a href=""Test/Page1"">1</a>" +
            @"<a href=""Test/Page2"">2</a>" +
            @"<a href=""Test/Page3"">3</a>",
            output.Content.GetContent()
            );
    }
}
}
