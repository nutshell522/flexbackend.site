using Discount.dll.Models.Infra;
using Discount.dll.Models.Infra.DapperRepositories;
using Discount.dll.Models.Interfaces;
using Discount.dll.Models.Services;
using Discount.dll.Models.ViewModels;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
            for (int i = 0; i < ids.Length; i++)
            {
                vms.Add(new ProjectTagStatusChangeVM
                {
                    ProjectTagId = ids[i],
                    Status = !isComplete
                });
            }

            var result = UpdateProjectTag(vms);

            return Json(new { success = result.IsSuccess, error = result.ErrorMessage });
        }
        [HttpPost]
        public ActionResult GetDatas(string input, bool getCompleteResult)
        {
            // 返回JSON格式的響應
            return Json(GetProjectTags(input, getCompleteResult));
        }
        [HttpPost]
        public ActionResult GetEditData(int id)
        {
            // 返回JSON格式的響應
            return Json(GetProjectTag(id));
        }

        private IEnumerable<ProjectTagIndexVM> GetProjectTags(string projectTagName = null, bool getCompleteResult = false)
        {
            return _service.Search(projectTagName, getCompleteResult).Select(x => x.ToViewModel());
        }
        private ProjectTagEditNameVM GetProjectTag(int id = 0)
        {
            return _service.GetProjectTag(id).ToEditNameVM();
        }
        private Result UpdateProjectTag(List<ProjectTagStatusChangeVM> vms)
        {
            return _service.Update(vms.Select(x => x.ToStatusChangeDto()).ToList());
        }

        public ActionResult CreateOrEdit(int? id)
        {
            if (id != null)
            {
                // 如果有id為 edit
                var vm = _service.GetProjectTag(id.Value).ToEditNameVM();
                return View(vm);
            }
            else
            {
                // 如果id為null 為create
                var vm = new ProjectTagEditNameVM
                {
                    ProjectTagId = 0
                };
                return View(vm);
            }
        }
        [HttpPost]
        public ActionResult CreateOrEdit(int id, string projectName)
        {
            if (!ModelState.IsValid) return Json(GetProjectTag(id));
            var vm = new ProjectTagEditNameVM { ProjectTagId = id, ProjectTagName = projectName, Status = true };
            if (id != 0)
            {
                // 如果有id為 edit
                var result = _service.Update(vm.ToEditNameDto());
                if (result.IsFail)
                {
                    var newVM = vm;
                    return Json(new { newVM, result.IsFail, result.ErrorMessage });
                }
                else
                {
                    var newVM = GetProjectTag(id);
                    return Json(new { newVM, result.IsFail, result.ErrorMessage });
                }
            }
            else
            {
                // 如果id為null 為create
                var result = _service.Create(vm.ToEditNameDto());
                if (result.result.IsFail)
                {
                    var newVM = vm;
                    return Json(new { newVM, result.result.IsFail, result.result.ErrorMessage });
                }
                else
                {
                    var newVM = GetProjectTag(result.id);
                    return Json(new { newVM, result.result.IsFail, result.result.ErrorMessage });
                }
            }
        }
    }
}