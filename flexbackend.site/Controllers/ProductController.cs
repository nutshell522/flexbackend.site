using EFModels.EFModels;
using Flex.Products.dll.Models.Dtos;
using Flex.Products.dll.Models.Infra.EFRepository;
using Flex.Products.dll.Models.Infra.Exts;
using Flex.Products.dll.Models.Interface;
using Flex.Products.dll.Models.Service;
using Flex.Products.dll.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace flexbackend.site.Controllers
{
    public class ProductController : Controller
    {
		private AppDbContext _db=new AppDbContext();
        // GET: Product
        public ActionResult Index()
        {
			var products = _db.Products.Include(p => p.ProductSubCategory);
            return View();
        }

		public ActionResult Create()
		{
			//PrepareProductSubCategoty(null);
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(ProductCreateVM vm)
		{
			if(ModelState.IsValid==false)return View(vm);
			Result result = CreateProduct(vm);

			if (result.IsSucces)
			{
				return RedirectToAction("Index");
			}
			else
			{
				ModelState.AddModelError(string.Empty, result.ErroeMessage);
				return View(vm);
			}
		}

		private Result CreateProduct(ProductCreateVM vm)
		{
			IProductRepository repo=new ProductEFRepository();
			var service = new ProductService(repo);
			return service.CreateProduct(vm.ToDto());
		}

		//private void PrepareProductSubCategoty(int? productSubCategoryId)
		//{
		//	var productSubCategories = _db.ProductSubCategories.ToList().Prepend(new ProductSubCategory());
		//	ViewBag.ProductSubCategories = new SelectList(productSubCategories, "ProductSubCategoryId", "SubCategoryPath", productSubCategoryId);
		//}
	}
}