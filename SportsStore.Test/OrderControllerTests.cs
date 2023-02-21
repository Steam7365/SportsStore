using Microsoft.AspNetCore.Mvc;
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
    public class OrderControllerTests
    {
        [Fact]
        public void Cannot_Chekout_Empty_Cart()
        {
            var mock = new Mock<IOrderRepository>();
            Cart cart=new Cart();
            Order order=new Order();
            OrderController target = new OrderController(mock.Object,cart);
            var result = target.Checkout(order) as ViewResult;

            mock.Verify(m=>m.SaveOrder(It.IsAny<Order>()),Times.Never);

            Assert.True(string.IsNullOrEmpty(result.ViewName));

            Assert.False(result.ViewData.ModelState.IsValid);
        }

        [Fact]
        public void Cannot_Chekout_Invalid_ShippingDetails()
        {
            var mock = new Mock<IOrderRepository>();
            Cart cart = new Cart();
            cart.AddItem(new Product(), 1);
            OrderController target = new OrderController(mock.Object, cart);
            target.ModelState.AddModelError("error", "error");

            Order order = new Order();
            var result = target.Checkout(order) as ViewResult;

            mock.Verify(m => m.SaveOrder(It.IsAny<Order>()), Times.Never);

            Assert.True(string.IsNullOrEmpty(result.ViewName));

            Assert.False(result.ViewData.ModelState.IsValid);
        }

        [Fact]
        public void Cannot_Chekout_And_Submit_Order()
        {
            var mock = new Mock<IOrderRepository>();
            Cart cart = new Cart();
            cart.AddItem(new Product(), 1);
            Order order = new Order();
            OrderController target = new OrderController(mock.Object, cart);
            var result = target.Checkout(order) as RedirectToActionResult;

            mock.Verify(m => m.SaveOrder(It.IsAny<Order>()), Times.Once);

            Assert.Equal("Completed", result.ActionName); ;
        }
    }
}
