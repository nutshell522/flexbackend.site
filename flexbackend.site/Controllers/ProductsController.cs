using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
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
using Flex.Products.dll.Service;

namespace flexbackend.site.Controllers
{
    public class ProductsController : Controller
    {
        private AppDbContext db = new AppDbContext();

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
            ViewBag.fk_ProductSubCategoryId = new SelectList(db.ProductSubCategories, "ProductSubCategoryId", "SubCategoryPath");
            return View();
        }

        // POST: Products/Create  
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductId,ProductName,ProductDescription,ProductMaterial,ProductOrigin,UnitPrice,SalesPrice,StartTime,EndTime,LogOut,Tag,fk_ProductSubCategoryId,CreateTime,EditTime")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.fk_ProductSubCategoryId = new SelectList(db.ProductSubCategories, "ProductSubCategoryId", "SubCategoryPath", product.fk_ProductSubCategoryId);
            return View(product);
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
