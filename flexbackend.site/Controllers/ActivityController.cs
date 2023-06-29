using EFModels.EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Flex_Activity.dll.Models.Exts;

namespace flexbackend.site.Controllers
{
    public class ActivityController : Controller
    {
	
		private AppDbContext db = new AppDbContext();

		// GET: Activity
		public ActionResult Index()
		{
			var activities = db.Activities.ToList()
										  .Select(a => a.ToIndexVM());
			return View(activities);
		}

		//public ActionResult Create()
		//{

		//}
	}
}