using EFModels.EFModels;
using Members.dll.Models.Dtos;
using Members.dll.Models.Exts;
using Members.dll.Models.Interfaces;
using Members.dll.Models.lnfra;
using Members.dll.Models.ViewsModels;
using Members.dll.Models.ViewsModels.Staff;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Principal;
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

		//忘記密碼
		public Result ResetPassword(ForgetPasswordDto dto)
		{
			var db = new AppDbContext();
			var staffAccount = db.Staffs.ToList().Select(s => s.Account);

			//判斷帳號是否存在
			if (!staffAccount.Contains(dto.Account))
			{
				return Result.Fail("帳號或密碼錯誤");
			}
			else
			{
				string newpassword = dto.NewPassword;
				_repo.SaveNewPassword(newpassword, dto.Account);
				return Result.Success();
			}
		}

		//Login 沒有寫到三層式
		public Result Login(LoginVM vm)
		{
			//判斷帳號、密碼是否存在
			//if(vm.Account==Staff.)


			return Result.Success();
		}
		//Create		
		public Result CreateStaff(StaffsCreateDto dto)
		{
			//判斷收到的資料是否正確
			//計算年齡，目前年分-出生年份
			int age = DateTime.Today.Year - dto.Birthday.Value.Year;
			dto.Age = (byte)age;

			//存到db
			_repo.CreateStaff(dto);

			return Result.Success();
		}

		//Read
		public List<StaffsIndexVM> GetStaffs()
		{
			var staffs = _repo.GetStaffs();
			List<StaffsIndexVM> staffsIndexVM = staffs.Select(s => s.ToStaffsIndexVM()).ToList();
			return staffsIndexVM;
		}

		public Result DeleteStaff(int staffId)
		{
			var db = new AppDbContext();
			var result = db.Staffs.Where(s => s.StaffId == staffId).FirstOrDefault();
			_repo.DeleteStaff(staffId);
			//db.Staffs.Remove(result);
			return Result.Fail("找不到此員工");
		}
	}
}
