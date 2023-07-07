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
using Members.dll.Models.ViewsModels.Member;
using Members.dll.Models.Services;
using Members.dll.Models.Interfaces;
using Members.dll.Models.lnfra.DapperRepositories;
using Members.dll.Models.lnfra.EFRepositories;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using Members.dll.Models.Exts;

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
		public ActionResult Details(int? memberId)
		{
			if (memberId == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Member member = db.Members.Find(memberId);
			if (member == null)
			{
				return HttpNotFound();
			}

			MemberDetailVM vms = new MemberDetailVM
			{
				MemberId = member.MemberId,
				Name = member.Name,
				Age = member.Age,
				Gender = member.Gender,
				Mobile = member.Mobile,
				Email = member.Email,
				Birthday = member.Birthday,
				Registration = member.Registration,
				fk_LevelId = member.fk_LevelId,
				fk_BlackListId = member.fk_BlackListId
			};
			return View(vms);
		}


		// GET: Member/Create
		public ActionResult Create()
		{
			ViewBag.fk_BlackListId = new SelectList(db.BlackLists, "BlackListId", "Behavior");
			ViewBag.fk_LevelId = new SelectList(db.MembershipLevels, "LevelId", "LevelName");
			PrepareCategoryDataSource(null);

			return View();
		}

		private void PrepareCategoryDataSource(int? id)
		{
			ViewBag.LevelId = new SelectList(db.MembershipLevels, "LevelId", "LevelName", id);
			List<SelectListItem> gender = new List<SelectListItem>
			{
			new SelectListItem { Value = "true" , Text = "男" },
			new SelectListItem { Value = "false" , Text = "女" },
			};
			ViewBag.Gender = gender;
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
		public ActionResult Edit(int? memberId)
		{
			if (memberId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			MemberService service = GetMemberRepository();
			var member = service.GetMemberId(memberId).ToMembersEditVM();
			if (memberId == null) return HttpNotFound();
			return View(member);
		}

		private MemberService GetMemberRepository()
		{
			IMemberRepository repo = new MemberEFRepository();
			return new MemberService(repo);
		}

		// POST: Member/Edit/5
		// 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
		// 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(MemberEditVM vm)
		{
			ViewBag.fk_BlackListId = new SelectList(db.BlackLists, "BlackListId", "Behavior", vm.fk_BlackListId);
			ViewBag.fk_LevelId = new SelectList(db.MembershipLevels, "LevelId", "LevelName", vm.fk_LevelId);
			if (!ModelState.IsValid) return View(vm);

			MemberService service = GetMemberRepository();
			var result = service.EditMember(vm.ToMembersEditDto());
			if (result.IsSuccess)
			{
				return RedirectToAction("Index");
			}
			else
			{
				return View(vm);
			}
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
