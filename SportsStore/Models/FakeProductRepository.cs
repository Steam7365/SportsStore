using System.Collections.Generic;
using System.Linq;

namespace SportsStore.Models
{
    /// <summary>
    /// 假数据
    /// </summary>
    public class FakeProductRepository
    {
        public IQueryable<Product> Products => new List<Product>()
        {
            new Product{Name = "小米",Price =1225},
            new Product{Name = "三星",Price =2235},
            new Product{Name = "可乐",Price =5},
            new Product{Name = "联想",Price =13425},
        }.AsQueryable();
    }
}
