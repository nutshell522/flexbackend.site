using Discount.dll.Models.Dtos;
using Discount.dll.Models.Interfaces;
using EFModels.EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Runtime.Remoting.Contexts;

namespace Discount.dll.Models.Infra.EFRepositories
{
    public class DiscountEFRepository : IDiscountRepository
    {
        private AppDbContext _db;

        public DiscountEFRepository()
        {
            _db = new AppDbContext();
        }
        public IEnumerable<DiscountIndexDto> GetDiscounts(bool searchExpired = false, string searchDiscountName = null)
        {
            var  query = _db.Discounts.AsQueryable();

			if (!searchExpired)
			{
				DateTime today = DateTime.Today;

				query = query.Where(d => d.EndDate == null || d.EndDate >= today);
			}

			// 根据传入的searchDiscountName参数进行模糊搜索
			if (!string.IsNullOrEmpty(searchDiscountName))
			{
				query = query.Where(d => d.DiscountName.Contains(searchDiscountName));
			}

			return query
				.AsNoTracking()
                .Include(p => p.ProjectTag)
                .OrderBy(p => p.OrderBy)
                .Select(p => new DiscountIndexDto
                {
                    DiscountId = p.DiscountId,
                    DiscountName = p.DiscountName,
                    DiscountDescription = p.DiscountDescription,
                    DiscountType = p.DiscountType,
                    DiscountValue = p.DiscountValue,
                    ProjectTagId = p.ProjectTag.ProjectTagId,
                    ProjectTagName = p.ProjectTag.ProjectTagName,
                    ConditionType = p.ConditionType,
                    ConditionValue = p.ConditionValue,
                    StartDate = p.StartDate,
                    EndDate = p.EndDate,
                    OrderBy = p.OrderBy
                });
        }

    }
}
