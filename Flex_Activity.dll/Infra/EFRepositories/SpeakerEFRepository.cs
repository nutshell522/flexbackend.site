using EFModels.EFModels;
using Flex_Activity.dll.Interface;
using Flex_Activity.dll.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Flex_Activity.dll.Models.Exts;

namespace Flex_Activity.dll.Infra.EFRepositories
{
    public class SpeakerEFRepository : ISpeakerRepository
    {
        private AppDbContext _db;
        public SpeakerEFRepository()
        {
            _db = new AppDbContext();
        }

        public void CreateSpeaker(SpeakerCreateDto dto)
        {
            var speaker = dto.ToCreateEntity();
            _db.Speakers.Add(speaker);
            _db.SaveChanges();
        }

		public void EditSpeaker(SpeakerEditDto dto)
		{
            var speaker = dto.ToEditEntity();
            _db.Entry(speaker).State = EntityState.Modified;
            _db.SaveChanges();
		}

		public IEnumerable<SpeakerIndexDto> Search()
        {
            return _db.Speakers
                .AsNoTracking()
                .Include(s => s.fk_SpeakerFieldId)
                .Select(s => new SpeakerIndexDto
                {
                    SpeakerId = s.SpeakerId,
                    SpeakerName = s.SpeakerName,
                    fk_SpeakerFieldId = s.fk_SpeakerFieldId
                });
        }
    }
}
