using Admin.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Application.Contracts
{
   public interface IProfileRepository
    {
        Task<ProfileEntity> SearchByEmpIdAsync(string empId);

        Task<List<ProfileEntity>> GetProfilesByname(string name);

        Task<IEnumerable<ProfileEntity>> SearchBySkillName(string skillName);
    }
}
