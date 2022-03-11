using Admin.Application.Contracts;
using Admin.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Infrastructure.Repositories
{
   public class ProfileRepository : IProfileRepository
    {
        
        private readonly CosmosDbContext _context;

        public ProfileRepository(CosmosDbContext dbcontext)
        {
            this._context = dbcontext;
        }

        public async Task<ProfileEntity> SearchByEmpIdAsync(string empId)
        {
            return _context.Profile.AsEnumerable().FirstOrDefault(s => s.EmpId.ToLower() == empId.ToLower());
        }

        public async Task<List<ProfileEntity>> GetProfilesByname(string name)
        {
            return _context.Profile.AsEnumerable().Where(s=>s.Name.StartsWith(name, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public async Task<IEnumerable<ProfileEntity>> SearchBySkillName(string skillName)
        {
            return _context.Profile.AsEnumerable().Where(company => company.skills.Any(user => user.Name.Equals(skillName,StringComparison.OrdinalIgnoreCase)));
        }

    }
}
