using EFModels.EFModels;
using Flex_Activity.dll.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace Flex_Activity.dll.Models.Exts
{
	public static class ActivityExts
	{
		public static ActivityIndexVM ToIndexVM(this Activity entity)
		{
			return new ActivityIndexVM
			{
				ActivityId = entity.ActivityId,
				ActivityName = entity.ActivityName,
				ActivityCategoryName = entity.ActivityCategory.ActivityCategoryName,
				ActivityDate = entity.ActivityDate,
				SpeakerName = entity.Speaker.SpeakerName,
				ActivityPlace = entity.ActivityPlace,
				ActivityBookStartTime = entity.ActivityBookStartTime,
				ActivityBookEndTime = entity.ActivityBookEndTime,
				ActivityStatusDescription = entity.ActivityStatus.ActivityStatusDescription
				

			};
		}

		public static ActivityCreateVM ToCreateVM(this Activity vm)
		{
			return new ActivityCreateVM
			{
				ActivityId = vm.ActivityId,
				ActivityName = vm.ActivityName,
				ActivityDate = vm.ActivityDate,
				ActivityPlace = vm.ActivityPlace,
				ActivityBookStartTime = vm.ActivityBookStartTime,
				ActivityBookEndTime = vm.ActivityBookEndTime,
				ActivityImage = vm.ActivityImage,
				ActivityAge = vm.ActivityAge,
				ActivitySalePrice = vm.ActivitySalePrice,
				ActivityOriginalPrice = vm.ActivityOriginalPrice,
				ActivityDescription = vm.ActivityDescription,
				fk_ActivityCategoryId = vm.fk_ActivityCategoryId,
				fk_SpeakerId = vm.fk_SpeakerId

			};
		}

		public static Activity ToEntity(this ActivityCreateVM vm)
		{
			return new Activity
			{
				ActivityId = vm.ActivityId,
				ActivityName = vm.ActivityName,
				ActivityDate = vm.ActivityDate,
				ActivityPlace = vm.ActivityPlace,
				ActivityBookStartTime = vm.ActivityBookStartTime,
				ActivityBookEndTime = vm.ActivityBookEndTime,
				ActivityImage = vm.ActivityImage,
				ActivityAge = (byte)vm.ActivityAge,
				ActivitySalePrice = vm.ActivitySalePrice,
				ActivityOriginalPrice = vm.ActivityOriginalPrice,
				ActivityDescription = vm.ActivityDescription
			};
		}

	}
}

