using Discount.dll.Models.Dtos;
using Discount.dll.Models.Infra;
using Discount.dll.Models.Infra.EFRepositories;
using Discount.dll.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discount.dll.Models.Services
{
	public class DiscountService
	{

		private IDiscountRepository _repo;
		public DiscountService(IDiscountRepository repo)
		{
			_repo = repo;
		}

		public IEnumerable<DiscountIndexDto> GetDiscounts(bool searchExpired = false, string searchDiscountName = null)
		{
			return _repo.GetDiscounts(searchExpired, searchDiscountName);
		}
		public DiscountCreateOrEditDto GetDiscountById(int id)
		{
			return _repo.GetDiscountById(id);
		}
		public (int id, Result result) Create(DiscountCreateOrEditDto dto)
		{
			// TODO 驗證規則

			return (_repo.Create(dto),Result.Success());
		}
		public Result Delete(int[] ids)
		{
			for (int i = 0; i < ids.Length; i++)
			{
				_repo.Delete(ids[i]);
			}
			return Result.Success();
		}
		public Result Update(DiscountCreateOrEditDto dto)
		{
			// TODO 驗證規則

			_repo.Update(dto);
			return Result.Success();
		}

	}
}
