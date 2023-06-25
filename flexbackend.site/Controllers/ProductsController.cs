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
using EFModels.EFModels;
using Flex.Products.dll.Exts;
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

			var products = new ProductEFRepository()
                .Search(criteria)
                .Select(p=>p.ToIndexVM());
			return View(products);
        }

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
            return View();
        }

        // POST: Products/Create  
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductCreateVM vm, HttpPostedFileBase[] CreateFile)
        {
			PrepareProductSubCategoryDataSource(vm.fk_ProductSubCategoryId);
			if (ModelState.IsValid == false) return View(vm);

            if (CreateFile[0]==null || CreateFile.Length < 1)
            {
				ModelState.AddModelError("ImgPaths", "請選擇檔案");
				return View(vm);
			}

			// 將CreateFile存檔，並取得最後存檔的名
			string path = Server.MapPath("/Public/Img");
			List<string> saveFileName = SaveFileName(path, CreateFile);
			if (saveFileName == null || saveFileName.Count == 0)
			{
				ModelState.AddModelError("ImgPaths", "請選擇檔案");
				return View(vm);
			}

			// 清空原有的 ImgPaths
			vm.ImgPaths = new List<string>();

			// 將檔案名稱加入 ImgPaths
			foreach (var fileName in saveFileName)
			{
				vm.ImgPaths.Add(fileName);
			}

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

		private List<string> SaveFileName(string path, IEnumerable<HttpPostedFileBase> createFile)
		{
			//沒上傳或是空值，就不處理;
			if (createFile == null || !createFile.Any()) return new List<string>();

			List<string> result = new List<string>();

			//允許的副檔名
			var allowExts = new string[] { ".jpg", ".jpeg", ".png", ".gif", ".tif" };

			foreach (var file in createFile)
			{
				if (file == null || file.ContentLength == 0) continue;

				//取得副檔名
				string ext = Path.GetExtension(file.FileName);

				//檢查副檔名是否在允許範圍，不允許就不處理
				if (!allowExts.Contains(ext.ToLower())) continue;

				//生成一個新的檔名
				string newFileName = Guid.NewGuid().ToString("N") + ext;

				//儲存檔案到硬碟
				file.SaveAs(Path.Combine(path, newFileName));

				//將檔案名稱加入結果清單
				result.Add(newFileName);
			}
			return result;
		}


		// GET: Products/Edit/5
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

	}
}
