using Flex_Activity.dll.Infra.DapperRepositories;
using Flex_Activity.dll.Interface;
using Flex_Activity.dll.Models.Exts;
using Flex_Activity.dll.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace flexbackend.site.Controllers
{
    public class OneToOneReservationController : Controller
    {
        // GET: OneToOneReservation
        public ActionResult Index()
        {
            IReservationRepository repo = new ReservationDapperRepository();
            OneToOneReservationServices service = new OneToOneReservationServices(repo);

            var reservations =service.GetAll();
            var vm = reservations.Select(r => r.ToIndexVM());       
            
            return View(vm);
        }

        public ActionResult ReservationList(int id)
        {
			IReservationRepository repo = new ReservationDapperRepository();
			OneToOneReservationServices service = new OneToOneReservationServices(repo);

            var reservations = service.GetAll(id);
            var vm = reservations.Select(r => r.ToIndexVM());

            return View(vm);

		}
    }
}

