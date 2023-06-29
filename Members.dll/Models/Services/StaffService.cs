using Members.dll.Models.Dtos;
using Members.dll.Models.Exts;
using Members.dll.Models.Interfaces;
using Members.dll.Models.ViewsModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Members.dll.Models.Services
{
	//商業邏輯判斷
	public class StaffService
	{
		private IStaffRepository _repo;
		public StaffService(IStaffRepository repo)
		{
			_repo = repo;
		}

		public List<StaffsIndexVM> GetStaffs()
		{
			var staffs = _repo.GetStaffs();
			List<StaffsIndexVM> staffsIndexVM = staffs.Select(s=>s.ToStaffsIndexVM()).ToList();
			return staffsIndexVM;
			//傳入表格....
		}
	}
}
