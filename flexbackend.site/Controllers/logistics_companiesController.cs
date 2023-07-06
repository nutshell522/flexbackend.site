using EFModels.EFModels;
using Flex.Products.dll.Models.ViewModel;
using Orders.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace flexbackend.site.Controllers
{
    public class logistics_companiesController : Controller
    {
        // GET: logistics_companies
        public ActionResult Logistics_companiesIndex()
        {
            IEnumerable<logistics_companiesVM> companies = Getcompanies();

            return View(companies);
        }

        private IEnumerable<logistics_companiesVM> Getcompanies()
        {
            var db = new AppDbContext();

            return db.logistics_companies
                .AsNoTracking()
                .ToList()
                .Select(p => new logistics_companiesVM
                {
                    Id = p.Id,
                    name = p.name,
                    tel = p.tel,
                    logistics_description = p.logistics_description,
                });
        }
    }
}