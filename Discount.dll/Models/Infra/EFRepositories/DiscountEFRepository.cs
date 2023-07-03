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
            IQueryable<Discount> query = _db.Discounts;

            return _db.Discounts
                .AsNoTracking()
                .Include(p => p.ProjectTag)
                .OrderBy(p => p.OrderBy)
                .Select(p => new DiscountIndexDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    CategoryName = p.Category.Name,
                    Price = p.Price,
                    ProductImage = p.ProductImage,
                    Stock = p.Stock,
                });
        }

    }
}
