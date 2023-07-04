using Discount.dll.Models.Dtos;
using Discount.dll.Models.Infra.EFRepositories;
using Discount.dll.Models.Interfaces;
using Discount.dll.Models.Services;
using Discount.dll.Models.ViewModels;
using EFModels.EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace flexbackend.site.Controllers
{
    public class DiscountController : Controller
    {
        private IDiscountRepository _repo;
        private DiscountService _service;
        public DiscountController()
        {
            _repo = new DiscountEFRepository();
            _service = new DiscountService(_repo);

        }

        // GET: Discount
        public ActionResult Index(bool searchExpired = false, string searchDiscountName = null)
        {
            ViewBag.searchExpired = searchExpired;
            ViewBag.searchDiscountName = searchDiscountName;

            return View();
        }
        [HttpPost]
        public ActionResult GetDatas(bool searchExpired = false, string searchDiscountName = null)
        {
            return Json(GetDiscounts(searchExpired, searchDiscountName));
        }

        public IEnumerable<DiscountIndexVM> GetDiscounts(bool searchExpired = false, string searchDiscountName = null)
        {
            return _service.GetDiscounts(searchExpired, searchDiscountName).Select(x => x.ToIndexVM());
        }
        public ActionResult CreateOrEdit(int? id)
        {
            List<SelectListItem> projectTagitems = new List<SelectListItem>
            {
                new SelectListItem { Value = null , Text = "所有商品" },
            };
            var db = new AppDbContext();
            projectTagitems.AddRange(db.ProjectTags.Where(p => p.Status == true)
                                .Select(p => new SelectListItem { Value = p.ProjectTagId.ToString(), Text = p.ProjectTagName }));

            if (id == null || id == 0)
            {
                ViewBag.projectTagitems = new SelectList(projectTagitems, "Value", "Text", null);

            }

            return View();
        }
    }
}