using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EFModels.EFModels;
using Members.dll.Models.Exts;
using Members.dll.Models.Interfaces;
using Members.dll.Models.lnfra.EFRepositories;
using Members.dll.Models.Services;
using Newtonsoft.Json;

namespace flexbackend.site.Controllers
{
    public class MembershipLevelsController : Controller
    {
        private MemberService GetMbRepository()
        {
            IMemberRepository repo = new MemberEFRepository();
            return new MemberService(repo);
        }
        private AppDbContext db = new AppDbContext();

		
		/// <summary>
		/// MembershipLevels
		/// </summary>
		/// <param name="levelId"></param>
		/// <returns></returns>
		public ActionResult Index()
        {
            if (ModelState.IsValid == false)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
            var members = db.MembershipLevels.Include(m => m.Members);
            MemberService service = GetMbRepository();

            var vms = service.GetMbLevels();

            return View(vms);
        }

		//public ActionResult GetLevels()
		//{
		//	List<MembershipLevel> mbrShiplevels = new List<MembershipLevel>();
		//	mbrShiplevels.Add(new MembershipLevel { LevelName = "text", Description = "text" });
		//	return Json(mbrShiplevels, JsonRequestBehavior.AllowGet);
		//}


		// GET: MembershipLevels/Details/5
		public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MembershipLevel membershipLevel = db.MembershipLevels.Find(id);
            if (membershipLevel == null)
            {
                return HttpNotFound();
            }
            return View(membershipLevel);
        }

        // GET: MembershipLevels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MembershipLevels/Create
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LevelId,LevelName,MinSpending,Discount,Description")] MembershipLevel membershipLevel)
        {
            if (ModelState.IsValid)
            {
                db.MembershipLevels.Add(membershipLevel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(membershipLevel);
        }

        // GET: MembershipLevels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MembershipLevel membershipLevel = db.MembershipLevels.Find(id);
            if (membershipLevel == null)
            {
                return HttpNotFound();
            }
            return View(membershipLevel);
        }

        // POST: MembershipLevels/Edit/5
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LevelId,LevelName,MinSpending,Discount,Description")] MembershipLevel membershipLevel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(membershipLevel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(membershipLevel);
        }

        // GET: MembershipLevels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MembershipLevel membershipLevel = db.MembershipLevels.Find(id);
            if (membershipLevel == null)
            {
                return HttpNotFound();
            }
            return View(membershipLevel);
        }

        // POST: MembershipLevels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MembershipLevel membershipLevel = db.MembershipLevels.Find(id);
            db.MembershipLevels.Remove(membershipLevel);
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
