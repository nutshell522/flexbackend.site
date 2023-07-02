﻿using EFModels.EFModels;
using Members.dll.Models.Dtos;
using Members.dll.Models.Exts;
using Members.dll.Models.Interfaces;
using Members.dll.Models.lnfra;
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
		//Login
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

	}
}
