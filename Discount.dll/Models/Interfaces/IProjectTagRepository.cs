using Discount.dll.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discount.dll.Models.Interfaces
{
    public interface IProjectTagRepository
    {
        IEnumerable<ProjectTagIndexDto> SearchProjectTags(string projectTagName = null,bool getCompleteResult = false);
        void UpdateProjectTag(ProjectTagStatusChangeDto dto);
        void UpdateProjectTag(ProjectTagEditNameDto dto);
		ProjectTagEditNameDto GetProjectTagById(int id);
	}
}
