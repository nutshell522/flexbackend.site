using Discount.dll.Models.Dtos;
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
	}
}
