using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using System.Linq;

namespace SportsStore.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private IProductRepository repository;
        public AdminController(IProductRepository repository)
        {
            this.repository = repository;
        }
        /// <summary>
        /// 填充种子数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult SeedDatabase()
        {
            SeedData.EnsurePopulated(HttpContext.RequestServices);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Index()
        {
            return View(repository.Products);
        }

        /// <summary>
        /// 修改页面
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public IActionResult Edit(int productId)
        {
            return View(repository.Products.FirstOrDefault(p => p.ProductID == productId));
        }


        /// <summary>
        /// 提交修改
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Edit(Product product)
        {
            //判断用户修改的验证是否正确
            if (ModelState.IsValid)
            {
                //保存修改
                repository.SaveProduct(product);
                TempData["message"] = $"{product.Name} 已保存";
                return RedirectToAction("Index");
            }
            else
            {
                return View(product);
            }
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <returns></returns>
        public ViewResult Create() => View("Edit", new Product());


        /// <summary>
        /// 删除编号
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Delete(int productID)
        {
            Product deletedProduct = repository.DeleteProduct(productID);
            if (deletedProduct != null)
            {
                TempData["message"] = $"{deletedProduct.Name} 已删除";
            }
            return RedirectToAction("Index");
        }
    }
}
