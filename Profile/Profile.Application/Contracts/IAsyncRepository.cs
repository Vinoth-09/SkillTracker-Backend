using SkillTracker.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Profile.Application.Contracts
{
    public interface IAsyncRepository<T> where T: EntityBase
    {
        Task<T> AddAsync(T entity);

        Task<T> UpdateAsync(T entity);
    }
}
