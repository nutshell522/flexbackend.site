using EFModels.EFModels;
using Flex_Activity.dll.Models.Dto;
using Flex_Activity.dll.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Flex_Activity.dll.Models.Exts
{
	public static class SpeakerExts
	{
		public static SpeakerCreateDto ToCreatDto(this SpeakerCreateVM vm)
		{
			return new SpeakerCreateDto
			{
				SpeakerId = vm.SpeakerId,
				SpeakerName = vm.SpeakerName,

				SpeakerPhone = vm.SpeakerPhone,

				fk_SpeakerFieldId = vm.fk_SpeakerFieldId,

				SpeakerImg = vm.SpeakerImg,

				fk_SpeakerBranchId = vm.fk_SpeakerBranchId,

				SpeakerDescription = vm.SpeakerDescription

			};
		}

		public static Speaker ToCreateEntity(this SpeakerCreateDto dto)
		{
			return new Speaker
			{
				SpeakerId = dto.SpeakerId,
				SpeakerName = dto.SpeakerName,

				SpeakerPhone = dto.SpeakerPhone,

				fk_SpeakerFieldId = dto.fk_SpeakerFieldId,

				SpeakerImg = dto.SpeakerImg,

				fk_SpeakerBranchId = dto.fk_SpeakerBranchId,

				SpeakerDescription = dto.SpeakerDescription

			};
		}

		public static SpeakerEditDto ToEditDto(this SpeakerEditVM vm)
		{
			return new SpeakerEditDto
			{
				SpeakerId = vm.SpeakerId,
				SpeakerName = vm.SpeakerName,
				SpeakerPhone = vm.SpeakerPhone,
				fk_SpeakerFieldId = vm.fk_SpeakerFieldId,
				SpeakerImg = vm.SpeakerImg,
				fk_SpeakerBranchId = vm.fk_SpeakerBranchId,
				SpeakerDescription = vm.SpeakerDescription
			};
			
		}

		public static Speaker ToEditEntity(this SpeakerEditDto dto)
		{
			return new Speaker
			{
				SpeakerId = dto.SpeakerId,
				SpeakerName = dto.SpeakerName,
				SpeakerPhone = dto.SpeakerPhone,
				fk_SpeakerFieldId = dto.fk_SpeakerFieldId,
				SpeakerImg = dto.SpeakerImg,
				fk_SpeakerBranchId = dto.fk_SpeakerBranchId,
				SpeakerDescription = dto.SpeakerDescription
			};
		}

		public static SpeakerEditDto ToEditDto (this Speaker entity)
		{
			return new SpeakerEditDto
			{
				SpeakerId = entity.SpeakerId,
				SpeakerName = entity.SpeakerName,
				SpeakerPhone = entity.SpeakerPhone,
				fk_SpeakerFieldId = entity.fk_SpeakerFieldId,
				SpeakerImg = entity.SpeakerImg,
				fk_SpeakerBranchId = entity.fk_SpeakerBranchId,
				SpeakerDescription = entity.SpeakerDescription
			};

		}
		public static SpeakerEditVM ToEditVM(this SpeakerEditDto dto)
		{
			return new SpeakerEditVM
			{
				SpeakerId = dto.SpeakerId,
				SpeakerName = dto.SpeakerName,
				SpeakerPhone = dto.SpeakerPhone,
				fk_SpeakerFieldId = dto.fk_SpeakerFieldId,
				SpeakerImg = dto.SpeakerImg,
				fk_SpeakerBranchId = dto.fk_SpeakerBranchId,
				SpeakerDescription = dto.SpeakerDescription
			};
				

		}

		public static SpeakerIndexVM ToIndexVM(this SpeakerIndexDto dto)
		{
			return new SpeakerIndexVM
			{
				SpeakerId = dto.SpeakerId,
				SpeakerName = dto.SpeakerName,
				fk_SpeakerFieldId = dto.fk_SpeakerFieldId
			};
		}

		public static SpeakerIndexDto ToIndexDto (this Speaker entity)
		{
			return new SpeakerIndexDto
			{
				SpeakerId = entity.SpeakerId,
				SpeakerName = entity.SpeakerName,
				fk_SpeakerFieldId = entity.fk_SpeakerFieldId
			};

		}
	}
}
