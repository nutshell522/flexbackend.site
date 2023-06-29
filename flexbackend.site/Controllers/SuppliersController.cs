using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Customized_Shoes.dll.Models.ViewModels;
using EFModels.EFModels;

namespace flexbackend.site.Controllers
{
	public class SuppliersController : Controller
	{
		private AppDbContext db = new AppDbContext();

		// GET: Suppliers
		public ActionResult Index(SupplierCriteria criteria)
		{
			ViewBag.Criteria = criteria;

			var query = db.Suppliers;

			#region where
			if (string.IsNullOrEmpty(criteria.CompanyName) == false)
			{
				query = (DbSet<Supplier>)query.Where(p => p.SupplierCompanyName.Contains(criteria.CompanyName));
			}
			if (criteria.Id != null && criteria.Id.Value > 0)
			{
				query = (DbSet<Supplier>)query.Where(p => p.SupplierId == criteria.Id.Value);
			}
			if (string.IsNullOrEmpty(criteria.Supply_Material) == false)
			{
				query = (DbSet<Supplier>)query.Where(p => p.Supply_Material.Contains(criteria.Supply_Material));
			}

			#endregion

			var suppliers = query.ToList().Select(p => p.ToIndexVM());

			return View(suppliers);
		}

		// GET: Suppliers/Details/5
		public ActionResult Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			Supplier supplier = db.Suppliers.Find(id);

			if (supplier == null)
			{
				return HttpNotFound();
			}
			return View(supplier);
		}

		// GET: Suppliers/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: Suppliers/Create
		// 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
		// 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(SupplierCreateVM vm)
		{
			if (ModelState.IsValid)
			{
				Supplier supplier = vm.ToEntity();

				db.Suppliers.Add(supplier);
				db.SaveChanges();
				return RedirectToAction("Index");
			}

			return View(vm);
		}



		// GET: Suppliers/Edit/5
		public ActionResult Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Supplier supplier = db.Suppliers.Find(id);
			if (supplier == null)
			{
				return HttpNotFound();
			}
			return View(supplier);
		}

		// POST: Suppliers/Edit/5
		// 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
		// 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit([Bind(Include = "SupplierId,SupplierCompanyName,HasCertificate,Supply_Material,SupplierCompanyPhone,SupplierCompanyEmail,SupplierCompanyAddress,SupplierCompanyNumber,SupplierStartDate")] Supplier supplier)
		{
			if (ModelState.IsValid)
			{
				db.Entry(supplier).State = EntityState.Modified;
				db.SaveChanges();
				return RedirectToAction("Index");
			}
			return View(supplier);
		}


		// GET: Suppliers/Delete/5
		public ActionResult Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Supplier supplier = db.Suppliers.Find(id);
			if (supplier == null)
			{
				return HttpNotFound();
			}
			return View(supplier);
		}

		// POST: Suppliers/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id)
		{
			Supplier supplier = db.Suppliers.Find(id);
			db.Suppliers.Remove(supplier);
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
	}
}
