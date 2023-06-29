﻿using EFModels.EFModels;
using Members.dll.Models.Dtos;
using Members.dll.Models.ViewsModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Members.dll.Models.Exts
{
	public static class StaffsExts
	{
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
				DueDate = entity.dueDate,
				Department = entity.Department.DepartmentName,
				TitleName = entity.JobTitle.TitleName,
				LevelName = entity.StaffPermission.LevelName
			};
		}
	}
}
