using EFModels.EFModels;
using Flex_Activity.dll.Infra.EFRepositories;
using Flex_Activity.dll.Interface;
using Flex_Activity.dll.Models.Dto;
using Flex_Activity.dll.Models.Exts;
using Flex_Activity.dll.Models.ViewModels;
using Flex_Activity.dll.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace flexbackend.site.Controllers
{
    public class SpeakerController : Controller
    {
        private AppDbContext db = new AppDbContext();
        private ISpeakerRepository _repo = new SpeakerEFRepository();

		// GET: Speaker Index
		public ActionResult Index()
        {
            //呼叫 GetSpeakers() 方法，並將回傳的演講者資料集合傳遞給視圖（View）
            IEnumerable<SpeakerIndexVM> speakers = GetSpeakers();
            return View(speakers);
        }

        //用來取得演講者的資料
        private IEnumerable<SpeakerIndexVM> GetSpeakers()
        {
            //建立一個資料庫儲存庫（Repository）的實例
            ISpeakerRepository repo = new SpeakerEFRepository();

            SpeakerServices service = new SpeakerServices(repo);
            return service.Search()
                .Select(dto => new SpeakerIndexVM
                {
                    SpeakerId = dto.SpeakerId,
                    SpeakerName = dto.SpeakerName,
                    FieldName = dto.FieldName
                });
        }

        //Get：Create
        public ActionResult Create()
        {
            PrepareSpeakerFieldDataSource(null);
            PrepareBranchDataSource(null);
            var vm = new SpeakerCreateVM();
            return View(vm);
        }

        //Post：Create
        [HttpPost]
        public ActionResult Create(SpeakerCreateVM vm, HttpPostedFileBase fileTeacher)
        {
            if (ModelState.IsValid) //若通過欄位驗證
            {
                //將file存檔，並取得最後存檔的檔案名稱
                string path = Server.MapPath("~/Public/Img");

                //檔案要存放的資料夾位置
                string fileName = SaveUploadedFile(path, fileTeacher);

                //將【檔名+路徑】存入VM
                vm.SpeakerImg = fileName;

                //將vm轉為Dto
                SpeakerCreateDto speakerDto = vm.ToCreatDto();

				//將Dto傳入Service進行邏輯驗證
				//SpeakerServices()需要一個型別是ISpeakerRepository的參數
				var service = new SpeakerServices(_repo);
                var result = service.CreateSpeaker(speakerDto);
                if (result.IsSucces)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, result.ErroeMessage);
                    return View(vm);
                }
            }
            return View(vm);
        }

        //Get：Edit
        public ActionResult Edit (int? id)
        {
			//（Bad Request）HTTP 400 錯誤狀態碼，表示客戶端的請求無效
			if (id == null) return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            Speaker speaker = db.Speakers.Find(id);

            if (speaker == null)
            {
                return HttpNotFound();
            }

            SpeakerEditVM vm = (speaker.ToEditDto()).ToEditVM();
            PrepareSpeakerFieldDataSource(vm.fk_SpeakerFieldId);
            PrepareBranchDataSource(vm.fk_SpeakerBranchId);

            return View(vm);

		}

        [HttpPost]
        public ActionResult Edit (SpeakerEditVM vm)
        {
            PrepareSpeakerFieldDataSource(vm.fk_SpeakerFieldId);
            PrepareBranchDataSource(vm.fk_SpeakerBranchId);

            if (ModelState.IsValid)
            {
                SpeakerEditDto dto = vm.ToEditDto();
                var service = new SpeakerServices(_repo);
                Result result = service.EditSpeaker(dto);

                if (result.IsSucces)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(vm);
                }
                
            }

            return View(vm);

		}
        private string SaveUploadedFile(string path, HttpPostedFileBase fileTeacher)
        {
            //如果沒有上傳檔案或檔案是空的，就不處理，傳回string.empty
            if (fileTeacher == null || fileTeacher.ContentLength == 0) return string.Empty;

            //取得上傳檔案的副檔名 
            string ext = System.IO.Path.GetExtension(fileTeacher.FileName);

            //如果副檔名不再允許範圍內，就不處理，傳回string.empty
            string[] allowedExts = new string[] { ".jpg", ".jpeg", ".png", ".tif" };
            if (allowedExts.Contains(ext.ToLower()) == false) return string.Empty;

            //生成一個不會重複的副檔名
            string newFileName = Guid.NewGuid().ToString("N") + ext;
            string fullName = System.IO.Path.Combine(path, newFileName);

            //將上傳檔案存到指定位置，把fileTeacher存到fullName
            fileTeacher.SaveAs(fullName);

            //傳回存放的檔名
            return newFileName;
        }

        private void PrepareSpeakerFieldDataSource(int? speakerFieldId)
        {
            ViewBag.fk_SpeakerFieldId = new SelectList(db.SpeakerFields, "FieldId", "FieldName", speakerFieldId);
        }
        private void PrepareBranchDataSource(int? branchId)
        {
            ViewBag.fk_SpeakerBranchId = new SelectList(db.Branches, "BranchId", "BranchName", branchId);
        }
    }
}