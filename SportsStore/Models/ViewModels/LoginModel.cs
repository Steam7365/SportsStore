using System.ComponentModel.DataAnnotations;

namespace SportsStore.Models.ViewModels
{
    public class LoginModel
    {
        [Required]//必须提供值
        public string Name { get; set; }//登录用户名

        [Required]
        //在Razor视图中的Input方法使用了asp-for属性时，
        //标签助手会将type属性设置为password。
        [UIHint("password")]
        public string Password { get; set; }//密码

        /// <summary>
        /// 上一个页面跳转过来的链接
        /// </summary>
        public string ReturnUrl { get; set; } = "/";
    }
}
