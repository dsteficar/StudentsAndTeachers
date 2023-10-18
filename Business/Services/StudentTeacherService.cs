using Business.Interfaces;
using Data.Interfaces;
using Data.Models;

namespace Business.Services
{
    public class StudentTeacherService : IStudentTeacherService
    {
        private readonly IStudentTeacherRepository<StudentTeacherDTO> _repository;

        //MUST MAKE REPOSITORY!!!
        public StudentTeacherService(IStudentTeacherRepository<StudentTeacherDTO> repository)
        {
            _repository = repository;
        }
        public async Task<List<StudentTeacherDTO>> GetAll()
        {
            var entity = await _repository.GetAll();
            return entity;
        }

        public async Task<StudentTeacherDTO> GetById(int firstId, int secondId)
        {
            var entity = await _repository.GetById(firstId, secondId);
            return entity;
        }

        public async Task<StudentTeacherDTO> Insert(StudentTeacherDTO studentTeacher)
        {
            await _repository.Insert(studentTeacher);
            return studentTeacher;
        }

        public async Task Update(StudentTeacherDTO studentTeacher)
        {
            await _repository.Update(studentTeacher);
        }
        public async Task Delete(int firstId, int secondId)
        {
            await _repository.Delete(firstId, secondId);
        }
    }
}
