using EFModels.EFModels;
using Flex.Products.dll.Exts;
using Flex.Products.dll.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flex.Products.dll.Infra.EFRepository
{
	public class ColorRepository
	{
		public List<ColorDto> GetColorCategory()
		{
			return new AppDbContext()
				.ColorCategories
				.OrderBy(c=>c.ColorId)
				.ToList()
				.Select(c=>c.ToDto())
				.ToList();
		}
	}
}
