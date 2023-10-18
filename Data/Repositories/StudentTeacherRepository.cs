using Data.Interfaces;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class StudentTeacherRepository : IStudentTeacherRepository<StudentTeacher>
    {
        protected readonly ApplicationDbContext context;
        private DbSet<StudentTeacher> entities;
        string errorMessage = string.Empty;

        public StudentTeacherRepository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<StudentTeacher>();
        }

        public async Task<List<StudentTeacher>> GetAll()
        {
            return await entities.ToListAsync();
        }

        public async Task<StudentTeacher> GetById(int studentId, int teacherId)
        {
            var entity = await entities.SingleOrDefaultAsync(s => s.StudentId == studentId && s.TeacherId == teacherId);
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            return entity;
        }

        public async Task Insert(StudentTeacher studentTeacher)
        {
            if (studentTeacher == null) throw new ArgumentNullException("entity");

            await entities.AddAsync(studentTeacher);
            await Save();
        }

        public async Task Delete(int studentId, int teacherId)
        {
            var entity = await entities.SingleOrDefaultAsync(s => s.StudentId == studentId && s.TeacherId == teacherId);
            if (entity == null) throw new ArgumentNullException("entity");

            entities.Remove(entity);

            await Save();
        }

        public async Task Save()
        {
            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {

                throw new DbUpdateConcurrencyException();
            }
        }

        public async Task Update(StudentTeacher studentTeacher)
        {
            if (studentTeacher == null) throw new ArgumentNullException("entity");
            await Save();
        }
    }
}
