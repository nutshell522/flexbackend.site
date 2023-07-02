using EFModels.EFModels;
using Orders.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace flexbackend.site.Controllers
{
    public class OrdersController : Controller
    {
        // GET: Orders
        public ActionResult OrdersIndex()
        {
            IEnumerable<OrdersIndexVM> orders = GetOrders();
            return View(orders);
        }

        private IEnumerable<OrdersIndexVM> GetOrders()
        {
            var db = new AppDbContext();
            return db.orders
                .AsNoTracking()
                //.Include(p => p.Member1)
                //.OrderBy(p => p.Category.DisplayOrder)
                .ToList()
                .Select(p => new OrdersIndexVM
                {
                    Id = p.Id,
                    ordertime = p.ordertime,
                    fk_member_Id = p.fk_member_Id,
                    total_quantity = p.total_quantity,
                    logistics_company_Id = p.logistics_company_Id,
                    order_status_Id = p.order_status_Id,
                    pay_method_Id = p.pay_method_Id,
                    pay_status_Id = p.pay_status_Id,
                    coupon_name = p.coupon_name,
                    coupon_discount = p.coupon_discount,
                    freight = p.freight,
                    cellphone = p.cellphone,
                    receipt = p.receipt,
                    receiver = p.receiver,
                    recipient_address = p.recipient_address,
                    order_description = p.order_description,
                    close_Id = p.close_Id,
                });
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OrdersIndexVM vm)
        {
            if (ModelState.IsValid == false) return View(vm);

            (bool IsSuccess, string ErrorMessage) result = CreateOrders(vm);

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
         private (bool IsSuccess, string ErrorMessage) CreateOrders(OrdersIndexVM vm)
        {
            var db = new AppDbContext();

            var order = new order
            {
                Id = vm.Id,
                ordertime = DateTime.Now,
                fk_member_Id = 1,
                total_quantity = vm.total_quantity,
                logistics_company_Id = 1,
                order_status_Id = 1,
                pay_method_Id = 1,
                pay_status_Id = 1,
                coupon_name = vm.coupon_name,
                coupon_discount = vm.coupon_discount,
                freight = vm.freight,
                cellphone = vm.cellphone,
                receipt = vm.receipt,
                receiver = vm.receiver,
                recipient_address = vm.recipient_address,
                order_description = vm.order_description,
                close_Id = 1
            };
            db.orders.Add(order);
            db.SaveChanges();

            return (true, "");
        }
    }
    
}
