using Customized_Shoes.dll.Models.Interface;
using EFModels.EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customized_Shoes.dll.Models.Service
{
	public class CustomizedShoesService
	{
		private IShoesRepository _repo;
		private AppDbContext _db;

		public CustomizedShoesService(IShoesRepository repo)
		{ 
			this._repo = repo;
			_db = new AppDbContext();
		}
	}
}
