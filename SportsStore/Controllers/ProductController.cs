using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsStore.Models;
using SportsStore.Models.ViewModels;
using System.Linq;

namespace SportsStore.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository repository;
        public int PageSize = 4;
        public ProductController(IProductRepository repository)
        {
            this.repository = repository;
        }

        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 根据类型分页
        /// </summary>
        /// <param name="category"></param>
        /// <param name="productPage"></param>
        /// <returns></returns>
        public IActionResult List(string category, int productPage = 1)
        {
            //如果类别为空返回空，不为空，返回于该类别有关的产品信息
            IQueryable<Product> productAll = repository.Products.Where(product => category == null || product.Category == category);

            //将返回的产品信息进行分页
            IQueryable<Product> products = productAll
                .OrderBy(x => x.ProductID)
                .Skip((productPage - 1) * PageSize)
                .Take(PageSize);

            //记录分页的基本数据
            PagingInfo paging = new PagingInfo
            {
                CurrentPage = productPage,
                ItemsPerPage = PageSize,
                TotalItems = productAll.Count()

                //TotalItems = category == null ? productAll.Count() : productAll.Where(x => x.Category == category).Count(),
            };
            //分页
            return View(new ProductsListViewModel() { Products = products, PagingInfo = paging, CurrentCategory = category });

        }
    }
}
