using AutoMapper;
using Business.Interfaces;
using Data.Interfaces;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class ContactInfoService : IContactInfoService
    {
        private readonly IRepository<ContactInfo> _contactInfoRepository;
        private readonly IRepository<Teacher> _teacherRepository;
        private readonly IMapper _mapper;

        public ContactInfoService(IRepository<ContactInfo> contactInfoRepository, IRepository<Teacher> teacherRepository, IMapper mapper)
        {
            _contactInfoRepository = contactInfoRepository;
            _teacherRepository = teacherRepository;
            _mapper = mapper;
        }
        // TRY CATCH ZA METODE REPOZITORIJA!
        public async Task<List<ContactInfoDTO>> GetAll()
        {
            var contactInfos = await _contactInfoRepository.GetAll();
            return contactInfos.Select(x => _mapper.Map<ContactInfoDTO>(x)).ToList();
        }

        public async Task<ContactInfoDTO> GetById(int id)
        {
            var contactInfo = await _contactInfoRepository.GetById(id);
            return _mapper.Map<ContactInfoDTO>(contactInfo);
        }

        public async Task<ContactInfoDTO> Insert(ContactInfoDTO contactInfoDTO)
        {
            var teacherDb = await _teacherRepository.GetById(contactInfoDTO.TeacherId);

            if (teacherDb == null || teacherDb.ContactInfo != null)
            {
                throw new DBConcurrencyException();
            }

            var contactInfoDb = _mapper.Map<ContactInfo>(contactInfoDTO);

            _mapper.Map(teacherDb, contactInfoDb);
            _mapper.Map(contactInfoDb, teacherDb);

            await _contactInfoRepository.Insert(contactInfoDb);
            await _teacherRepository.Update(teacherDb);

            return contactInfoDTO;
        }

        public async Task Update(int id, ContactInfoDTO contactInfoDTO)
        {
            var teacherDb = await _teacherRepository.GetById(contactInfoDTO.TeacherId);
            var contactInfoDb = await _contactInfoRepository.GetById(id);

            if (teacherDb == null || contactInfoDb == null)
            {
                throw new DBConcurrencyException();
            }

            _mapper.Map(contactInfoDTO, contactInfoDb);
            _mapper.Map(teacherDb, contactInfoDb);
            _mapper.Map(contactInfoDb, teacherDb);
            try
            {
                await _contactInfoRepository.Update(contactInfoDb);
                await _teacherRepository.Update(teacherDb);
            }
            catch (DbUpdateException)
            {
                throw new DbUpdateException();
            }
        }

        //Provjeri cascade
        public async Task Delete(int id)
        {
            await _contactInfoRepository.Delete(id);
        }

    }
}
