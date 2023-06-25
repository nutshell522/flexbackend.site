using EFModels.EFModels;
using Flex.Products.dll.Interface;
using Flex.Products.dll.Models.Dtos;
using Flex.Products.dll.Models.Infra.Exts;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flex.Products.dll.Service
{
	public class ProductService
	{
		private IProductRepository _repo;
        private AppDbContext _db;
        public ProductService(IProductRepository repo)
        {
            this._repo = repo;
            _db= new AppDbContext();
        }


        public Result CreateProduct(ProductDto dto)
        {
            //檢查ProductId是否存在
            if (ExisProductID(dto.ProductId))
            {
                return Result.Fail($"商品編碼 {dto.ProductId} 已存在，請更換其他編碼");
            }

            if (ValidationStartAndEndTime(dto.StartTime, dto.EndTime))
            {
                return Result.Fail($"上架時間{dto.StartTime}不得晚於下架時間{dto.EndTime}");
            }
            dto.LogOut = false;
            dto.CreateTime = DateTime.Now;
            dto.EditTime = DateTime.Now;

            _repo.CreateProduct(dto);

            return Result.Success();
        }

		//檢查上下架時間，上架時間不得>下架時間
		public bool ValidationStartAndEndTime(DateTime start, DateTime? end)
		{
			if (end != null && start > end) { return true; }
			return false;
		}

		//判斷產品識別碼是否已存在
		public bool ExisProductID(string ProductId)
		{
			return _db.Products.Any(p => p.ProductId == ProductId);
		}

	}
	
}
