using Microsoft.AspNetCore.Mvc;
using SportsStore.Infrastructure;
using SportsStore.Models;
using SportsStore.Models.ViewModels;
using System.Linq;

namespace SportsStore.Controllers
{
    public class CartController : Controller
    {
        private IProductRepository repository;
        private Cart cart;
        public CartController(IProductRepository repository, Cart cartService)
        {
            this.repository = repository;
            cart = cartService;
        }

        /// <summary>
        /// 添加购物车
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        public RedirectToActionResult AddToCart(int productId, string returnUrl)
        {
            Product product = repository.Products.FirstOrDefault(p => p.ProductID == productId);

            if (product != null)
            {
                //获取之前添加时的Session[Cart] Json数据 转换为实体Cart数据
                //Cart cart = GetCart();

                //将这次的CartLine数据添加Cart
                cart.AddItem(product, 1);

                //将这次与之前的Cart数据转换为Json 然后用Session保存起来
                //SaveCart(cart);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        /// <summary>
        /// 删除购物车
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        public RedirectToActionResult RomoveFromToCart(int productId, string returnUrl)
        {
            Product product = repository.Products.FirstOrDefault(p => p.ProductID == productId);

            if (product != null)
            {
                //Cart cart = GetCart();
                cart.RemoeLine(product);
                //SaveCart(cart);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        ///// <summary>
        ///// 获取Session[Cart]的Json转换的实体数据
        ///// </summary>
        ///// <returns>实体数据Cart</returns>
        //private Cart GetCart()
        //{
        //    Cart cart = HttpContext.Session.GetJson<Cart>("Cart") ?? new Cart();
        //    return cart;
        //}

        ///// <summary>
        ///// 将Cart的数据转换为Json保存至Seesion[Cart]中
        ///// </summary>
        //private void SaveCart(Cart cart)
        //{
        //    HttpContext.Session.SetJson("Cart", cart);
        //}

        /// <summary>
        /// 跳转到Cart的Index页面
        /// </summary>
        /// <param name="returnUrl">获取上一次的链接</param>
        /// <returns></returns>
        public IActionResult Index(string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                //Cart = GetCart(),
                Cart = cart,
                ReturnUrl = returnUrl
            });
        }
    }
}
