using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;

namespace SportsStore.Components
{
    /// <summary>
    /// 标题导航视图组件操作类
    /// </summary>
    public class CartSummaryViewComponent : ViewComponent
    {
        private Cart cart;
        public CartSummaryViewComponent(Cart cartService) => cart = cartService;

        /// <summary>
        /// 跳转到
        /// /Views/Product/Components/CartSummary/Default.cshtml
        /// /Views/Shared/Components/CartSummary/Default.cshtml
        /// /Pages/Shared/Components/CartSummary/Default.cshtml
        /// 其中之一的页面
        /// </summary>
        /// <returns></returns>
        public IViewComponentResult Invoke()
        {
            return View(cart);
        }
    }
}
