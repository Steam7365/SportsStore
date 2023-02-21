namespace SportsStore.Models.ViewModels
{
    public class CartIndexViewModel
    {
        /// <summary>
        /// 购物车的逻辑类
        /// </summary>
        public Cart Cart { get; set; }
       
        /// <summary>
        /// 上一次的链接
        /// </summary>
        public string ReturnUrl { get; set; }
    }
}
