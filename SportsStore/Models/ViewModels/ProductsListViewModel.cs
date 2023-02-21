using System.Collections.Generic;

namespace SportsStore.Models.ViewModels
{
    public class ProductsListViewModel
    {
        /// <summary>
        /// 商品集合
        /// </summary>
        public IEnumerable<Product> Products { get; set; }
        /// <summary>
        /// 分页信息
        /// </summary>

        public PagingInfo PagingInfo { get; set; }

        /// <summary>
        /// 选择的商品类别
        /// </summary>
        public string CurrentCategory { get; set; }
    }
}
