using System;

namespace SportsStore.Models.ViewModels
{
    public class PagingInfo
    {
        /// <summary>
        /// 总数据
        /// </summary>
        public int TotalItems { get; set; }
        /// <summary>
        /// 每页数
        /// </summary>
        public int ItemsPerPage { get; set; }
        /// <summary>
        /// 当前页
        /// </summary>
        public int CurrentPage { get; set; }

        /// <summary>
        /// 总页数
        /// </summary>
        public int TotalPages=>(int)Math.Ceiling((decimal)TotalItems / ItemsPerPage);
    }
}
