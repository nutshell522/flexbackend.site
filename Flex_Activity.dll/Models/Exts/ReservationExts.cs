using EFModels.EFModels;
using Flex_Activity.dll.Models.Dto;
using Flex_Activity.dll.Models.ViewModels;
using Flex_Activity.dll.Models.ViewModels.DapperVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flex_Activity.dll.Models.Exts
{
	public static class ReservationExts
	{
		public static OneToOneReservationDapperVM ToIndexVM(this OneToOneReservationIndexDto dto)
		{
			return new OneToOneReservationDapperVM
			{
				SpeakerId = dto.SpeakerId,
				SpeakerName = dto.SpeakerName,
				FieldName = dto.FieldName

			};
		}

		public static ReservationListVM ToIndexVM(this ReservationListDto dto)
		{
			return new ReservationListVM
			{
				MemberId = dto.MemberId,
				Name = dto.Name,
				Mobile = dto.Mobile,
				ReservationStartTime = dto.ReservationStartTime,
				BranchName = dto.BranchName,
				ReservationStatusDescription = dto.ReservationStatusDescription,
				ReservationId = dto.ReservationId

			};
		}
	}
}
