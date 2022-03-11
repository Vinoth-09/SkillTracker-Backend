using Admin.Application.Contracts;
using Admin.Application.Features.Queries.SearchQuery;
using Admin.Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SkillTracker.Entities;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Application.Services
{
    public class SearchService : ISearchService
    {
        private readonly IProfileRepository _profileRepository;
        private readonly ICacheProvider _cacheProvider;
        private readonly ICacheRepository _cacheRepo;
        private readonly AutoMapper.IMapper _mapper;
        private readonly ILogger<SearchService> _logger;

        private readonly bool _cacheEnabled;

        public SearchService(
            ICacheProvider cacheProvider,
            ICacheRepository cacheRepo,
            AutoMapper.IMapper mapper,
            ILogger<SearchService> logger,
            IConfiguration configuration,
            IProfileRepository profileRepository
        )
        {
            _cacheProvider = cacheProvider;
            _cacheRepo = cacheRepo;
            _mapper = mapper;
            _logger = logger;
            _profileRepository = profileRepository;

            bool.TryParse(configuration["CacheEnabled"], out _cacheEnabled);
        }

        public async Task<IEnumerable<Profile>> Search(SearchProfileQuery query)
        {
            if (!string.IsNullOrWhiteSpace(query.EmpId))
            {
                var response = new List<Profile>();
                var profile =  await SearchById(query.EmpId);
                if (profile != null)
                {
                    response.Add(profile);
                }
                return response;
            }
            else if (!string.IsNullOrWhiteSpace(query.Name))
            {
                return await SearchByName(query.Name);
            }
            else if (!string.IsNullOrWhiteSpace(query.Skill))
            {
                return await SearchBySkillName(query.Skill);
            }
            return new List<Profile>();
        }

        private async Task<Profile> SearchById(string empId)
        {
            var cachedData = GetFromCache(empId);
            if (cachedData != null)
            {
                return cachedData;
            }

            var profile = _mapper.Map<Profile>(await _profileRepository.SearchByEmpIdAsync(empId));
            
            if (profile != null)
            {
                await SetToCache(empId, profile);
                return profile;
            }
            return null;
        }
        private async Task<IEnumerable<Profile>> SearchByName(string name)
        {

            var profileData = await _profileRepository.GetProfilesByname(name);

            List<Profile> profiles = new List<Profile>();
            foreach (var profile in profileData)
            {
                profiles.Add(_mapper.Map<Profile>(profile));
            }

            return profiles;
        }

        private async Task<IEnumerable<Profile>> SearchBySkillName(string skillName)
        {
            var profileData = await _profileRepository.SearchBySkillName(skillName);

            List<Profile> profiles = new List<Profile>();
            foreach (var profile in profileData)
            {
                profiles.Add(_mapper.Map<Profile>(profile));
            }

            return profiles;
        }

        private Profile GetFromCache(string key)
        {
            return _cacheEnabled ? _cacheProvider.GetCache<Profile>(key) : null;
        }

        private async Task SetToCache(string key, Profile data)
        {
            if (_cacheEnabled)
            {
                await _cacheRepo.SetAsync<Profile>(key, data);
            }
        }
    }
}
