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
		//建立了一個私有欄位 _repo，型別是 ISpeakerRepository
		private ISpeakerRepository _repo;

		//將實作了 ISpeakerRepository 介面的物件傳入。也就是說，我們將能夠執行資料庫操作的類別的物件傳入 SpeakerServices。
		//SpeakerServices 就可以使用 _repo 物件來執行與講者資料庫存取相關的操作
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
            //if (dto.SpeakerPhone.Length != 10)
            //{
            //    return Result.Fail("電話號碼長度錯誤");
            //}
            _repo.CreateSpeaker(dto);
            return Result.Success();
        }

        public Result EditSpeaker(SpeakerEditDto dto)
        {
            _repo.EditSpeaker(dto);
            return Result.Success();

		}
    }

}
