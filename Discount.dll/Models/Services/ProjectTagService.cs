using Discount.dll.Models.Dtos;
using Discount.dll.Models.Infra;
using Discount.dll.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discount.dll.Models.Services
{
    public class ProjectTagService
    {
        private IProjectTagRepository _repo;
        public ProjectTagService(IProjectTagRepository repo)
        {
            _repo = repo;
        }
        public IEnumerable<ProjectTagIndexDto> Search(string projectTagName = null, bool getCompleteResult = false)
        {
            return _repo.GetProjectTags(projectTagName, getCompleteResult);
        }

        /// <summary>
        /// 更新專案狀態
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public Result Update(List<ProjectTagStatusChangeDto> dtos)
        {
            for (int i = 0; i < dtos.Count; i++)
            {
                _repo.UpdateProjectTag(dtos[i]);
            }
            return Result.Success();
        }
    }
}
