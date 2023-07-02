using Flex_Activity.dll.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flex_Activity.dll.Interface
{
    public interface ISpeakerRepository
    {
        IEnumerable<SpeakerIndexDto> Search();
    }
}
