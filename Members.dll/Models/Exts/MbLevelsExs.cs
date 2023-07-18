using Members.dll.Models.Dtos.Member;
using Members.dll.Models.ViewsModels.MembershipLevels;
using EFModels.EFModels;

namespace Members.dll.Models.Exts
{
	public static class MbLevelsExs
	{
		public static levelIndexVM TolevelsIndexVM(this LevelsIndexDto dto)
		{
			return new levelIndexVM
			{
				LevelId = dto.LevelId,
				LevelName = dto.LevelName,
				MinSpending = dto.MinSpending,
				Discount = dto.Discount,
				Description = dto.Description,
			};
		}
		public static LevelsIndexDto TolevelsIndexDto(this levelIndexVM vm)
		{
			return new LevelsIndexDto
			{
				LevelId = vm.LevelId,
				LevelName = vm.LevelName,
				MinSpending = vm.MinSpending,
				Discount = vm.Discount,
				Description = vm.Description,
			};
		}
	}
}
