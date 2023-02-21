using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using SportsStore.Infrastructure;
using System;
using Newtonsoft.Json;

namespace SportsStore.Models
{
    /// <summary>
    /// 使用Session的
    /// </summary>
    public class SessionCart : Cart
    {

        //禁止序列化
        [JsonIgnore]
        public ISession Session { get; set; }

        //public override IEnumerable<CartLine> Lines => base.Lines;

        public SessionCart()
        {

        }

        public static Cart GetCart(IServiceProvider service)
        {
            var session = service.GetRequiredService<IHttpContextAccessor>().HttpContext.Session;
            //获取Session[Cart]的值
            SessionCart cart = session?.GetJson<SessionCart>("Cart") ?? new SessionCart();
            cart.Session = session;
            return cart;
        }

        /// <summary>
        /// 添加购物车
        /// </summary>
        /// <param name="product"></param>
        /// <param name="quantity"></param>
        public override void AddItem(Product product, int quantity)
        {
            base.AddItem(product, quantity);
            //将当前对象SessionCart转换为json并保存Session会话中
            Session.SetJson("Cart", this);
        }

        /// <summary>
        /// 清空购物车
        /// </summary>
        public override void Clear()
        {
            base.Clear();
            Session.Remove("Cart");
        }
        /// <summary>
        /// 删除购物车
        /// </summary>
        /// <param name="product"></param>
        public override void RemoeLine(Product product)
        {
            base.RemoeLine(product);
            Session.SetJson("Cart", this);
        }
    }
}
