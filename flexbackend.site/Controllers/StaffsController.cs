using Members.dll.Models.Interfaces;
using Members.dll.Models.lnfra.DapperRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Members.dll.Models.Services;
using Members.dll.Models.ViewsModels;
using Members.dll.Models.Exts;
using Members.dll.Models.Dtos;
using Members.dll.Models.lnfra;
using System.Web.ModelBinding;
using System.Web.UI.WebControls;
using EFModels.EFModels;
using System.Web.Security;
using Members.dll.Models.ViewsModels.Staff;
using System.Net;
using Microsoft.Ajax.Utilities;
using System.EnterpriseServices;

namespace flexbackend.site.Controllers
{
	public class StaffsController : Controller
	{
		// 控制器，路由和請求處理get、post ; 畫面呈現

		//Create 也要使用這支method但還沒用
		private StaffService GetStaffRepository()
		{
			IStaffRepository repo = new StaffDapperRepository();
			return new StaffService(repo);
		}
		//編輯員工資料
		public ActionResult EditStaff(int staffId)
		{
			if (staffId == 0)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			
			return View();
		}
		[HttpPost]
		public ActionResult EditStaff(EditStaffVM vm)
		{
			if (ModelState.IsValid == false) return View(vm);
			Result result = ResetStaff(vm);
			if (result.IsSuccess == false)
			{
				ModelState.AddModelError(string.Empty, result.ErrorMessage);
				return View(vm);
			}

			return RedirectToAction("GetStaffDetail");
		}

		private Result ResetStaff(EditStaffVM vm)
		{
			StaffService service = GetStaffRepository();
			var a = service.ResetStaff(vm.ToStaffEditDto());
			return Result.Success();
		}

		//檢視某筆員工
		public ActionResult GetStaffDetail(int staffId)
		{
			if (staffId == 0)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			StaffService service = GetStaffRepository();
			return View(service.GetStaffDetail(staffId).ToStaffDetailVM());
		}

		//刪除員工
		public ActionResult DeleteStaff(int staffId)
		{
			//如果staffId為空，返回404錯誤碼
			if (staffId == 0)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			StaffService service = GetStaffRepository();
			service.DeleteStaff(staffId);
			return RedirectToAction("StaffList");
		}

		//[HttpPost,ActionName("DeleteStaff")]
		//[ValidateAntiForgeryToken]
		//public ActionResult DeleteStaff2(int staffId)
		//{
		//	var db = new AppDbContext();
		//	//List<StaffsIndexVM> staffsIndexVM = new List<StaffsIndexVM>();
		//	var staff = db.Staffs.FirstOrDefault(s => s.StaffId == staffId);

		//	//for (int i = 0; i < staff) ; i++)
		//	//{
		//	//	staffsIndexVM.Add(staffsIndexVM[staffId]);
		//	//}

		//	StaffService service = GetStaffRepository();
		//	service.DeleteStaff(staffId);

		//	return new EmptyResult();
		//}

		public ActionResult EditPassword()
		{
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize]
		public ActionResult EditPassword(ForgetPasswordVM vm)
		{
			if (ModelState.IsValid == false) return View(vm);
			Result result = ResetPassword(vm);
			if (result.IsSuccess == false)
			{
				ModelState.AddModelError(string.Empty, result.ErrorMessage);
				return View(vm);
			}
			return RedirectToAction("Login");
		}

		//忘記密碼
		public ActionResult ForgetPassword()
		{
			return View();
		}
		[HttpPost]
		public ActionResult ForgetPassword(ForgetPasswordVM vm)
		{
			if (ModelState.IsValid == false) return View(vm);
			Result result = ResetPassword(vm);

			if (result.IsSuccess == false)
			{
				ModelState.AddModelError(string.Empty, result.ErrorMessage);
				return View(vm);
			}
			return RedirectToAction("Login");
		}

		private Result ResetPassword(ForgetPasswordVM vm)
		{
			StaffService service = GetStaffRepository();
			return service.ResetPassword(vm.ToForgetPasswordDto());
		}

