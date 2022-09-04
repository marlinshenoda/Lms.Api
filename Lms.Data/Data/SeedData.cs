using Bogus;
using Lms.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Data.Data
{
    public class SeedData
    {
        private static LmsApiContext db;
        private static Faker faker = null!;

        public static async Task InitAsync(LmsApiContext context)
        {
            if (context is null) throw new ArgumentNullException(nameof(context));
            db = context;
            if (await db.Course.AnyAsync()) return;

            faker = new Faker("sv");


            var courses = GenerateCourses(30);
            await db.AddRangeAsync(courses);
          

            await db.SaveChangesAsync();

        }
        private static IEnumerable<Course> GenerateCourses(int numberOfCourses)
        {
            var courses = new List<Course>();

            for (int i = 0; i < numberOfCourses; i++)
            {
              
                courses.Add(new Course
                {
                Title = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(faker.Company.Bs()),

                StartDate = DateTime.Now.AddDays(faker.Random.Int(-5, 5)),
                Modules = new Module[]
                  {
                        new Module
                        {
                            Title = faker.Finance.AccountName(),
                            StartDate = DateTime.Now.AddDays(faker.Random.Int(-5, 5)),
                           
                        },
                        new Module
                        {
                            Title = faker.Finance.AccountName(),
                            StartDate = DateTime.Now.AddDays(faker.Random.Int(-5, 5)),
                         
                        }
                  }
                });


                db.AddRange(courses);
            }

            return courses;
        }

    }
}
