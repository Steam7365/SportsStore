using System.Linq;

namespace SportsStore.Models
{
    public interface IProductRepository
    {
        /// <summary>
        /// 所有产品
        /// </summary>
        IQueryable<Product> Products { get; }
        /// <summary>
        /// 保存产品
        /// </summary>
        /// <param name="product"></param>
        void SaveProduct(Product product);
        /// <summary>
        /// 删除产品
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        Product DeleteProduct(int productID);
    }
}
