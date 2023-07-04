using Flex_Activity.dll.Infra.DapperRepositories;
using Flex_Activity.dll.Interface;
using Flex_Activity.dll.Models.Dto;
using Flex_Activity.dll.Models.Exts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flex_Activity.dll.Services
{
	public class OneToOneReservationServices
	{
		private IReservationRepository _repo;
        public OneToOneReservationServices(IReservationRepository repo)
        {
            _repo = repo;
        }

        public List<OneToOneReservationIndexDto> GetAll()
        {
            var reservations =_repo.GetAll();
            return reservations.ToList();
        }

        public List<ReservationListDto> GetAll(int speakerId)
        {
            var reservations = _repo.GetAll(speakerId);
            return reservations.ToList();

        }

		public Result Delete(int id)
        {
           _repo.Delete(id);
            return Result.Success();
        }


	}
}
