using EFModels.EFModels;
using Members.dll.Models.Dtos;
using Members.dll.Models.Dtos.Staff;
using Members.dll.Models.ViewsModels;
using Members.dll.Models.ViewsModels.Staff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Members.dll.Models.Exts
{
	public static class StaffsExts
	{
		public static ForgetPasswordDto ToForgetPasswordDto(this ForgetPasswordVM vm)
		{
			return new ForgetPasswordDto
			{
				Account = vm.Account,
				NewPassword = vm.NewPassword,
				ConfirmPassword = vm.ConfirmPassword
			};
		}

		public static StaffsCreateDto ToStaffsCreateDto(this StaffsCreateVM vm)
		{
			return new StaffsCreateDto()
			{
				Department = vm.Department,
				JobTitle = vm.JobTitle,
				Name = vm.Name,
				Age = vm.Age,
				Birthday = vm.Birthday,
				Gender = vm.Gender,
				Email = vm.Email,
				StaffPermission = vm.StaffPermission
			};
		}

		public static StaffsIndexVM ToStaffsIndexVM(this StaffsIndexDto dto)
		{
			return new StaffsIndexVM()
			{
				StaffId = dto.StaffId,
				Name = dto.Name,
				Age = dto.Age,
				Gender = dto.Gender,
				Email = dto.Email,
				DueDate = dto.DueDate,
				Department = dto.Department,
				JobTitle = dto.TitleName,
				StaffPermission = dto.LevelName
			};
		}

		public static StaffsIndexDto ToStaffsIndexDto(this Staff entity)
		{
			return new StaffsIndexDto()
			{
				StaffId = entity.StaffId,
				Name = entity.Name,
				Age = entity.Age,
				Gender = entity.Gender,
				Email = entity.Email,
				DueDate = entity.DueDate,
				Department = entity.Department.DepartmentName,
				TitleName = entity.JobTitle.TitleName,
				LevelName = entity.StaffPermission.LevelName
			};
		}

		public static StaffDetailVM ToStaffDetailVM(this StaffDetailDto dto)
		{
			return new StaffDetailVM()
			{
				StaffId = dto.StaffId,
				Department = dto.Department,
				TitleName = dto.TitleName,
				Gender = dto.Gender,
				Name = dto.Name,
				Age = dto.Age,
				Email = dto.Email,
				StaffPermission = dto.LevelName,
				DueDate = dto.DueDate,
			};
		}

		public static EditStaffDto ToStaffEditDto(this EditStaffVM vm)
		{
			return new EditStaffDto()
			{
				StaffId = vm.StaffId,
				Name = vm.Name,
				Age = vm.Age,
				Gender = vm.Gender,
				Mobile = vm.Mobile,
				Email = vm.Email,
				Birthday = vm.Birthday,
				DueDate = vm.DueDate,
				Department = vm.Department,
				TitleName = vm.TitleName,
				LevelName = vm.LevelName
			};
		}
	}
}
