using EFModels.EFModels;
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
		//擴充方法，將MembersDto 轉為 MembersVM，將資料取出
		public static MembersIndexVM ToIndexVM(this Member entity)
		{

			return new MembersIndexVM()
			{
				MemberId = entity.MemberId,
				Name = entity.Name,
				Gender = entity.Gender,
				Email = entity.Email,
				fk_LevelId = entity.fk_LevelId,
				//PointSubtotal = entity.MemberPoints,//需要經過join才有的欄位該如何傳入
				Registration = entity.Registration,
				fk_BlackListId = entity.fk_BlackListId
			};
		}
		//
	}
}
