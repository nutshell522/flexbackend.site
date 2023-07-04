﻿using EFModels.EFModels;
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
        public ActionResult OrdersIndex(string searchString)
		{
			IEnumerable<OrdersIndexVM> orders = GetOrders(searchString);
			return View(orders);

		}

		[HttpPost]
		private IEnumerable<OrdersIndexVM> GetOrders(string searchString)
		{
			var db = new AppDbContext();
			var orders = db.orders
				.AsNoTracking();

			if (!string.IsNullOrEmpty(searchString) && int.TryParse(searchString, out int memberId))
			{
				orders = (System.Data.Entity.Infrastructure.DbQuery<order>)orders.Where(o => o.fk_member_Id == memberId);
			}

			return orders.ToList()
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
					orderItems = (List<OrderItemsVM>)GetOrderItemsIndex(p.Id)
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


        public ActionResult OrderItemsIndex(int id)
        {
            IEnumerable<OrderItemsVM> orderItems = GetOrderItemsIndex(id);
            TempData["OrderId"] = id; // 將 id 儲存在 TempData 中
            return View(orderItems);
        }
        private IEnumerable<OrderItemsVM> GetOrderItemsIndex(int orderId)
		{
			var db = new AppDbContext();

			return db.orderItems
			.AsNoTracking()
			.Where(o => o.order_Id == orderId)
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
				Items_description = o.Items_description
			})
			.ToList();
		}

        public ActionResult CreateOrderItems()
        {
            
            return View();
        }
        [HttpPost]
		[ValidateAntiForgeryToken]
        public ActionResult CreateOrderItems(OrderItemsVM vm)
        {
            int orderId = (int)TempData["OrderId"];
            
            if (ModelState.IsValid == false) return View(vm);

            (bool IsSuccess, string ErrorMessage, int OrderItemId) result = CreatenewOrderItems(vm, orderId);

            if (result.IsSuccess)
            {
                int orderItemId = result.OrderItemId;
                return RedirectToAction("OrderItemsIndex", new { id = orderItemId });
            }
            else
            {
                ModelState.AddModelError(string.Empty, result.ErrorMessage);
                return View(vm);
            }
        }
        private (bool IsSuccess, string ErrorMessage, int OrderItemId) CreatenewOrderItems(OrderItemsVM vm, int orderId)
		{
			var db = new AppDbContext();

			var orderitems = new orderItem
			{
				Id = vm.Id,
				order_Id = orderId,
				product_name = vm.product_name,
				fk_typeId = vm.fk_typeId,
				per_price = vm.per_price,
				quantity = vm.quantity,
				discount_name = vm.discount_name,
				subtotal = vm.subtotal,
				discount_subtotal = vm.discount_subtotal,
				Items_description = vm.Items_description
			};

			db.orderItems.Add(orderitems);
			db.SaveChanges();

            return (true, "", orderitems.order_Id);

        }
	}
}
