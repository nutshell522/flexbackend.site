using Flex_Activity.dll.Interface;
using Flex_Activity.dll.Models.Dto;
using Flex_Activity.dll.Models.Exts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Flex_Activity.dll.Services
{
    public class SpeakerServices
    {
        private ISpeakerRepository _repo;

        public SpeakerServices (ISpeakerRepository repo)
        {
            _repo = repo;
        }

        public IEnumerable<SpeakerIndexDto> Search()
        {
            return _repo.Search();
        }

        public Result CreateSpeaker(SpeakerCreateDto dto) 
        { 
            _repo.CreateSpeaker(dto);
            return Result.Success();
        }
    }

}
