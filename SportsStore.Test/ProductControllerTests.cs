using Microsoft.AspNetCore.Mvc;
using Moq;
using SportsStore.Controllers;
using SportsStore.Models;
using SportsStore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SportsStore.Test
{
    public class ProductControllerTests
    {
        /// <summary>
        /// 分页数据测试
        /// </summary>
        [Fact]
        public void Can_Paginate()
        {
            var mock = new Mock<IProductRepository>();
            //设置Products属性的值，最后转换为IQueryable类型
            mock.Setup(m => m.Products).Returns(new[]
            {
                new Product { ProductID = 1, Name = "p1"},
                new Product { ProductID = 2, Name = "p2"},
                new Product { ProductID = 3, Name = "p3"},
                new Product { ProductID = 4, Name = "p4"},
                new Product { ProductID = 5, Name = "p5"},
                new Product { ProductID = 6, Name = "p6"},
                new Product { ProductID = 7, Name = "p7"}
            }.AsQueryable());

            //将控制器的IProductRepository的值改为自定义的假数据
            var controller = new ProductController(mock.Object);

            //设置每页的数据为3
            controller.PageSize = 3;

            //调用List方法页数为2
            var result = (controller.List(null, 2) as ViewResult).ViewData.Model as ProductsListViewModel;

            Product[] proArray = result.Products.ToArray();

            //验证表达式是否为真。
            Assert.True(proArray.Length == 3);
            //比较数据
            Assert.Equal("p4", proArray[0].Name);
            Assert.Equal("p5", proArray[1].Name);
            Assert.Equal("p6", proArray[2].Name);
        }

        /// <summary>
        /// 分页测试
        /// </summary>
        [Fact]
        public void Can_Send_pagination_View_Model()
        {
            var mock = new Mock<IProductRepository>();
            //设置Products属性的值，最后转换为IQueryable类型
            mock.Setup(m => m.Products).Returns(new[]
            {
                new Product { ProductID = 1, Name = "p1"},
                new Product { ProductID = 2, Name = "p2"},
                new Product { ProductID = 3, Name = "p3"},
                new Product { ProductID = 4, Name = "p4"},
                new Product { ProductID = 5, Name = "p5"},
                new Product { ProductID = 6, Name = "p6"},
                new Product { ProductID = 7, Name = "p7"}
            }.AsQueryable());

            //将控制器的IProductRepository的值改为自定义的假数据
            var controller = new ProductController(mock.Object);

            //设置每页的数据为3
            controller.PageSize = 3;

            //调用List方法页数为2
            var result = (controller.List(null, 2) as ViewResult).ViewData.Model as ProductsListViewModel;

            Product[] proArray = result.Products.ToArray();

            PagingInfo pagingInfo = result.PagingInfo;
            Assert.Equal(2, pagingInfo.CurrentPage);
            Assert.Equal(3, pagingInfo.ItemsPerPage);
            Assert.Equal(3, pagingInfo.TotalPages);
            Assert.Equal(7, pagingInfo.TotalItems);
        }

        [Fact]
        /// <summary>
        /// List类别过滤
        /// </summary>
        public void Can_Filter_Products()
        {
            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns((new Product[]
            {
                new Product { ProductID = 1, Name ="n1",Category="cat1"},
                new Product { ProductID = 2, Name ="n2",Category="cat1"},
                new Product { ProductID = 3, Name ="n3",Category="cat3"},
                new Product { ProductID = 4, Name ="n4",Category="cat3"},
                new Product { ProductID = 5, Name ="n5",Category="cat2"},
                new Product { ProductID = 6, Name ="n6",Category="cat1"}
            }).AsQueryable());

            var controller = new ProductController(mock.Object);
            controller.PageSize = 3;

            var result = ((controller.List("cat1", 1) as ViewResult)
                .ViewData.Model as ProductsListViewModel).Products.ToArray();

            Assert.Equal(3, result.Length);
            Assert.True(result[0].Name == "n1" && result[0].Category == "cat1");
            Assert.True(result[1].Name == "n2" && result[1].Category == "cat1");
            Assert.True(result[2].Name == "n6" && result[2].Category == "cat1");
        }

        /// <summary>
        /// 测试特定类别的商品数量
        /// </summary>
        [Fact]
        public void Generate_Category_Specific_Product_Count()
        {
            var mock = new Mock<IProductRepository>();
            mock.Setup(x => x.Products).Returns(new[]
            {
                new Product { ProductID = 1, Name ="b1",Category="cat1"},
                new Product { ProductID = 2, Name ="b2",Category="cat2"},
                new Product { ProductID = 3, Name ="b3",Category="cat1"},
                new Product { ProductID = 4, Name ="b4",Category="cat3"},
                new Product { ProductID = 5, Name ="b5",Category="cat1"},
                new Product { ProductID = 6, Name ="b6",Category="cat3"},
                new Product { ProductID = 7, Name ="b7",Category="cat1"},
            }.AsQueryable());

            var target = new ProductController(mock.Object);
            Func<IActionResult, ProductsListViewModel> GetModel = result =>
             {
                 return (result as ViewResult).ViewData.Model as ProductsListViewModel;
             };

            int? res1 = GetModel(target.List("cat1"))?.PagingInfo.TotalItems;
            int? res2 = GetModel(target.List("cat2"))?.PagingInfo.TotalItems;
            int? res3 = GetModel(target.List("cat3"))?.PagingInfo.TotalItems;

            Assert.Equal(4, res1);
            Assert.Equal(1, res2);
            Assert.Equal(2, res3);
        }
    }
}
