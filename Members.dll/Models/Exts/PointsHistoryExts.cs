using Members.dll.Models.Dtos;
using Members.dll.Models.ViewsModels.PointHistories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace Members.dll.Models.Exts
{
	public static class PointsHistoryExts
	{
		public static IndexVM ToIndexVM(this PointsHistoryIndexDto dto)
		{
			return new IndexVM
			{
				PointHistoryId = dto.PointHistoryId,
				Name = dto.Name,
				Id = dto.Id,
				TypeName = dto.TypeName,
				GetOrDeduct = dto.GetOrDeduct,
				//PointSubtotal = dto.MemberPoints.Sum(p => p.PointSubtotal),
				UsageAmount = dto.UsageAmount,
				ordertime = dto.ordertime,
				PointSubtotal = dto.PointSubtotal
			};
		}
	}
}
