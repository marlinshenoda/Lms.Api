using Lms.Core.Repositories;
using Lms.Data.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private LmsApiContext db;
        public ICourseRepository CourseRepository { get; }

        public IModuleRepository ModuleRepository { get; }

        public UnitOfWork(LmsApiContext db)
        {
            this.db = db;
            this.CourseRepository = new CourseRepository(db);
            this.ModuleRepository = new ModuleRepository(db);
        }

        public async Task CompleteAsync()
        {
            await db.SaveChangesAsync();
        }
    }
}
