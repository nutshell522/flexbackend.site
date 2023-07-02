using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;
using Antlr.Runtime.Tree;
using EFModels.EFModels;
using Flex.Products.dll.Exts;
using Flex.Products.dll.Infra.EFRepository;
using Flex.Products.dll.Interface;
using Flex.Products.dll.Models.Dtos;
using Flex.Products.dll.Models.Infra.EFRepository;
using Flex.Products.dll.Models.Infra.Exts;
using Flex.Products.dll.Models.ViewModel;
using Flex.Products.dll.Service;


namespace flexbackend.site.Controllers
{
    public class ProductsController : Controller
    {
        private AppDbContext db = new AppDbContext();
        private IProductRepository _repo=new ProductEFRepository();

        // GET: Products
        public ActionResult Index(IndexSearchCriteria criteria)
        {
            criteria = criteria ?? new IndexSearchCriteria();
            PrepareProductSubCategoryDataSource(criteria.ProductSubCategoryId);

			ViewBag.Criteria = criteria;
            ViewBag.StatusOption = new SelectList(criteria.StatusOption);

			var service = new ProductService(_repo);
			var products = service.IndexProduct(criteria).Select(p => p.ToIndexVM());

			return View(products);

		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Index(List<ProductIndexVM> vm)
		{
			var dtos = new List<ProductDto>();
			foreach (var product in vm)
			{
				dtos.Add(product.ToDto());
			}
			var service = new ProductService(_repo);
			var products = service.EditProductsStatus(dtos);
			if (products.IsSucces)
			{
				return RedirectToAction("Index");
			}
			return View(products);

		}

		//      [HttpPost]
		//      [ValidateAntiForgeryToken]
		//      public JsonResult Index(List<ProductIdAndStatusVM> productIdAndStatus)
		//      {
		//          var dtos = new List<ProductDto>();
		//          foreach (var product in productIdAndStatus)
		//          {
		//              dtos.Add(product.ToDto());
		//          }
		//          var service = new ProductService(_repo);
		//          var products = service.EditProductsStatus(dtos);
		//	if (products.IsSucces)
		//	{
		//		return Json(new { success = true, message = "產品狀態更新成功" });
		//	}
		//	else
		//	{
		//		return Json(new { success = false, message = "產品狀態更新失敗" });
		//	}

		//}

		// GET: Products/Details/5
		public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
			PrepareProductSubCategoryDataSource(null);
            PrepareColorDataSource(null);
            PrepareSizeDataSource(null);
			var vm = new ProductCreateVM();
			return View(vm);
        }

        // POST: Products/Create  
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductCreateVM vm)
		{
			PrepareProductSubCategoryDataSource(vm.fk_ProductSubCategoryId);
            PrepareColorDataSource(null);
            PrepareSizeDataSource(null);
            if (ModelState.IsValid == false) return View(vm);

			#region 照片上傳
			var files = Request.Files;
            if (files == null || files.Count == 0)
            {
                ModelState.AddModelError("ImgPaths", "請選擇檔案");
                return View(vm);
            }

            // 設定照片路徑不存在就新增一個
            string path = Server.MapPath("~/Public/Img");
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);

            // 將CreateFile存檔，並取得最後存檔的名稱，副檔名可能不合規格，所以在做一次偵錯
            List<string> saveFileName = SaveFileName(path,files);
            if (saveFileName == null || saveFileName.Count == 0)
            {
                ModelState.AddModelError("ImgPaths", "請選擇檔案");
                return View(vm);
            }

            // 將雜湊後的檔名newFileName加到VM的ImgPaths
            foreach (var fileName in saveFileName)
            {
                vm.ImgPaths.Add(fileName);
            }
			#endregion

			var service = new ProductService(_repo);
            var result = service.CreateProduct(vm.ToCreateDto());
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

		public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.fk_ProductSubCategoryId = new SelectList(db.ProductSubCategories, "ProductSubCategoryId", "ProductSubCategoryName", product.fk_ProductSubCategoryId);
            return View(product);
        }

        // POST: Products/Edit/5
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductId,ProductName,ProductDescription,ProductMaterial,ProductOrigin,UnitPrice,SalesPrice,StartTime,EndTime,LogOut,Tag,fk_ProductSubCategoryId,CreateTime,EditTime")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.fk_ProductSubCategoryId = new SelectList(db.ProductSubCategories, "ProductSubCategoryId", "ProductSubCategoryName", product.fk_ProductSubCategoryId);
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
         protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private void PrepareProductSubCategoryDataSource(int? ProductSubCategoryId)
        {
            ViewBag.ProductSubCategoryId = new SelectList(
                new ProductSubCategoryRepository()
                .GetProductSubCategory()
                .Prepend(new ProductSubCategoryDto { ProductSubCategoryId=0,SubCategoryPath="請選擇分類"}), "ProductSubCategoryId", "SubCategoryPath", ProductSubCategoryId);
        }

        private void PrepareColorDataSource(int? ColorId)
        {
            ViewBag.Color = new SelectList(
                new ColorRepository()
                .GetColorCategory()
                .Prepend(new ColorDto { ColorId = 0, ColorName = "顏色" }), "ColorId", "ColorName", ColorId);
        }

        private void PrepareSizeDataSource(int? SizeId)
        {
            ViewBag.Size = new SelectList(
                new SizeRepository()
                .GetSizeCategory()
                .Prepend(new SizeDto { SizeId = 0, SizeName = "尺寸" }), "SizeId", "SizeName", SizeId);
        }
        /// <summary>
        /// 上傳多張照片
        /// </summary>
        /// <param name="path">照片存放路徑</param>
        /// <param name="files">照片</param>
        /// <returns></returns>
        private List<string> SaveFileName(string path, HttpFileCollectionBase files)
        {
            //沒上傳或是空值，就不處理;
            if (files == null || files.Count == 0) return new List<string>();

            List<string> result = new List<string>();

            //允許的副檔名
            var allowExts = new string[] { ".jpg", ".jpeg", ".png", ".gif", ".tif" };
            for (int i = 0; i < files.Count; i++)
            {
                var file = files[i];

                if (file == null || file.ContentLength == 0) continue;

                //取得副檔名
                string ext = System.IO.Path.GetExtension(file.FileName);

                //檢查副檔名是否在允許範圍，不允許就不處理
                if (!allowExts.Contains(ext.ToLower())) continue;

                //生成一個新的檔名
                string newFileName = $"{Guid.NewGuid().ToString("N")}{ext}";

                //儲存檔案
                file.SaveAs(System.IO.Path.Combine(path, newFileName));

                //將檔案名稱加入結果清單
                result.Add(newFileName);
            }
            return result;
        }


    }
}
