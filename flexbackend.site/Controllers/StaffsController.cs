using Discount.dll.Models.Infra;
using Members.dll.Models.Interfaces;
using Members.dll.Models.lnfra.DapperRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Members.dll.Models.Services;
using Members.dll.Models.ViewsModels;

namespace flexbackend.site.Controllers
{
    public class StaffsController : Controller
    {
        // 控制器，路由和請求處理get、post ; 畫面呈現

        private  StaffService GetStaffRepository()
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
		public ActionResult CreateStaff(StaffsCreateVM vm)
		{
			StaffService service = GetStaffRepository();
			return View(service);
		}

		//Read
		public ActionResult StaffList()
        {
			StaffService service = GetStaffRepository();
			return View(service.GetStaffs());
        }
        
    }
}