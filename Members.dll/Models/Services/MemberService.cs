using EFModels.EFModels;
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
	public class MemberService
	{
		private IMemberRepository _repo;
		public MemberService(IMemberRepository repo)
		{
			_repo = repo;
		}

		//會員資料管理
		public Result EditMember(MembersEditDto dto)
		{
			//判斷是否符合等級
			//if () 
			//{
			
			//} else
			//{

			//}

			//判斷是否為黑名單

			return Result.Success();
		}


		//會員名單總覽
		public List<MembersIndexVM> MemberList()
		{
			List<MembersIndexDto> members = _repo.GetMemberList();
			List<MembersIndexVM> membersIndexVM = members.Select(m => m.ToIndexVM()).ToList();
			return membersIndexVM;
		}

		//會員註冊
		public Result Register(RegisterDto dto)
		{
			//判斷帳號是否已被啟用
			if (_repo.ExistAccount(dto.Account))//呼叫介面裡的方法傳入RegisterDto中的帳號
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
			_repo.Register(dto);//呼叫介面的Register方法，傳入RegisterDto添加到db

			//todo 寄發 email

			return Result.Success();
		}
	}
}
