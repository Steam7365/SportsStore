
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.Routing;
using Moq;
using SportsStore.Components;
using SportsStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SportsStore.Test
{
    public class NavigationMenuViewComponentTests
    {
        /// <summary>
        /// 生成类别列表测试
        /// </summary>
        [Fact]
        public void Can_Select_Categories()
        {
            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new[]
            {
                new Product { ProductID = 1, Name = "x1",Category="电子产品"},
                new Product { ProductID = 2, Name = "x2",Category="生活用品"},
                new Product { ProductID = 3, Name = "x3",Category="文具"},
                new Product { ProductID = 4, Name = "x4",Category="电子产品"},
                new Product { ProductID = 5, Name = "x5",Category="电子产品"},
            }.AsQueryable());

            var target = new NavigationMenuViewComponent(mock.Object);
            var result = ((target.Invoke() as ViewViewComponentResult).ViewData.Model as IEnumerable<string>).ToArray();

            Assert.True(Enumerable.SequenceEqual(new string[] { "电子产品", "生活用品", "文具" }, result));
        }
        /// <summary>
        /// 测试所选的类型
        /// </summary>
        [Fact]
        public void Can_select_Categories()
        {
            string categoryToSelect = "电子产品";
            var mock = new Mock<IProductRepository>();
            mock.Setup(x => x.Products).Returns(new[]
            {
                new Product { ProductID = 1, Name ="g1",Category="电子产品"},
                new Product { ProductID = 2, Name ="g2",Category="生活用品"},
                new Product { ProductID = 3, Name ="g3",Category="玩具"},
            }.AsQueryable());
            var target = new NavigationMenuViewComponent(mock.Object);

            //设置视图上下文的选择的类别
            target.ViewComponentContext = new ViewComponentContext
            {
                ViewContext = new ViewContext { RouteData = new RouteData() }
            };
            target.RouteData.Values["category"] = categoryToSelect;

            //获取选择的类别
            string result = (string)(target.Invoke() as ViewViewComponentResult).ViewData["SelectedCategory"];

            //选择的和获取的是否一致
            //或者说预料的值与实际的值是否一致
            Assert.Equal(categoryToSelect, result);
        }
    }
}
