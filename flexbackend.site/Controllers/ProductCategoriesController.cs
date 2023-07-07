using EFModels.EFModels;
using Flex.Products.dll.Exts;
using Flex.Products.dll.Infra.DapperRepository;
using Flex.Products.dll.Interface;
using Flex.Products.dll.Models.Infra.Exts;
using Flex.Products.dll.Models.ViewModel;
using Flex.Products.dll.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace flexbackend.site.Controllers
{
	public class ProductCategoriesController : Controller
	{
		private ICategoryRepository _repo = new SalesCategotyDPRepository();
		private AppDbContext _db = new AppDbContext();

		// GET: ProductCategories
		public ActionResult SalesCategoryIndex()
		{
			var service = new CategoryService(_repo).Search();
			var vm = service.Select(s => s.ToIndexVM());
			if (Request.IsAjaxRequest())
			{
				return Json(vm);
			}
			return View(vm);
		}

		[HttpPost]
		public ActionResult ReLoadSalesCategoryIndex()
		{
			var service = new CategoryService(_repo).Search();
			var vm = service.Select(s => s.ToIndexVM());
			return Json(new { data = vm });
		}

		// GET: ProductCategories/CreateSalesCategory
		public ActionResult CreateSalesCategory()
		{
			var vm = new SalesCategoryCreateVM();
			return View(vm);
		}

		// POST: ProductCategories/CreateSalesCategory
		[HttpPost]
		public ActionResult CreateSalesCategory(SalesCategoryCreateVM vm)
		{
			if (ModelState.IsValid == false) return View(vm);

			var service = new CategoryService(_repo);
			Result result = service.CreateSalesCategory(vm.ToCreateDto());

			if (result.IsFailed)
			{
				ModelState.AddModelError("SalesCategoryName", result.ErroeMessage);
				return View(vm);
			}

			return RedirectToAction("SalesCategoryIndex");

		}

		// GET: ProductCategories/EditSalesCategory/
		public ActionResult EditSalesCategory(int salesCategoryId)
		{
			if (salesCategoryId == 0) return new HttpStatusCodeResult(HttpStatusCode.BadGateway);

			var service = new CategoryService(_repo);
			var vm = service.GetSalesCategoryById(salesCategoryId).ToEditVM();

			if (vm == null) return HttpNotFound();

			return View(vm);
		}

		//POST: ProductCategories/EditSalesCategory/
		[HttpPost]
		public ActionResult EditSalesCategory(SalesCategoryEditVM vm)
		{
			if (ModelState.IsValid == false) return View(vm);
			var service = new CategoryService(_repo);
			Result result = service.EditSalesCategory(vm.ToEditDto());

			if (result.IsFailed)
			{
				ModelState.AddModelError("SalesCategoryName", result.ErroeMessage);
				return View(vm);
			}

			return RedirectToAction("SalesCategoryIndex");
		}

		// GET: ProductCategories/Delete/5
		//public ActionResult Delete(int id)
		//{
		//	return View();
		//}

		// POST: ProductCategories/Delete/5
		[HttpPost]
		public ActionResult Delete(int salesCategoryId)
		{

			var service = new CategoryService(_repo);
			Result result = service.DeleteSalesCategory(salesCategoryId);

			if (result.IsSuccess)
			{
				return Json(new { success = true });
			}
			return Json(new { success = false,message=result.ErroeMessage });

		}
	}

}
