using Business.CustomExceptions;
using Business.Interfaces;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Web_API.Controllers
{

    [Route("api/teachers")]
    [Produces("application/json")]
    [ApiController]
    public class TeachersController : Controller
    {
        private readonly IService<Teacher, TeacherDTO> _teacherService;
        public TeachersController(IService<Teacher, TeacherDTO> teacherService)
        {
            _teacherService = teacherService;
        }
        /// <summary>
        /// Shows all teachers
        /// </summary>
        // GET: api/Teachers
        [HttpGet]
        public async Task<ActionResult<List<TeacherDTO>>> GetTeacherList()
        {
            try
            {
                return await _teacherService.GetAll();
            }
            catch(Exception)
            {
                return NotFound();
            }
        }
        /// <summary>
        /// Shows a specified teacher by id
        /// </summary>
        // GET: api/Teachers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TeacherDTO>> GetTeacher(int id)
        {
            TeacherDTO teacher;

            try
            {
                teacher = await _teacherService.GetById(id);
            }
            catch (Exception)
            {
                return NotFound();
            };

            return teacher;
        }
        /// <summary>
        /// Updates the selected teacher by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT/Teacher
        ///          "id": 0,
        ///          "createdAt": "2023-04-28T04:51:07.443Z",
        ///          "name": "Marko",
        ///          "surname": "Markic",
        ///          "yearsOfTeaching": 5,
        ///          "salary": 1000,
        ///          "associate": true,
        ///          "address": "Ulica Stanka Vraza 5"
        ///         }
        ///         
        /// </remarks>
        /// <response code="204">Returns information that the teacher is altered</response>
        /// <response code="404">If the teachers is not found</response>
        // PUT: api/Teachers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateTeacher(int id, TeacherDTO teacherDTO)
        {
            if (id != teacherDTO.Id)
                return BadRequest("Teacher ID mismatch");

            try
            {
                await _teacherService.Update(id, teacherDTO);
            }
            catch (Exception ex)
            {
                if (ex is DBConcurrencyException)
                    return NotFound();
                if (ex is DbUpdateException)
                    return BadRequest();
            }

            return NoContent();
        }
        /// <summary>
        /// Creates a new teacher
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST/User
        ///         {
        ///          "id": 0,
        ///          "createdAt": "2023-04-28T04:51:07.443Z",
        ///          "name": "Marko",
        ///          "surname": "Markic",
        ///          "yearsOfTeaching": 5,
        ///          "salary": 1000,
        ///          "associate": true,
        ///          "address": "Ulica Stanka Vraza 5"
        ///         }
        ///         
        /// </remarks>
        /// <response code="201">Returns the newly created teacher</response>
        /// <response code="400">If the teacher data is not in correct syntax</response>
        // POST: api/Teachers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<TeacherDTO>> CreateTeacher(TeacherDTO teacherDTO)
        {
            if (await _teacherService.Insert(teacherDTO) == null)
                return BadRequest();

            return CreatedAtAction(
                nameof(GetTeacher),
                new { id = teacherDTO.Id },
                teacherDTO);
        }
        /// <summary>
        /// Deletes the specified teacher
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="204">Returns confirmation that teacher is deleted</response>
        /// <response code="404">If the teacher is null</response>
        // DELETE: api/Students/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteTeacher(int id)
        {
            try
            {
                await _teacherService.Delete(id);
            }
            catch (Exception)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
