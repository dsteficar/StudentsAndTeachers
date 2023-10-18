using Business.CustomExceptions;
using Business.Interfaces;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Web_API.Controllers
{
    [Route("api/students")]
    [Produces("application/json")]
    [ApiController]
    public class StudentsController : Controller
    {

        private readonly IService<Student, StudentDTO> _service;
        public StudentsController(IService<Student, StudentDTO> service)
        {
            _service = service;
        }
        /// <summary>
        /// Shows all users
        /// </summary>
        // GET: api/Students
        [HttpGet]
        public async Task<ActionResult<List<StudentDTO>>> GetStudentList()
        {
            try
            {
                return await _service.GetAll();
            }
            catch (Exception) { 
                return NotFound();
            }
        }
        /// <summary>
        /// Shows a specified user by id
        /// </summary>
        // GET: api/Students/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentDTO>> GetStudent(int id)
        {
            StudentDTO student;

            try
            {
                student = await _service.GetById(id);
            }
            catch (Exception)
            {
                return NotFound();
            };

            return student;
        }
        /// <summary>
        /// Updates the selected user by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT/Student
        ///         {
        ///         "id" : 1,
        ///         "fullname" : "Marko Markic",
        ///         "number" : "098123456"
        ///         }
        ///         
        /// </remarks>
        /// <response code="204">Returns information that the user is altered</response>
        /// <response code="404">If the user is not found</response>
        // PUT: api/Students/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateStudent(int id, StudentDTO studentDTO)
        {
            if (id != studentDTO.Id)
                return BadRequest("User ID mismatch");

            try
            {
                await _service.Update(id, studentDTO);
            }
            catch (Exception ex)
            {
                if (ex is UserNotFoundException)
                    return NotFound();
                if (ex is DbUpdateException)
                    return BadRequest();
            }

            return NoContent();
        }
        /// <summary>
        /// Creates a new user
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST/User
        ///         {
        ///         "fullname" : "Marko Markic",
        ///         "number" : "098123456"
        ///         }
        ///         
        /// </remarks>
        /// <response code="201">Returns the newly created user</response>
        /// <response code="400">If the user data is not in correct syntax</response>
        // POST: api/Students
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<StudentDTO>> CreateStudent(StudentDTO studentDTO)
        {
            if (await _service.Insert(studentDTO) == null)
                return BadRequest();

            return CreatedAtAction(
                nameof(GetStudent),
                new { id = studentDTO.Id },
                studentDTO);
        }
        /// <summary>
        /// Deletes the specified user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="204">Returns confirmation that user is deleted</response>
        /// <response code="404">If the user is null</response>
        // DELETE: api/Students/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteStudent(int id)
        {
            try
            {
                await _service.Delete(id);
            }
            catch (Exception)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
