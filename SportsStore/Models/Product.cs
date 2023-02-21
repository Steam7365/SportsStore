using System.ComponentModel.DataAnnotations;

namespace SportsStore.Models
{
    public class Product
    {
        public int ProductID { get; set; }

        [Required(ErrorMessage = "请输入商品名称")]
        public string Name { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        [Required(ErrorMessage = "请输入商品描述")]
        public string Description { get; set; }
        [Required(ErrorMessage = "请输入商品价格")]
        [Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }
        /// <summary>
        /// 类别
        /// </summary>
        [Required(ErrorMessage = "请输入商品类别")]
        public string Category { get; set; }
    }
}
