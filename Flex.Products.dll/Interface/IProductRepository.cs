﻿using Flex.Products.dll.Exts;
using Flex.Products.dll.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Flex.Products.dll.Service.ProductService;

namespace Flex.Products.dll.Interface
{
	public interface IProductRepository
	{
		void CreateProduct(ProductDto dto);

		//判斷產品識別碼是否已存在
		bool ExisProductID(string ProductId);

		//檢查上下架時間，上架時間不得>下架時間
		bool ValidationStartAndEndTime(DateTime start, DateTime? end);

		List<ProductDto> Search(IndexSearchCriteria criteria);
	}
}
