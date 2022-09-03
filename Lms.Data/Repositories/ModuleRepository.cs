using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lms.Core.Entities;
using Lms.Core.Repositories;
using Lms.Data.Data;
using Microsoft.EntityFrameworkCore;

namespace Lms.Data.Repositories
{
    internal class ModuleRepository: IModuleRepository
    {
        private readonly LmsApiContext db = null!;

        public ModuleRepository(LmsApiContext db)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public void Add(Module module)
        {
            db.Module.Add(module);
        }

        public async Task<bool> AnyAsync(int id)
        {
            return db.Module.Any(m => m.Id == id);
        }

        public async Task<Module> FindAsync(int id)
        {
            return await db.Module.FindAsync(id);
        }

        public async Task<IEnumerable<Module>> GetAllModules()
        {
            return await db.Module.ToListAsync();
        }

        public async Task<Module?> GetModule(int id)
        {
            return await db.Module.FirstOrDefaultAsync(m => m.Id == id);
        }

        public void Remove(Module module)
        {
            db.Module.Remove(module);
        }

        public void Update(Module module)
        {
            db.Module.Update(module);
        }
    }
}
