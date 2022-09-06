using Lms.Core.Entities;
using Lms.Core.Repositories;
using Lms.Data.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Data.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly LmsApiContext db = null!;

        public CourseRepository(LmsApiContext db)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
        }
       public void Add(Course course )
        {
            db.Course.Add(course);
        }

        public async Task<bool> AnyAsync(int? id)
        {
            return db.Course.Any(m => m.Id == id);
        }

      public async Task<Course> FindAsync(int? id)
        {
            return await db.Course.FindAsync(id);
        }

        public async Task<IEnumerable<Course>> GetAllCourses(bool includeModules = true)
        {
            return includeModules ? await db.Course
                                       .Include(c => c.Modules)
                                       .ToListAsync() :
                                       await db.Course
                                       .Include(c => c.Modules)
                                       .ToListAsync();
        }

        public async Task<Course?> GetCourse(int id)
        {
            return await db.Course.FindAsync(id);
        }

        public void Remove(Course course)
        {
            db.Course.Remove(course);
        }

        public  void Update(Course course)
        {
            db.Course.Update(course);
        }

        public async Task<Course?> GetCourse(string title, bool includeModules = false)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException($"'{nameof(title)}' cannot be null or whitespace.", nameof(title));
            }

            var query = db.Course
                            .Include(c => c.Modules)
                            .AsQueryable();

            if (includeModules)
            {
                query = query.Include(c => c.Modules);
            }

            return await query.FirstOrDefaultAsync(c => c.Title == title);
        }
    }
}
