using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Profile.Application.Contracts;
using Profile.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profile.Infrastructure.Repositories
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly SkillTrackerContext _context;

        public ProfileRepository(SkillTrackerContext dbcontext)
        {
            this._context = dbcontext;
        }

        public async Task<ProfileEntity> AddAsync(ProfileEntity entity)
        {
            await this._context.Profile.AddAsync(entity);
            await this._context.SaveChangesAsync();
            return entity;
        }

        public async Task<ProfileEntity> UpdateAsync(ProfileEntity entity)
        {
            ProfileEntity existingProfile = this._context.Profile.FirstOrDefault(s => s.EmpId == entity.EmpId);
           
            foreach (var skill in entity.skills)
            {
                var exSkill = existingProfile.skills.FirstOrDefault(s=>s.Name.Equals(skill.Name,StringComparison.OrdinalIgnoreCase));
                exSkill.Proficiency = skill.Proficiency;
            }
            existingProfile.LastModifiedDate = DateTime.Now;
            
            this._context.Update(existingProfile);
            await this._context.SaveChangesAsync();
            
            return entity;
        }
    }
}
