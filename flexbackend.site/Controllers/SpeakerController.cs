using Flex_Activity.dll.Infra.EFRepositories;
using Flex_Activity.dll.Interface;
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
        // GET: Speaker
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
    }
}