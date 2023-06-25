using EFModels.EFModels;
using Members.dll.Models.Dtos;
using Members.dll.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Members.dll.Models.lnfra.EFRepositories
{
	//Repository用來存取資料庫
	public class MemberEFRepository : IMemberRepository
	{
		private AppDbContext _db;
		public MemberEFRepository()
		{
			_db = new AppDbContext();
		}

		public bool ExistAccount(string account) //判斷帳號是否存在
		{
			return _db.Members.Any(m => m.Account == account);//Any指的是有沒有
		}

		public void Register(RegisterDto dto)
		{
			//將RegisterDto 轉為 Member
			Member member = new Member
			{
				Account = dto.Account,
				EncryptedPassword = dto.EncryptedPassword,
				Name = dto.Name,
				Gender = dto.Gender,
				Mobile = dto.Mobile,
				Email = dto.Email,
				Birthday = dto.Birthday,
				IsConfirmed = dto.IsConfirmed,
				ConfirmCode = dto.ConfirmCode
			};

			//將它存到db
			_db.Members.Add(member);
			_db.SaveChanges();
		}
	}
}
