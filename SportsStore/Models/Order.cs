using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SportsStore.Models
{
    public class Order
    {
        [BindNever]
        public int OrderID { get; set; }

        [BindNever]
        public bool Shipped { get; set; }

        /// <summary>
        /// 外键
        /// </summary>
        [BindNever]
        public ICollection<CartLine> Lines { get; set; }

        [Required(ErrorMessage ="请输入姓名")]
        public string Name { get; set; }
        
        [Required(ErrorMessage ="请输入至少一个地址")]
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string Line3 { get; set; }

        [Required(ErrorMessage ="请说明你的状态信息")]
        public string State { get; set; }

        [Required(ErrorMessage ="请输入你的城市")]
        public string City { get; set; }

        /// <summary>
        /// 邮政编码
        /// </summary>
        public string Zip { get; set; }

        [Required(ErrorMessage ="请输入国家")]
        public string Country { get; set; }

        /// <summary>
        /// 是否包装
        /// </summary>
        public bool GiftWrap { get; set; }
    }
}
