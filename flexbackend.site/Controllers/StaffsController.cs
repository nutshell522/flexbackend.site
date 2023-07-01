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

namespace flexbackend.site.Controllers
{
	public class StaffsController : Controller
	{
		// 控制器，路由和請求處理get、post ; 畫面呈現

		private StaffService GetStaffRepository()
		{
			IStaffRepository repo = new StaffDapperRepository();
			return new StaffService(repo);
		}

		//Create
		public ActionResult CreateStaff()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken] //防止跨網站偽造請求的攻擊
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
				return View();
			}
			else
			{
				//通過驗證，將資料存到db
				ModelState.AddModelError(string.Empty, result.ErrorMessage);
				return View(vm);
			}
		}

		private Result Create(StaffsCreateVM vm)
		{
			IStaffRepository repo = new StaffDapperRepository();
			StaffService service = new StaffService(repo);
			StaffsCreateDto dto = vm.ToStaffsCreateDto();
			dto.Mobile = "0921787456";
			dto.Account = Session["Account"].ToString();
			dto.Password = Session["Password"].ToString();
			dto.fk_PermissionsId = 1;
			dto.fk_TitleId = 1;
			dto.fk_DepartmentId = 1;
			return service.CreateStaff(dto);
		}

		//Read
		public ActionResult StaffList()
		{
			StaffService service = GetStaffRepository();
			return View(service.GetStaffs());//return View (model)
		}

	}
}