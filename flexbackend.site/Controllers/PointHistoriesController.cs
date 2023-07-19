using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using DocumentFormat.OpenXml.Spreadsheet;
using EFModels.EFModels;
using Members.dll.Models.Interfaces;
using Members.dll.Models.lnfra.EFRepositories;
using Members.dll.Models.Services;
using Members.dll.Models.ViewsModels;
using Members.dll.Models.ViewsModels.PointHistories;
using Members.dll.Models.Exts;
using Members.dll.Models.Dtos;
using DocumentFormat.OpenXml.Office2010.ExcelAc;

namespace flexbackend.site.Controllers
{
	public class PointHistoriesController : Controller
	{
		private PointHistoriesService GetHistoriesRepository()
		{
			IPointHistoriesRepository repo = new PointHistoriesEFRepository();
			return new PointHistoriesService(repo);
		}

		private AppDbContext db = new AppDbContext();

		// GET: PointHistories
		public ActionResult Index()
		{
			//var pointHistories = db.PointHistories.Include(p => p.Member).Include(p => p.order).Include(p => p.Type).Include(p => p.MemberPoint);

			//List<PointsHistoryIndexDto> pointHistories2 = (List<PointsHistoryIndexDto>)db.PointHistories.Include(p => p.Member).Include(p => p.order).Include(p => p.Type).Include(p => p.MemberPoint);

			List<PointsHistoryIndexDto> pointHistories3 = db.PointHistories
					.Include(p => p.Member)
					.Include(p => p.order)
					.Include(p => p.Type)
					.Include(p => p.MemberPoint)
					.Select(p => new PointsHistoryIndexDto
					{
						PointHistoryId = p.PointHistoryId,
						Name = p.Member.Name,
						Id = p.order.Id,//join的是null
						//TypeName = p.Type.TypeName,
						//GetOrDeduct = p.GetOrDeduct,
						//UsageAmount = p.UsageAmount,
						//ordertime = p.order.ordertime,
						//PointSubtotal = p.MemberPoint.PointSubtotal
					}).ToList();

			var vms = new List<IndexVM> { };

			foreach (var item in pointHistories3)
			{
				vms.Add(PointsHistoryExts.ToIndexVM(item));
			}

			return View(vms);
		}

		// GET: PointHistories/Details/5
		public ActionResult Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			PointHistory pointHistory = db.PointHistories.Find(id);
			if (pointHistory == null)
			{
				return HttpNotFound();
			}
			return View(pointHistory);
		}

		// GET: PointHistories/Create
		public ActionResult Create()
		{
			ViewBag.fk_MemberId = new SelectList(db.Members, "MemberId", "Name");
			ViewBag.fk_OrderId = new SelectList(db.orders, "Id", "coupon_name");
			ViewBag.fk_TypeId = new SelectList(db.Types, "TypeId", "TypeName");
			return View();
		}

		// POST: PointHistories/Create
		// 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
		// 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind(Include = "PointHistoryId,GetOrDeduct,UsageAmount,EffectiveDate,fk_MemberId,fk_OrderId,fk_TypeId")] PointHistory pointHistory)
		{
			if (ModelState.IsValid)
			{
				db.PointHistories.Add(pointHistory);
				db.SaveChanges();
				return RedirectToAction("Index");
			}

			ViewBag.fk_MemberId = new SelectList(db.Members, "MemberId", "Name", pointHistory.fk_MemberId);
			ViewBag.fk_OrderId = new SelectList(db.orders, "Id", "coupon_name", pointHistory.fk_OrderId);
			ViewBag.fk_TypeId = new SelectList(db.Types, "TypeId", "TypeName", pointHistory.fk_TypeId);
			return View(pointHistory);
		}

		// GET: PointHistories/Edit/5
		public ActionResult Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			PointHistory pointHistory = db.PointHistories.Find(id);
			if (pointHistory == null)
			{
				return HttpNotFound();
			}
			ViewBag.fk_MemberId = new SelectList(db.Members, "MemberId", "Name", pointHistory.fk_MemberId);
			ViewBag.fk_OrderId = new SelectList(db.orders, "Id", "coupon_name", pointHistory.fk_OrderId);
			ViewBag.fk_TypeId = new SelectList(db.Types, "TypeId", "TypeName", pointHistory.fk_TypeId);
			return View(pointHistory);
		}

		// POST: PointHistories/Edit/5
		// 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
		// 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit([Bind(Include = "PointHistoryId,GetOrDeduct,UsageAmount,EffectiveDate,fk_MemberId,fk_OrderId,fk_TypeId")] PointHistory pointHistory)
		{
			if (ModelState.IsValid)
			{
				db.Entry(pointHistory).State = EntityState.Modified;
				db.SaveChanges();
				return RedirectToAction("Index");
			}
			ViewBag.fk_MemberId = new SelectList(db.Members, "MemberId", "Name", pointHistory.fk_MemberId);
			ViewBag.fk_OrderId = new SelectList(db.orders, "Id", "coupon_name", pointHistory.fk_OrderId);
			ViewBag.fk_TypeId = new SelectList(db.Types, "TypeId", "TypeName", pointHistory.fk_TypeId);
			return View(pointHistory);
		}

		// GET: PointHistories/Delete/5
		public ActionResult Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			PointHistory pointHistory = db.PointHistories.Find(id);
			if (pointHistory == null)
			{
				return HttpNotFound();
			}
			return View(pointHistory);
		}

		// POST: PointHistories/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id)
		{
			PointHistory pointHistory = db.PointHistories.Find(id);
			db.PointHistories.Remove(pointHistory);
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
