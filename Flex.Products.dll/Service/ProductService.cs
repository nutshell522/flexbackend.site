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
        public ProductService(IProductRepository repo)
        {
            this._repo = repo;
        }


        public Result CreateProduct(ProductDto dto)
        {
            //檢查ProductId是否存在
            if (_repo.ExisProductID(dto.ProductId))
            {
                return Result.Fail($"商品編碼 {dto.ProductId} 已存在，請更換其他編碼");
            }

            if (_repo.ValidationStartAndEndTime(dto.StartTime, dto.EndTime))
            {
                return Result.Fail($"上架時間{dto.StartTime}不得晚於下架時間{dto.EndTime}");
            }
            dto.LogOut = false;
            dto.CreateTime = DateTime.Now;
            dto.EndTime = DateTime.Now;

            _repo.CreateProduct(dto);

            return Result.Success();
        }

	}
	
}
