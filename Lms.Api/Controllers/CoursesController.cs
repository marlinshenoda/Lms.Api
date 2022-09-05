using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lms.Data.Data;
using Lms.Core.Entities;
using Lms.Data.Repositories;
using Lms.Core.Dto;
using Bogus.DataSets;
using AutoMapper;

namespace Lms.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly LmsApiContext db;
        private readonly UnitOfWork uow;
        private readonly IMapper mapper;

        public CoursesController(LmsApiContext context, IMapper mapper)
        {
            db = context;
            uow = new UnitOfWork(db);
            this.mapper = mapper;

        }
  
        // GET: api/Courses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetCourses()
        {
          
                var courses = await uow.CourseRepository.GetAllCourses();
            return Ok(mapper.Map<IEnumerable<CourseDto>>(courses));
        }

        // GET: api/Courses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Course>> GetCourse(int id)
        {
          if (db.Course == null)
          {
              return NotFound();
          }
            var course = await db.Course.FindAsync(id);

            if (course == null)
            {
                return NotFound();
            }

            return course;
        }

        // PUT: api/Courses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCourse(int id, Course course)
        {
            if (id != course.Id)
            {
                return BadRequest();
            }

            db.Entry(course).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Courses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Course>> PostCourse(Course course)
        {
          if (db.Course == null)
          {
              return Problem("Entity set 'LmsApiContext.Course'  is null.");
          }
            db.Course.Add(course);
            await db.SaveChangesAsync();

            return CreatedAtAction("GetCourse", new { id = course.Id }, course);
        }

        // DELETE: api/Courses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            if (db.Course == null)
            {
                return NotFound();
            }
            var course = await db.Course.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            db.Course.Remove(course);
            await db.SaveChangesAsync();

            return NoContent();
        }

        private bool CourseExists(int id)
        {
            return (db.Course?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
