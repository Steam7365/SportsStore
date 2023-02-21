using System.Collections.Generic;
using System.Linq;

namespace SportsStore.Models
{
    public class CartLine
    {
        public int CartLineId { get; set; }
        /// <summary>
        /// 商品
        /// </summary>
        public Product Product { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int Quantity { get; set; }
    }

    public class Cart
    {
        //存储所有购物车信息
        private List<CartLine> lineCollection = new List<CartLine>();

        /// <summary>
        /// 添加购物车
        /// </summary>
        /// <param name="product"></param>
        /// <param name="quantity"></param>
        public virtual void AddItem(Product product, int quantity)
        {
            CartLine line = lineCollection.Where(p => p.Product.ProductID == product.ProductID)
                    .FirstOrDefault();
            //如果购物车中未找到此商品
            if (line == null)
            {
                lineCollection.Add(new CartLine
                {
                    Product = product,
                    Quantity = quantity
                });
            }
            //如果找到了
            else
            {
                line.Quantity += quantity;
            }
        }
        /// <summary>
        /// 删除购物车
        /// </summary>
        /// <param name="product"></param>
        public virtual void RemoeLine(Product product)
        {
            lineCollection.RemoveAll(x => x.Product.ProductID == product.ProductID);
        }

        /// <summary>
        /// 获取购物车内所有的商品价格
        /// </summary>
        /// <returns></returns>
        public virtual decimal ComputeTotalValue()
        {
            return lineCollection.Sum(x => x.Product.Price * x.Quantity);
        }

        /// <summary>
        /// 清空购物车
        /// </summary>
        public virtual void Clear()=>lineCollection.Clear();

        /// <summary>
        /// 存储所有购物车的购物信息
        /// </summary>
        public virtual IEnumerable<CartLine> Lines => lineCollection;
    }
}
