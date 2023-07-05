using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EFModels.EFModels;

namespace flexbackend.site.Controllers
{
    public class MemberController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: Member
        public ActionResult Index()
        {
            var members = db.Members.Include(m => m.BlackList).Include(m => m.MembershipLevel);
            return View(members.ToList());
        }

        // GET: Member/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        // GET: Member/Create
        public ActionResult Create()
        {
            ViewBag.fk_BlackListId = new SelectList(db.BlackLists, "BlackListId", "Behavior");
            ViewBag.fk_LevelId = new SelectList(db.MembershipLevels, "LevelId", "LevelName");
            return View();
        }

        // POST: Member/Create
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MemberId,Name,Age,Gender,Mobile,Email,Birthday,CommonAddress,Account,EncryptedPassword,Registration,IsConfirmed,ConfirmCode,fk_LevelId,fk_BlackListId")] Member member)
        {
            if (ModelState.IsValid)
            {
                db.Members.Add(member);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.fk_BlackListId = new SelectList(db.BlackLists, "BlackListId", "Behavior", member.fk_BlackListId);
            ViewBag.fk_LevelId = new SelectList(db.MembershipLevels, "LevelId", "LevelName", member.fk_LevelId);
            return View(member);
        }

        // GET: Member/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            ViewBag.fk_BlackListId = new SelectList(db.BlackLists, "BlackListId", "Behavior", member.fk_BlackListId);
            ViewBag.fk_LevelId = new SelectList(db.MembershipLevels, "LevelId", "LevelName", member.fk_LevelId);
            return View(member);
        }

        // POST: Member/Edit/5
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MemberId,Name,Age,Gender,Mobile,Email,Birthday,CommonAddress,Account,EncryptedPassword,Registration,IsConfirmed,ConfirmCode,fk_LevelId,fk_BlackListId")] Member member)
        {
            if (ModelState.IsValid)
            {
                db.Entry(member).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.fk_BlackListId = new SelectList(db.BlackLists, "BlackListId", "Behavior", member.fk_BlackListId);
            ViewBag.fk_LevelId = new SelectList(db.MembershipLevels, "LevelId", "LevelName", member.fk_LevelId);
            return View(member);
        }

        // GET: Member/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        // POST: Member/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Member member = db.Members.Find(id);
            db.Members.Remove(member);
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
