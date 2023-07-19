using Members.dll.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Members.dll.Models.Services
{
	public class PointHistoriesService
	{
		private IPointHistoriesRepository _repo;
		public PointHistoriesService(IPointHistoriesRepository repo)
		{
			_repo = repo;
		}
	}
}
