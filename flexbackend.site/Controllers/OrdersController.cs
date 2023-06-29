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
                    //fk_member_Id = p.fk_member_Id,
                    total_quantity = p.total_quantity,
                    //logistics_company_Id = p.logistics_company_Id,
                    //order_status_Id = p.order_status_Id,
                    //pay_method_Id = p.pay_method_Id,
                    //pay_status_Id = p.pay_status_Id,
                    coupon_name = p.coupon_name,
                    coupon_discount = p.coupon_discount,
                    freight = p.freight,
                    cellphone = p.cellphone,
                    receipt = p.receipt,
                    receiver = p.receiver,
                    recipient_address = p.recipient_address,
                    order_description = p.order_description,
                    //close_Id = p.close_Id,
                });
        }
    }
}