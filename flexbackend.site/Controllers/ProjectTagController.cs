using Discount.dll.Models.Infra;
using Discount.dll.Models.Infra.DapperRepositories;
using Discount.dll.Models.Interfaces;
using Discount.dll.Models.Services;
using Discount.dll.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace flexbackend.site.Controllers
{
    public class ProjectTagController : Controller
    {
        private readonly IProjectTagRepository _repo;
        private readonly ProjectTagService _service;
        public ProjectTagController()
        {
            _repo = new ProjectTagDapperRepository();
            _service = new ProjectTagService(_repo);
        }
        // GET: ProjectTag
        public ActionResult Index()
        {
            IEnumerable<ProjectTagIndexVM> ProjectTags = GetProjectTags();
            return View(ProjectTags);
        }
        [HttpPost]
        public ActionResult UpdateStatus(int[] ids, bool isComplete)
        {
            List<ProjectTagStatusChangeVM> vms = new List<ProjectTagStatusChangeVM>();
            for(int i = 0; i<ids.Length; i++)
            {
                vms.Add(new ProjectTagStatusChangeVM
                {
                    ProjectTagId = ids[i],
                    Status = !isComplete
                });
            }

            var result = UpdateProjectTag(vms);

            return Json(new { success = result.IsSuccess , error = result.ErrorMessage });
        }
        [HttpPost]
        public ActionResult GetData(string input, bool getCompleteResult)
        {
            // 返回JSON格式的響應
            return Json(GetProjectTags(input, getCompleteResult));
        }

        private IEnumerable<ProjectTagIndexVM> GetProjectTags(string projectTagName = null , bool getCompleteResult = false)
        {
            return _service.Search(projectTagName, getCompleteResult).Select(x=>x.ToViewModel()).ToList();
        }

        private Result UpdateProjectTag(List<ProjectTagStatusChangeVM> vms)
        {
            return _service.Update(vms.Select(x=>x.ToStatusChangeDto()).ToList());
        }
    }
}