		//Logout
		public ActionResult Logout()
		{
			Session.Abandon();
			FormsAuthentication.SignOut();
			return Redirect("/Staffs/Login");
		}

		//Login
		public ActionResult Login()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Login(LoginVM vm)
		{
			if (ModelState.IsValid == false) return View(vm);

			//對帳號密碼進行驗證
			Result result = ValidLogin(vm);

			//驗證失敗
			if (result.IsSuccess == false)
			{
				ModelState.AddModelError(string.Empty, result.ErrorMessage);
				return RedirectToAction("Home");
			}

			//是否記住登入成功的會員
			const bool remeberMe = false;

			//驗證成功，處理後續登入作業，並將密碼編碼之後加到 Cookie裡面
			(string returnUrl, HttpCookie cookie) processResult = ProcessLogin(vm.Account, remeberMe);
			Response.Cookies.Add(processResult.cookie);

			return Redirect(processResult.returnUrl);
		}

		private (string returnUrl, HttpCookie cookie) ProcessLogin(string account, bool rememberMe)
		{
			var roles = string.Empty; //如用到角色權限就填入

			//建立一張認證票
			var ticket =
				new FormsAuthenticationTicket(
					1,                          //版本別,沒有特別用處
					account,
					DateTime.Now,               //發行日
					DateTime.Now.AddDays(2),    //到期日
					rememberMe,                 //是否續存
					roles,                      //userdata
					"/"                         //cookie位置,通常都放在網站的根目錄
					);

			//將它加密,利用內建的表單認證類別呼叫編碼
			var value = FormsAuthentication.Encrypt(ticket);

			//存入cookie
			var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, value);

			//取得return url
			var url = FormsAuthentication.GetRedirectUrl(account, true); //第二個引數沒有用處

			return (url, cookie);
		}

		private Result ValidLogin(LoginVM vm)
		{
			var db = new AppDbContext();

			//根據帳號去找，並檢查帳號
			var staff = db.Staffs.FirstOrDefault(s => s.Account == vm.Account);
			if (staff == null) return Result.Fail("帳號或密碼有誤");

			//找到帳號，將密碼密碼進行雜湊
			//var salt = HashUtility.GetSalt();
			//var hashPrassword = HashUtility.ToHA256(vm.Password, salt);

			string password = vm.Password;

			//檢查密碼
			return string.CompareOrdinal(staff.Password, password) == 0
				? Result.Success()
				: Result.Fail("帳號或密碼有誤");
		}

		//Create
		public ActionResult CreateStaff()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken] //防止跨網站偽造請求的攻擊
		[Authorize]
		public ActionResult CreateStaff(StaffsCreateVM vm)
		{
			Session["Account"] = "Account";
			Session["Password"] = "Password";
			//判斷資料是否通過驗證
			//不通過驗證，再次返回 StaffsCreateVM 
			if (ModelState.IsValid == false) return View(vm);

			Result result = Create(vm);
			if (result.IsSuccess)
			{
				return View();//回到員工總覽
			}
			else
			{
				//通過驗證，將資料存到db
				ModelState.AddModelError(string.Empty, result.ErrorMessage);
				return RedirectToAction("StaffList");
			}
		}

		private Result Create(StaffsCreateVM vm)
		{
			StaffService service = GetStaffRepository();
			StaffsCreateDto dto = vm.ToStaffsCreateDto();
			dto.Mobile = "0921133312";
			//dto.Account = Session["Account"].ToString();
			//dto.Password = Session["Password"].ToString();
			dto.Account = "4561";
			dto.Password = "4561";
			dto.fk_PermissionsId = 3;
			dto.fk_TitleId = 2;
			dto.fk_DepartmentId = 3;
			return service.CreateStaff(dto);
		}

		//Read
		[Authorize]
		public ActionResult StaffList()
		{
			StaffService service = GetStaffRepository();
			return View(service.GetStaffs());//return View (model)
		}
	}
}