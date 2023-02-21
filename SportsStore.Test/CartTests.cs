using SportsStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SportsStore.Test
{
    public class CartTests
    {
        /// <summary>
        /// 测试购物车无此商品的添加
        /// </summary>
        [Fact]
        public void Can_Add_New_Lines()
        {
            Product p1 = new Product { ProductID = 1, Name = "p1" };
            Product p2 = new Product { ProductID = 2, Name = "p2" };

            Cart target = new Cart();
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            CartLine[] results = target.Lines.ToArray();

            Assert.Equal(2, results.Length);
            Assert.Equal(p1, results[0].Product);
            Assert.Equal(p2, results[1].Product);
        }

        /// <summary>
        /// 测试购物车有此商品的添加
        /// </summary>
        [Fact]
        public void Can_Add_Quantity_For_Existing_Lines()
        {
            Product p1 = new Product { ProductID = 1, Name = "p1" };
            Product p2 = new Product { ProductID = 2, Name = "p2" };

            Cart target = new Cart();
            target.AddItem(p1, 5);
            target.AddItem(p2, 1);
            target.AddItem(p1, 10);
            CartLine[] results = target.Lines.ToArray();

            Assert.Equal(2, results.Length);
            Assert.Equal(15, results[0].Quantity);
            Assert.Equal(1, results[1].Quantity);
        }

        /// <summary>
        /// 测试删除购物车
        /// </summary>
        [Fact]
        public void Can_Remove_Line()
        {
            Product p1 = new Product { ProductID = 1, Name = "p1" };
            Product p2 = new Product { ProductID = 2, Name = "p2" };
            Product p3 = new Product { ProductID = 3, Name = "p3" };

            Cart target = new Cart();
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            target.AddItem(p3, 10);

            target.RemoeLine(p3);

            Assert.True(0 == target.Lines.Where(x => x.Product.ProductID == p3.ProductID).Count());
            Assert.Equal(2, target.Lines.Count());
        }
        /// <summary>
        /// 测试购物车里面所有商品的总价
        /// </summary>
        [Fact]
        public void Calculate_Cart_Total()
        {
            Product p1 = new Product { ProductID = 1, Name = "p1", Price = 40m };
            Product p2 = new Product { ProductID = 2, Name = "p2", Price = 30m };
            Product p3 = new Product { ProductID = 3, Name = "p3", Price = 70m };

            Cart target = new Cart();
            target.AddItem(p1, 1);
            target.AddItem(p2, 3);
            target.AddItem(p3, 2);

            decimal result = target.ComputeTotalValue();

            Assert.True(result == 270m);
        }

        /// <summary>
        /// 测试清空购物车
        /// </summary>
        [Fact]
        public void Can_Clear_Contents()
        {
            Product p1 = new Product { ProductID = 1, Name = "p1" };
            Product p2 = new Product { ProductID = 2, Name = "p2" };
            Product p3 = new Product { ProductID = 3, Name = "p3" };

            Cart target = new Cart();
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            target.AddItem(p3, 10);

            target.Clear();

            Assert.True(0 == target.Lines.Count());
        }
    }
}
