using System.Linq;

namespace SportsStore.Models
{
    /// <summary>
    /// ApplicationDbContext的DbSet<Products>属性数据
    /// </summary>
    public class EFProductRepository : IProductRepository
    {
        private ApplicationDbContext _context;
        public EFProductRepository(ApplicationDbContext ctx)
        {
            _context = ctx;
        }
        public IQueryable<Product> Products => _context.Products;

        /// <summary>
        /// 删除商品信息
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public Product DeleteProduct(int productID)
        {
            var dbEntry = _context.Products.FirstOrDefault(p => p.ProductID == productID);
            if (dbEntry != null)
            {
                _context.Products.Remove(dbEntry);
                _context.SaveChanges();
            }
            return dbEntry;
        }

        /// <summary>
        /// 保存Product数据
        /// </summary>
        /// <param name="product"></param>
        public void SaveProduct(Product product)
        {
            //新增的商品默认ID为0，因为不会进行操作新增商品ID的值
            if (product.ProductID == 0)
            {
                _context.Products.Add(product);
            }
            //修改数据
            else
            {
                var dbEntry = _context.Products.FirstOrDefault(x => x.ProductID == product.ProductID);
                if (dbEntry != null)
                {
                    dbEntry.Name = product.Name;
                    dbEntry.Description = product.Description;
                    dbEntry.Price = product.Price;
                    dbEntry.Category = product.Category;
                }
            }
            _context.SaveChanges();
        }
    }
}
