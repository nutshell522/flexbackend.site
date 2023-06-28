using EFModels.EFModels;
using Members.dll.Models.Dtos;
using Members.dll.Models.ViewsModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Members.dll.Models.Exts
{
	public static class MembersExts
	{
		//擴充方法，將MembersIndexDto 轉為 MembersIndexVM，將資料取出
		public static MembersIndexVM ToIndexVM(this MembersIndexDto dto)
		{

			return new MembersIndexVM()
			{
				MemberId = dto.MemberId,
				Name = dto.Name,
				Gender = dto.Gender,
				Email = dto.Email,
				//LevelName = dto.LevelName,
				//PointSubtotal = dto.MemberPoints,//需要經過join才有的欄位該如何傳入
				Registration = dto.Registration,
				fk_BlackListId = dto.fk_BlackListId
			};
		}
		//擴充方法，將EF entity 轉為 MembersIndexDto，將資料取出
		public static MembersIndexDto ToIndexDto(this Member entity)

		{
			return new MembersIndexDto
			{
				MemberId = entity.MemberId,
				Name = entity.Name,
				Gender = entity.Gender,
				Email = entity.Email,
				//LevelName = entity.LevelName,
				//PointSubtotal = entity.MemberPoints,//需要經過join才有的欄位該如何傳入
				Registration = entity.Registration,
				fk_BlackListId = entity.fk_BlackListId,
			};
		}



	}
}
