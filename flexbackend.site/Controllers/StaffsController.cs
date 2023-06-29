using Discount.dll.Models.Infra;
using Members.dll.Models.Interfaces;
using Members.dll.Models.lnfra.DapperRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Members.dll.Models.Services;


namespace flexbackend.site.Controllers
{
    public class StaffsController : Controller
    {
		// 控制器，路由和請求處理get、post ; 畫面呈現

		public ActionResult StaffList()
        {
			IStaffRepository repo = new StaffDapperRepository();
            StaffService service = new StaffService(repo);
            return View(service.GetStaffs());
        }
        
    }
}