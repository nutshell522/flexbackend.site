﻿using EFModels.EFModels;
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

		public ActionResult create()
		{
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult create(logistics_companiesVM vm)
		{
			if (ModelState.IsValid == false) return View(vm);

			//建立新會員
			(bool IsSuccess, string ErrorMessage) result = createcompaines(vm);

			if (result.IsSuccess)
			{
				//若成功,轉到ComfirmRegister頁
				return RedirectToAction("Logistics_companiesIndex");
			}
			else
			{
				ModelState.AddModelError(string.Empty, result.ErrorMessage);
				return View(vm);
			}
		}

		private (bool IsSuccess, string ErrorMessage) createcompaines(logistics_companiesVM vm)
		{
			var db = new AppDbContext();

			var createcompaines = new logistics_companies
			{
				Id = vm.Id,
				name = vm.name,
				tel = vm.tel,
				logistics_description = vm.logistics_description,
			};
			db.logistics_companies.Add(createcompaines);
			db.SaveChanges();

			return (true, "");
		}

		public ActionResult Edit(int id)
		{
			var _Companies = GetCompainesById(id);

			if (_Companies == null)
			{
				return HttpNotFound(); // 可以根據你的需求返回一個適當的錯誤頁面或訊息
			}

			var vm = new logistics_companiesVM
			{
				Id = _Companies.Id,
				name = _Companies.name,
				tel = _Companies.tel,
				logistics_description = _Companies.logistics_description,
			};

			return View(vm);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(logistics_companiesVM vm)
		{
			if (ModelState.IsValid == false)
			{
				return View(vm);
			}

			var result = UpdateConpaines(vm);

			if (result.IsSuccess)
			{
				
				return RedirectToAction("Logistics_companiesIndex");
			}
			else
			{
				ModelState.AddModelError(string.Empty, result.ErrorMessage);
				return View(vm);
			}
		}

		private (bool IsSuccess, string ErrorMessage) UpdateConpaines(logistics_companiesVM vm)
		{
			var db = new AppDbContext();

			var companies = db.logistics_companies.FirstOrDefault(o => o.Id == vm.Id);

			if (companies == null)
			{
				return (false, "找不到該訂單"); // 可以根據你的需求返回一個適當的錯誤訊息
			}

			// 更新訂單的相關屬性
			//order.ordertime = vm.ordertime;
			companies.Id = vm.Id;
			companies.name = vm.name;
			companies.tel = vm.tel;
			companies.logistics_description = vm.logistics_description;

			db.SaveChanges();

			return (true, "");
		}

		private logistics_companies GetCompainesById(int id)
		{
			var db = new AppDbContext();
			return db.logistics_companies.FirstOrDefault(o => o.Id == id);
		}
	}
}