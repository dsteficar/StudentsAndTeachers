using AutoMapper;
using Business.CustomExceptions;
using Business.ExtensionMethods;
using Business.Interfaces;
using Data.Interfaces;
using Data.Models;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Business.Services
{
    public class StudentService : IStudentService
    {
        private readonly IRepository<Student> _studentRepository;
        private readonly IRepository<SchoolClass> _schoolClassRepository;
        private readonly IMapper _mapper;

        public StudentService(IRepository<Student> studentRepository, IRepository<SchoolClass> schoolClassRepository, IMapper mapper)
        {
            _studentRepository = studentRepository;
            _schoolClassRepository = schoolClassRepository;
            _mapper = mapper;
        }

        public async Task<List<StudentDTO>> GetAll()
        {
            var students = await _studentRepository.GetAll();
            return students.Select(x => _mapper.Map<StudentDTO>(x)).ToList();
        }

        public async Task<StudentDTO> GetById(int id)
        {
            var student = await _studentRepository.GetById(id);
            return _mapper.Map<StudentDTO>(student);
        }

        public async Task<StudentDTO> Insert(StudentDTO studentDTO)
        {
            var schoolclassDb = await _schoolClassRepository.GetById(studentDTO.Id);

            if(schoolclassDb == null)
            {
                throw new DBConcurrencyException();
            }
            var studentDb = _mapper.Map<Student>(studentDTO);

            _mapper.Map(schoolclassDb, studentDb);
            
            await _studentRepository.Insert(studentDb);
            return studentDTO;
        }

        public async Task Update(int id, StudentDTO studentDTO)
        {
            var schoolclassDb = await _schoolClassRepository.GetById(studentDTO.SchoolClassId);
            var studentDb = await _studentRepository.GetById(id);

            if (schoolclassDb == null || studentDb == null)
            {
                throw new DBConcurrencyException();
            }

            _mapper.Map(studentDTO, studentDb);
            _mapper.Map(schoolclassDb, studentDb);

            try
            {
                await _studentRepository.Update(studentDb);
            }
            catch (DbUpdateException)
            {
                throw new DbUpdateException();
            }
        }

        //Provjeri cascade
        public async Task Delete(int id)
        {
            await _studentRepository.Delete(id);
        }
    }
}
