using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using System.Linq;

namespace SportsStore.Components
{
    /// <summary>
    /// 类别导航视图组件操作类
    /// </summary>
    public class NavigationMenuViewComponent : ViewComponent
    {
        private IProductRepository repository;

        public NavigationMenuViewComponent(IProductRepository repo)
        {
            repository = repo;
        }
        /// <summary>
        /// 返回时会在这几个路径下查找视图
        /// /Views/Product/Components/NavigationMenu/Default.cshtml
        /// /Views/Shared/Components/NavigationMenu/Default.cshtml
        /// /Pages/Shared/Components/NavigationMenu/Default.cshtml
        /// </summary>
        /// <returns></returns>
        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedCategory = RouteData.Values["category"];
            return View(repository.Products.Select(x => x.Category)
                .Distinct().OrderBy(x => x));
        }
    }
}
