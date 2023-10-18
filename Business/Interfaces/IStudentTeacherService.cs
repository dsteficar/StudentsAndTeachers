using Data.Models;

namespace Business.Interfaces
{
    public interface IStudentTeacherService
    {
        Task<List<StudentTeacherDTO>> GetAll();
        Task<StudentTeacherDTO> GetById(int firstId, int secondId);
        Task<StudentTeacherDTO> Insert(StudentTeacherDTO studentTeacher);
        Task Update(StudentTeacherDTO studentTeacher);
        Task Delete(int studentId, int teacherId);
    }
}
