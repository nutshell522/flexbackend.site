using EFModels.EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Flex_Activity.dll.Models.Exts;
using Flex_Activity.dll.Models.ViewModels;
using System.Net;

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

		public ActionResult Create()
		{
			ViewBag.fk_ActivityCategoryId = new SelectList(db.ActivityCategories, "ActivityCategoryId", "ActivityCategoryName");
			ViewBag.fk_SpeakerId = new SelectList(db.Speakers, "SpeakerId", "SpeakerName");
			return View();
		}

		[HttpPost]
		public ActionResult Create(ActivityCreateVM vm, HttpPostedFileBase file1)
		{
			if (ModelState.IsValid) //如果通過欄位驗證
			{
				//將file存檔，並取得最後存檔的檔案名稱
				//path = 路徑
				string path = Server.MapPath("~/Public/Img"); 
				
				//檔案要存放的資料夾位置
				string fileName = SaveUploadedFile(path, file1);

				//將【路徑+檔名】存入vm裡
				vm.ActivityImage = fileName;

				//將 view model轉型為Activity
				Activity activity = vm.ToEntity();

				//新增一筆紀錄
				db.Activities.Add(activity);
				db.SaveChanges();

				//網頁轉到Activity /Index/
				return RedirectToAction("Index");
			}

			//如果驗證失敗，就留在本網頁

			//取得活動類別需要下拉清單的內容
			ViewBag.fk_ActivityCategoryId = new SelectList(db.ActivityCategories, "ActivityCategoryId", "ActivityCategoryName", vm.fk_ActivityCategoryId);



			////取得活動講者需要下拉清單的內容
			ViewBag.fk_SpeakerId = new SelectList(db.Speakers, "SpeakerId", "SpeakerName", vm.fk_SpeakerId);
			//顯示網頁
			return View(vm);
		}

		private string SaveUploadedFile(string path, HttpPostedFileBase file1)
		{
			//如果沒有上傳檔案或檔案是空的, 就不處理, 傳回string.empty
			if (file1 == null || file1.ContentLength == 0) return string.Empty;
			
			//取得上傳檔案的副檔名 (副檔名 = ext)
			string ext = System.IO.Path.GetExtension(file1.FileName); //".jpg"而不是"jpg"

			//如果副檔名不在允許的範圍裡, 表示上傳不合理的檔案類型, 就不處理, 傳回string.empty
			string[] allowedExts = new string[] { ".jpg", ".jpeg", ".png", ".tif" };
			if(allowedExts.Contains(ext.ToLower()) == false) return string.Empty;

			//生成一個不會重複的副檔名
			string newFileName = Guid.NewGuid().ToString("N") + ext;  //生成er537245dfd5552.jpg
			string fullName = System.IO.Path.Combine(path, newFileName); //結合路徑和副檔名

			//將上傳檔案存放到指定位置，把file1存到fullName
			file1.SaveAs(fullName);  

			//傳回存放的檔名
			return newFileName;

		}

		public ActionResult Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Activity activity = db.Activities.Find(id);
			
			if (activity == null)
			{
				return HttpNotFound();
			}

			ActivityEditVM vm = activity.ToEditVM();
			//取得活動類別需要下拉清單的內容
			ViewBag.fk_ActivityCategoryId = new SelectList(db.ActivityCategories, "ActivityCategoryId", "ActivityCategoryName", vm.fk_ActivityCategoryId);

			////取得活動講者需要下拉清單的內容
			ViewBag.fk_SpeakerId = new SelectList(db.Speakers, "SpeakerId", "SpeakerName", vm.fk_SpeakerId);

			return View(vm);
		}

		[HttpPost]
		public ActionResult Edit([Bind(Include = "ActivityId, ActivityName, fk_ActivityCategoryId, ActivityDate, ActivityPlace, ActivityBookStartTime, ActivityBookEndTime, fk_SpeakerId, ActivityImage, ActivityAge, ActivitySalePrice, ActivityOriginalPrice, ActivityDescription")] ActivityEditVM vm)
		{
			if (ModelState.IsValid)
			{
				Activity activity = vm.ToEntity();
				db.Entry(activity).State = EntityState.Modified;
				db.SaveChanges();
				return RedirectToAction("Index");
			}
			ViewBag.fk_ActivityCategoryId = new SelectList(db.ActivityCategories, "ActivityCategoryId", "ActivityCategoryName", vm.fk_ActivityCategoryId);
			ViewBag.fk_SpeakerId = new SelectList(db.Speakers, "SpeakerId", "SpeakerName", vm.fk_SpeakerId);
			return View(vm);

		}


	}
}
