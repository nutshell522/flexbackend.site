using Members.dll.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Members.dll.Models.Interfaces
{
	public interface IStaffRepository
	{
		IEnumerable<StaffsIndexDto> GetStaffs();//取得員工資料
	}
}
