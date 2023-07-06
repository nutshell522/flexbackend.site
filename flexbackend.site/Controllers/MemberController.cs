using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EFModels.EFModels;
using Members.dll.Models.ViewsModels;
using Members.dll.Models.lnfra;

namespace flexbackend.site.Controllers
{
	public class MemberController : Controller
	{
		private AppDbContext db = new AppDbContext();

		// GET: Member
		public ActionResult Index()
		{
			var members = db.Members.Include(m => m.BlackList).Include(m => m.MembershipLevel);
			var vms = members.ToList().Select(entity =>
			 new MembersIndexVM
			 {
				 MemberId = entity.MemberId,
				 Name = entity.Name,
				 Gender = entity.Gender,
				 Email = entity.Email,
				 LevelName = entity.MembershipLevel.LevelName,
				 PointSubtotal = entity.MemberPoints.Sum(p => p.PointSubtotal),
				 Registration = entity.Registration,
				 fk_BlackListId = entity.fk_BlackListId
			 });

			return View(vms);
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

		[Authorize]
		public ActionResult Edit()
		{
			var currentUserAccount = User.Identity.Name; 
			var model = GetMemberProfile(currentUserAccount);
			return View(model);
		}

		private MembersEditVM GetMemberProfile(string account)
		{
			var memberInDb = new AppDbContext().Members.FirstOrDefault(m => m.Account == account);
			return memberInDb == null
				? null
				: new MembersEditVM
				{
					Name = memberInDb.Name,
					Age = memberInDb.Age,
					Gender = memberInDb.Gender,
					Mobile = memberInDb.Mobile,
					Email = memberInDb.Email,
					Birthday = memberInDb.Birthday,
					fk_LevelId=memberInDb.fk_LevelId,
					fk_BlackListId= memberInDb.fk_LevelId,
				};
		}

		// POST: Member/Edit/5
		// 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
		// 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(MembersEditVM vm)
		{
			//ViewBag.fk_BlackListId = new SelectList(db.BlackLists, "BlackListId", "Behavior", member.fk_BlackListId);
			//ViewBag.fk_LevelId = new SelectList(db.MembershipLevels, "LevelId", "LevelName", member.fk_LevelId);

			Result updateResult = UpdateProfile(vm);

			if (updateResult.IsSuccess) return RedirectToAction("Index");

			ModelState.AddModelError(string.Empty, updateResult.ErrorMessage);
			return View(vm);
		}

		private Result UpdateProfile(MembersEditVM vm)
		{
			// 取得在db裡的原始記錄
			var db = new AppDbContext();

			var currentUserAccount = User.Identity.Name;
			var memberInDb = db.Members.FirstOrDefault(m => m.Account == currentUserAccount);
			if (memberInDb == null) return Result.Fail("找不到要修改的會員記錄");

			// 更新記錄
			memberInDb.Name = vm.Name;
			memberInDb.Gender = vm.Gender;
			memberInDb.Age = vm.Age;
			memberInDb.Mobile = vm.Mobile;
			memberInDb.Email = vm.Email;
			memberInDb.Birthday = vm.Birthday;
			memberInDb.fk_LevelId = vm.fk_LevelId;
			memberInDb.fk_BlackListId = vm.fk_BlackListId;

			db.SaveChanges();

			return Result.Success();
		}

		// POST: Member/Edit/5
		// 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
		// 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
		[HttpPost]
		[ValidateAntiForgeryToken]
		//public ActionResult Edit([Bind(Include = "MemberId,Name,Age,Gender,Mobile,Email,Birthday,CommonAddress,Account,EncryptedPassword,Registration,IsConfirmed,ConfirmCode,fk_LevelId,fk_BlackListId")] Member member)
		//{
		//	if (ModelState.IsValid)
		//	{
		//		db.Entry(member).State = EntityState.Modified;
		//		db.SaveChanges();
		//		return RedirectToAction("Index");
		//	}
		//	ViewBag.fk_BlackListId = new SelectList(db.BlackLists, "BlackListId", "Behavior", member.fk_BlackListId);
		//	ViewBag.fk_LevelId = new SelectList(db.MembershipLevels, "LevelId", "LevelName", member.fk_LevelId);
		//	return View(member);
		//}

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
