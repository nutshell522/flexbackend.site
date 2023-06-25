using Members.dll.Models.Dtos;
using Members.dll.Models.Interfaces;
using Members.dll.Models.lnfra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Members.dll.Models.Services
{
	//商業邏輯判斷
	public class MemberService
	{
		private IMemberRepository _repo;
		public MemberService (IMemberRepository repo)
		{
			_repo = repo;
		}

		public Result Register(RegisterDto dto)
		{
			//判斷帳號是否已被盜用
			if(_repo.ExistAccount(dto.Account))
			{
				//丟出異常或傳回 Result
				return Result.Fail($"帳號{dto.Account}已存在,請更換後再試一次");
			}

			//將密碼進行雜湊
			//var salt = HashUtility.

			//填入isConfirmed,ConfirmCode
			dto.IsConfirmed = false;
			dto.ConfirmCode = Guid.NewGuid().ToString("N");	

			//新增一筆紀錄
			_repo.Register(dto);

			//todo 寄發 email

			return Result.Success();
		}
	}
}
