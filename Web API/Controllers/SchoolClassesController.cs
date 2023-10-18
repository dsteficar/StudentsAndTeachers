using Business.CustomExceptions;
using Business.Interfaces;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Web_API.Controllers
{
    [Route("api/schoolclasses")]
    [Produces("application/json")]
    [ApiController]
    public class SchoolClassesController : Controller
    {
        private readonly IService<SchoolClass, SchoolClassDTO> _service;
        public SchoolClassesController(IService<SchoolClass, SchoolClassDTO> service)
        {
            _service = service;
        }
        /// <summary>
        /// Shows all users
        /// </summary>
        // GET: api/Students
        [HttpGet]
        public async Task<ActionResult<List<SchoolClassDTO>>> GetSchoolClassList()
        {
            try
            {
                return await _service.GetAll();
            }catch(Exception) {
                return NotFound();    
            }
        }
        /// <summary>
        /// Shows a specified user by id
        /// </summary>
        // GET: api/Students/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SchoolClassDTO>> GetSchoolClass(int id)
        {
            SchoolClassDTO schoolClassDTO;

            try
            {
                schoolClassDTO = await _service.GetById(id);
            }
            catch (Exception)
            {
                return NotFound();
            };

            return schoolClassDTO;
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
        public async Task<ActionResult> UpdateSchoolClass(int id, SchoolClassDTO schoolClassDTO)
        {
            if (id != schoolClassDTO.Id)
                return BadRequest("User ID mismatch");

            try
            {
                await _service.Update(id, schoolClassDTO);
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
        public async Task<ActionResult<SchoolClassDTO>> CreateSchoolClass(SchoolClassDTO schoolClassDTO)
        {
            if (await _service.Insert(schoolClassDTO) == null)
                return BadRequest();

            return CreatedAtAction(
                nameof(CreateSchoolClass),
                new { id = schoolClassDTO.Id },
                schoolClassDTO);
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
        public async Task<ActionResult> DeleteSchoolClass(int id)
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
