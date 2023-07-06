using EFModels.EFModels;
using Members.dll.Models.Interfaces;
using Members.dll.Models.lnfra;
using Members.dll.Models.lnfra.EFRepositories;
using Members.dll.Models.Services;
using Members.dll.Models.Dtos;
using Members.dll.Models.Exts;
using Members.dll.Models.ViewsModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity.Core.Metadata.Edm;
using System.Web.Services.Description;
using System.Data.Entity;

namespace flexbackend.site.Controllers
{
	public class MembersController : Controller
	{
		//控制器，路由和請求處理get、post ; 畫面呈現

		private AppDbContext db =new AppDbContext();//初始化EF的DbContext物件
		

		//Update--會員資料管理
		public ActionResult EditMember()
		{
			return View();
		}

		//[HttpPost]
		//[ValidateAntiForgeryToken]
		//public ActionResult EditMembers(MembersEditVM vm)
		//{

		//	檢查Model Binding(輸入驗證與模型驗證)是否成功
		//	if (ModelState.IsValid == false) return View(vm);

		//	Result result = EditMember(vm);

		//	if (result.IsSuccess)
		//	{
		//		return RedirectToAction("MembersList");//返回會員名單總覽
		//	}
		//	else
		//	{
		//		ModelState.AddModelError(string.Empty, result.ErrorMessage);
		//		return View(vm);
		//	}
		//}

		//Read--會員名單總覽
		public ActionResult MembersList()
		{
			IMemberRepository repo = new MemberEFRepository();
			MemberService service = new MemberService(repo);
			return View(service.MemberList());
		}

		//會員註冊


		public ActionResult Register()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken] //可以套至個別的Action、Controller、或是全域的Action Filter，套用之後接收到的Request如果不包含有效的antiforgery token，則會直接回應HTTP 400
		public ActionResult Register(RegisterVM vm)//控制器方法，傳入RegisterVM(用戶輸入)
		{
			if (ModelState.IsValid == false) return View(vm); //利用ModelState.IsValid來檢查Model Binding(輸入驗證與模型驗證)是否成功

			//建立新會員
			Result result = RegisterMember(vm); //模型綁定成功，呼叫RegisterMember 進行註冊 ，將註冊結果存在result

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

		
		private Result RegisterMember(RegisterVM vm)//回傳Result對象
		{
			// call Service object
			IMemberRepository repo = new MemberEFRepository();//創建一個介面接口repo，用EFRepo實例化(方便切換其他ORM工具)，為了訪問Db
			MemberService service = new MemberService(repo);//讓service使用repo進行會員註冊
			return service.Register(vm.ToDto());//vm.ToDto()方法將RegisterVM對象vm轉換為RegisterDto對象，並傳给service.Register()方法，進行商業邏輯判斷。
		}

		//public Result EditMember(MembersEditVM vm)
		//{
		//	IMemberRepository repo = new MemberEFRepository();
		//	MemberService service = new MemberService(repo);
		//	return service.EditMember(vm.ToMembersEditDto());
		//}


	}
}