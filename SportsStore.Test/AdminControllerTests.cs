using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using SportsStore.Controllers;
using SportsStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SportsStore.Test
{
    public class AdminControllerTests
    {
        [Fact]
        /// <summary>
        /// 测试Index是否正确返回存储库中的Product对象
        /// </summary>
        public void Can_Filter_Products()
        {
            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns((new Product[]
            {
                new Product { ProductID = 1, Name ="n1",Category="cat1"},
                new Product { ProductID = 2, Name ="n2",Category="cat1"},
                new Product { ProductID = 3, Name ="n3",Category="cat3"},
            }).AsQueryable());

            var controller = new AdminController(mock.Object);

            var result = GetViewModel<IEnumerable<Product>>(controller.Index()).ToArray();

            Assert.Equal(3, result.Length);
            Assert.True(result[0].Name == "n1" && result[0].Category == "cat1");
            Assert.True(result[1].Name == "n2" && result[1].Category == "cat1");
            Assert.True(result[2].Name == "n3" && result[2].Category == "cat3");
        }

        [Fact]
        /// <summary>
        /// 测试Edit根据Id是否得到正确数据
        /// </summary>
        public void Can_Edit_Products()
        {
            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns((new Product[]
            {
                new Product { ProductID = 1, Name ="n1",Category="cat1"},
                new Product { ProductID = 2, Name ="n2",Category="cat1"},
                new Product { ProductID = 3, Name ="n3",Category="cat3"},
            }).AsQueryable());

            var controller = new AdminController(mock.Object);
            var p1 = GetViewModel<Product>(controller.Edit(1));
            var p2 = GetViewModel<Product>(controller.Edit(2));
            var p3 = GetViewModel<Product>(controller.Edit(3));

            Assert.Equal(1, p1.ProductID);
            Assert.Equal(2, p2.ProductID);
            Assert.Equal(3, p3.ProductID);
        }

        [Fact]
        /// <summary>
        /// 测试Edit根据不存在的Id是否未得到商品
        /// 未得到商品为成功
        /// </summary>
        public void Can_Edit_Nonexistent_Products()
        {
            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns((new Product[]
            {
                new Product { ProductID = 1, Name ="n1",Category="cat1"},
                new Product { ProductID = 2, Name ="n2",Category="cat1"},
                new Product { ProductID = 3, Name ="n3",Category="cat3"},
            }).AsQueryable());

            var controller = new AdminController(mock.Object);
            var p1 = GetViewModel<Product>(controller.Edit(4));

            Assert.Null(p1);
        }

        /// <summary>
        /// 测试用户填入的值是否保存成功
        /// </summary>
        [Fact]
        public void Can_Save_Vaild_Products()
        {
            var mock = new Mock<IProductRepository>();
            var tempData = new Mock<ITempDataDictionary>();

            var controller = new AdminController(mock.Object)
            {
                TempData = tempData.Object,
            };
            var pro = new Product() { Name = "张三Test" };
            IActionResult result = controller.Edit(pro);
            mock.Verify(m => m.SaveProduct(pro));

            //判断result是否为RedirectToActionResult类型
            Assert.IsType<RedirectToActionResult>(result);
            //验证跳转的控制器名
            Assert.Equal("Index", (result as RedirectToActionResult).ActionName);
        }

        /// <summary>
        /// 测试用户填入的值是否保存成功
        /// </summary>
        [Fact]
        public void Can_Save_Invalid_Products()
        {
            var mock = new Mock<IProductRepository>();
            var tempData = new Mock<ITempDataDictionary>();

            var controller = new AdminController(mock.Object);

            var pro = new Product() { Name = "张三Test" };

            controller.ModelState.AddModelError("error", "error");
            IActionResult result = controller.Edit(pro);
            mock.Verify(m => m.SaveProduct(It.IsAny<Product>()), Times.Never());

            //判断result是否为RedirectToActionResult类型
            Assert.IsType<ViewResult>(result);
        }

        /// <summary>
        /// 是否删除成功
        /// </summary>
        [Fact]
        public void Can_Delete_Valid_Products()
        {
            Product product = new Product() { Name = "Test", ProductID = 2 };
            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product { ProductID = 1, Name ="n1",Category="cat1"},
                product,
                new Product { ProductID = 3, Name ="n2",Category="cat1"},
                new Product { ProductID = 4, Name ="n3",Category="cat3"},
            }.AsQueryable());
            var controller = new AdminController(mock.Object);

            controller.Delete(product.ProductID);

            mock.Verify(m => m.DeleteProduct(product.ProductID));
        }

        /// <summary>
        /// 获取Action的返回结果
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result"></param>
        /// <returns></returns>
        private T GetViewModel<T>(IActionResult result) where T : class
        {
            return (result as ViewResult)?.ViewData.Model as T;
        }

    }
}
