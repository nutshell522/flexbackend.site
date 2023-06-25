using EFModels.EFModels;
using Members.dll.Models.Interfaces;
using Members.dll.Models.lnfra;
using Members.dll.Models.lnfra.EFRepositories;
using Members.dll.Models.Services;
using Members.dll.Models.Dtos;
using Members.dll.Models.ViewsModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace flexbackend.site.Controllers
{
	public class MembersController : Controller
	{
		private AppDbContext db =new AppDbContext();//初始化EF的DbContext物件
		
		//會員名單總覽
		public ActionResult MembersList()
		{
			var members = db.Members
			.ToList()
			.Select(m =>m.ToIndexVM());
			return View(members);
		}

		//會員註冊
		public ActionResult Register()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken] //可以套至個別的Action、Controller、或是全域的Action Filter，套用之後接收到的Request如果不包含有效的antiforgery token，則會直接回應HTTP 400
		public ActionResult Register(RegisterVM vm)
		{
			if (ModelState.IsValid == false) return View(vm); //利用ModelState.IsValid來檢查Model Binding(輸入驗證與模型驗證)是否成功

			//建立新會員
			Result result = RegisterMember(vm);

			if (result.IsSuccess)
			{
				return View("ConfirmRegister");
			}
			else
			{
				ModelState.AddModelError(string.Empty, result.ErrorMessage);
				return View(vm);
			}
		}

		//public ActionResult ActionRegister(int memberId,string confirmCode)
		//{ 
		//	Result result = ActiveMember( memberId, confirmCode);

		//	return View();
		//}

		//public ActionResult Login()
		//{
		//	return View();
		//}

		private Result RegisterMember(RegisterVM vm)
		{
			// call Service object
			IMemberRepository repo = new MemberEFRepository();
			MemberService service = new MemberService(repo);
			return service.Register(vm.ToDto());
		}
	}
}