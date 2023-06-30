using EFModels.EFModels;
using Members.dll.Models.Dtos;
using Members.dll.Models.Exts;
using Members.dll.Models.Interfaces;
using Members.dll.Models.ViewsModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using static Dapper.SqlMapper;

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

		//會員資料管理
		public void EditMember(MembersEditDto dto)
		{
			Member member = new Member
			{
				MemberId=dto.MemberId,
				Name=dto.Name,
				Age=dto.Age,
				Gender=dto.Gender,
				Mobile=dto.Mobile,
				Email=dto.Email,
				Birthday=dto.Birthday,
				Registration=dto.Registration,
				fk_LevelId=dto.fk_LevelId,
				fk_BlackListId=dto.fk_BlackListId,
			};
			_db.Members.Add(member);
			_db.SaveChanges();
		}

		//取得員工資料
		public List<MembersIndexDto> GetMemberList()
		{
			var members = _db.Members.Include(m => m.MemberPoints).Include(m => m.BlackList).Include(m => m.MembershipLevel);
			var memberList = members.ToList().Select(m => m.ToIndexDto()).ToList();
			return memberList;
		}

		//判斷帳號是否存在
		public bool ExistAccount(string account)
		{
			return _db.Members.Any(m => m.Account == account);//Any指的是有沒有
		}

		//通過 RegisterDto傳入Dto
		public void Register(RegisterDto dto)
		{
			//將RegisterDto 轉為 Member，Member是數據訪問層中對應數據表的實體對象(EF裡面的Member)。
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

			//使用Entity Framework的DbContext將Member存到db
			_db.Members.Add(member);
			_db.SaveChanges();
		}


	}
}
