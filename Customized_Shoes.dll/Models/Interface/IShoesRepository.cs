using Customized_Shoes.dll.Models.Dtos;
using Customized_Shoes.dll.Models.Exts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customized_Shoes.dll.Models.Interface
{
	public interface IShoesRepository
	{
		void CreateShoes(CustomizedShoesDto dto);

		List<CustomizedShoesDto> Search(ShoesSearchCriteria criteria);

		void EditProductsStatus(List<CustomizedShoesDto> dto);
	}
}
