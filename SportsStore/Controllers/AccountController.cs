using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using SportsStore.Models.ViewModels;
using System.Threading.Tasks;

namespace SportsStore.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        //用户集合
        private UserManager<IdentityUser> userManager;
        //登录用户集合
        private SignInManager<IdentityUser> signInManager;
        public AccountController(UserManager<IdentityUser> userMgr, SignInManager<IdentityUser> signInMgr)
        {
            userManager = userMgr;
            signInManager = signInMgr;
            //填充种子数据
            IdentitySeedData.EnsurePopulated(userMgr).Wait();
        }

        /// <summary>
        /// 渲染Login页面
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        /// 指定该方法不需要授权验证，并可访问
        [AllowAnonymous]
        public ViewResult Login(string returnUrl)
        {
            return View(new LoginModel
            {
                ReturnUrl = returnUrl
            });
        }

        /// <summary>
        /// 提交Login表单
        /// 对用户进行身份验证
        /// </summary>
        /// <param name="loginModel"></param>
        /// <returns></returns>
        [HttpPost]
        //指定该方法不需要授权验证，并可访问
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            //先判断是否验证通过，如果通过判断是否存在用户，如果用户存在则判断密码是否正确
            if (ModelState.IsValid)
            {
                //是否能找到该用户
                IdentityUser user = await userManager.FindByNameAsync(loginModel.Name);
                if (user != null)
                {
                    //将当前登录用户注销
                    await signInManager.SignOutAsync();
                    //判断是否登录指定的用户和密码
                    if ((await signInManager.PasswordSignInAsync(user, loginModel.Password, false, false)).Succeeded)
                    {
                        //如果上一个页面为
                        return Redirect(loginModel?.ReturnUrl ?? "/Admin/Index");
                    }
                }
            }
            ModelState.AddModelError(string.Empty, "无效的名称或密码");
            return View(loginModel);
        }

        public async Task<RedirectResult> Logout(string returnUrl = "/")
        {
            await signInManager.SignOutAsync();
            return Redirect(returnUrl);
        }

    }
}
