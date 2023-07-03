using EFModels.EFModels;
using Orders.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
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
			var orderItems = GetOrderItemsIndex();
			return db.orders
                .AsNoTracking()
				//.Include(p => p.orderItems)
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
					total_price = p.total_price,
					orderItems = orderItems.Where(o => o.order_Id == p.Id).ToList()
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
                return RedirectToAction("OrdersIndex");
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
                total_price = vm.total_price,
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
		public ActionResult Edit(int id)
		{
			var order = GetOrderById(id);

			if (order == null)
			{
				return HttpNotFound(); // 可以根據你的需求返回一個適當的錯誤頁面或訊息
			}

			var vm = new OrdersIndexVM
			{
				Id = order.Id,
				ordertime = order.ordertime,
				fk_member_Id = order.fk_member_Id,
				total_quantity = order.total_quantity,
				logistics_company_Id = order.logistics_company_Id,
				order_status_Id = order.order_status_Id,
				pay_method_Id = order.pay_method_Id,
				pay_status_Id = order.pay_status_Id,
				coupon_name = order.coupon_name,
				coupon_discount = order.coupon_discount,
				freight = order.freight,
				cellphone = order.cellphone,
				receipt = order.receipt,
				receiver = order.receiver,
				recipient_address = order.recipient_address,
				order_description = order.order_description,
				close_Id = order.close_Id,
				total_price = order.total_price,
		};

			return View(vm);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(OrdersIndexVM vm)
		{
			if (ModelState.IsValid == false)
			{
				return View(vm);
			}

			var result = UpdateOrder(vm);

			if (result.IsSuccess)
			{
				return RedirectToAction("OrdersIndex");
			}
			else
			{
				ModelState.AddModelError(string.Empty, result.ErrorMessage);
				return View(vm);
			}
		}

		private (bool IsSuccess, string ErrorMessage) UpdateOrder(OrdersIndexVM vm)
		{
			var db = new AppDbContext();

			var order = db.orders.FirstOrDefault(o => o.Id == vm.Id);

			if (order == null)
			{
				return (false, "找不到該訂單"); // 可以根據你的需求返回一個適當的錯誤訊息
			}

			// 更新訂單的相關屬性
			//order.ordertime = vm.ordertime;
			order.fk_member_Id = vm.fk_member_Id;
			order.total_quantity = vm.total_quantity;
			order.logistics_company_Id = vm.logistics_company_Id;
			order.order_status_Id = vm.order_status_Id;
			order.pay_method_Id = vm.pay_method_Id;
			order.pay_status_Id = vm.pay_status_Id;
			order.coupon_name = vm.coupon_name;
			order.coupon_discount = vm.coupon_discount;
			order.freight = vm.freight;
			order.cellphone = vm.cellphone;
			order.receipt = vm.receipt;
			order.receiver = vm.receiver;
			order.recipient_address = vm.recipient_address;
			order.order_description = vm.order_description;
			order.close_Id = vm.close_Id;
			order.total_price = vm.total_price;

			db.SaveChanges();

			return (true, "");
		}
		private order GetOrderById(int id)
		{
			var db = new AppDbContext();
			return db.orders.FirstOrDefault(o => o.Id == id);
		}
		//public ActionResult Editorder()
		//{
		//	var currentorder = User.Identity.Name;
		//	var model = Getorder(currentorder);
		//	return View(model);
		//}

		//private object Getorder(int currentorder)
		//{
		//	var orderInDb = new AppDbContext().orders.FirstOrDefault(m => m.Id == currentorder);
		//	return orderInDb == null
		//		? null
		//		: new OrdersIndexVM
		//		{
		//			Id = orderInDb.Id,
		//			ordertime = orderInDb.ordertime,
		//			fk_member_Id = orderInDb.fk_member_Id,
		//			total_price = orderInDb.total_price,
		//			total_quantity = orderInDb.total_quantity,
		//			logistics_company_Id = orderInDb.logistics_company_Id,
		//			order_status_Id = orderInDb.order_status_Id,
		//			pay_method_Id = orderInDb.pay_method_Id,
		//			pay_status_Id = orderInDb.pay_status_Id,
		//			coupon_name = orderInDb.coupon_name,
		//			coupon_discount = orderInDb.coupon_discount,
		//			freight = orderInDb.freight,
		//			cellphone = orderInDb.cellphone,
		//			receipt = orderInDb.receipt,
		//			receiver = orderInDb.receiver,
		//			recipient_address = orderInDb.recipient_address,
		//			order_description = orderInDb.order_description,
		//			close_Id = orderInDb.close_Id
		//		};
		//}


		public ActionResult OrderItemsIndex()
		{
			IEnumerable<OrderItemsVM> orderItems = GetOrderItemsIndex();
			return View(orderItems);
		}

		private IEnumerable<OrderItemsVM> GetOrderItemsIndex()
		{
			var db = new AppDbContext();
			return db.orderItems
				.AsNoTracking()
				//.Include(p => p.Member1)
				//.OrderBy(p => p.Category.DisplayOrder)
				.ToList()
				.Select(o => new OrderItemsVM
				{
					Id = o.Id,
					order_Id = o.order_Id,
					product_name = o.product_name,
					fk_typeId = o.fk_typeId,
					per_price = o.per_price,
					quantity = o.quantity,
					discount_name = o.discount_name,
					subtotal = o.subtotal,
					discount_subtotal = o.discount_subtotal,
					Items_description = o.Items_description,
					
				});
		}
	}
    
}
