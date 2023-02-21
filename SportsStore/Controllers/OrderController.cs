using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using System.Linq;

namespace SportsStore.Controllers
{
    public class OrderController : Controller
    {
        private IOrderRepository repository;
        private Cart cart;
        public OrderController(IOrderRepository repoService, Cart cartService)
        {
            repository = repoService;
            //cart的数据为Session["Cart"]的Json数据转换而来的
            cart = cartService;
        }

        /// <summary>
        /// 订单列表
        /// </summary>
        /// <returns></returns>
        /// 限制对该操作方法的访问
        [Authorize]
        public ViewResult List()
        {
            return View(repository.Orders.Where(o => !o.Shipped));
        }

        /// <summary>
        /// 标记该订单已发货
        /// 
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public IActionResult MarkShipped(int orderID)
        {
            //获取传输过来的订单
            Order order = repository.Orders.FirstOrDefault(o => o.OrderID == orderID);
            if (order != null)
            {
                order.Shipped = true;
                repository.SaveOrder(order);
            }
            //重定向订单列表
            return RedirectToAction(nameof(List));
        }

        /// <summary>
        /// 提交输入的地址信息
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            //判断购物车是否存在数据
            if (cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "对不起，你的购物车为空");
            }
            //判断输入的验证是否合格
            if (ModelState.IsValid)
            {
                order.Lines = cart.Lines.ToArray();
                repository.SaveOrder(order);
                return RedirectToAction(nameof(Completed));
            }
            //不合格重新输入
            else
            {
                return View(order);
            }
        }

        /// <summary>
        /// 结账后跳转感谢页面
        /// </summary>
        /// <returns></returns>
        public IActionResult Completed()
        {
            cart.Clear();
            return View();
        }

        /// <summary>
        /// 跳转到用户信息页面
        /// </summary>
        /// <returns></returns>
        public IActionResult Checkout()
        {
            return View(new Order());
        }
    }
}
