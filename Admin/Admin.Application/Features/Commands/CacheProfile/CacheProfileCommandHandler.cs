using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Admin.Application.Contracts;
using Admin.Domain.Models;
using Microsoft.Extensions.Configuration;

namespace Admin.Application.Features.Commands.CacheProfile
{
    public class CacheProfileCommandHandler : IRequestHandler<CacheProfileCommand, string>
    {
        private readonly ICacheRepository _cacheRepo;
        private readonly AutoMapper.IMapper _mapper;
        private readonly ILogger<CacheProfileCommandHandler> _logger;

        private readonly bool _cacheEnabled;

        public CacheProfileCommandHandler(
            ICacheRepository cacheRepo,
            AutoMapper.IMapper mapper,
            ILogger<CacheProfileCommandHandler> logger,
            IConfiguration configuration)
        {
            _cacheRepo = cacheRepo;
            _mapper = mapper;
            _logger = logger;

            bool.TryParse(configuration["CacheEnabled"], out _cacheEnabled);
        }

        public async Task<string> Handle(CacheProfileCommand request, CancellationToken cancellationToken)
        {
            if (_cacheEnabled)
            {
                var profile = _mapper.Map<Profile>(request);

                await _cacheRepo.SetAsync<object>(profile.EmpId, profile);

                _logger.LogInformation($"Profile {profile.EmpId} is successfully cached.");

                return profile.EmpId;
            }
            else
            {
                _logger.LogInformation($"Cached not enabled.");
                return request.EmpId;
            }
        }
    }
}
