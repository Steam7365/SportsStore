using System.Linq;

namespace SportsStore.Models
{
    public interface IOrderRepository
    {
        /// <summary>
        /// 所有订单信息
        /// </summary>
        IQueryable<Order> Orders { get; }
        /// <summary>
        /// 保存订单
        /// </summary>
        /// <param name="order"></param>
        void SaveOrder(Order order);
    }
}
