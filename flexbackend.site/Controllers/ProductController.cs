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

namespace flexbackend.site.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

		public ActionResult CreateProduct()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult ProductCreate(ProductCreateVM vm)
		{
			if(ModelState.IsValid==false)return View(vm);
			Result result = CreateProduct(vm);

			if (result.IsSucces)
			{
				return View(vm);
			}
			else
			{
				ModelState.AddModelError(string.Empty, result.ErroeMessage);
				return View("Index");
			}
		}

		private Result CreateProduct(ProductCreateVM vm)
		{
			IProductRepository repo=new ProductEFRepository();
			var service = new ProductService(repo);
			return service.CreateProduct(vm.ToDto());
		}

		private void PrepareProductSubCategoty(int? ProductSubCategoryId)
		{
			
		}
	}
}