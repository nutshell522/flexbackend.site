using Members.dll.Models.Dtos;
using Members.dll.Models.ViewsModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Members.dll.Models.Interfaces
{
	public interface IStaffRepository
	{
		void CreateStaff(StaffsCreateDto dto);//新增員工資料
		IEnumerable<StaffsIndexDto> GetStaffs();//取得員工資料
	}
}
