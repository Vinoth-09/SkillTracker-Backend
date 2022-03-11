using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Profile.Application.Contracts;
using System.Threading;
using System.Threading.Tasks;
using SkillTracker.Entities;
using System.Linq;
using EventBus.Messaging.Events;
using Profile.Domain.Entities;

namespace Profile.Application.Features.Commands.AddProfile
{
    public class AddProfileCommandHandler : IRequestHandler<AddProfileCommand, string>
    {
        private readonly IProfileRepository _profileRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<AddProfileCommandHandler> _logger;

        public AddProfileCommandHandler(
            IMapper mapper,
            ILogger<AddProfileCommandHandler> logger,
            IProfileRepository profileRepository
         )
        {
            _mapper = mapper;
            _logger = logger;
            _profileRepository = profileRepository;
        }

        public async Task<string> Handle(AddProfileCommand request, CancellationToken cancellationToken)
        {
            var userId = await SaveProfileInfo(request);

            _logger.LogInformation($"Profile {request.EmpId} is successfully created.");

            return userId;
        } 

        private async Task<string> SaveProfileInfo(AddProfileCommand request)
        {
            var profileInfo = _mapper.Map<ProfileEntity>(request);
            profileInfo.UserId = $"user{profileInfo.EmpId.ToUpper().Replace("CTS", "")}";
            profileInfo.CreatedDate = System.DateTime.UtcNow;
            profileInfo.LastModifiedDate = System.DateTime.UtcNow;

            await _profileRepository.AddAsync(profileInfo);

            return profileInfo.UserId;
        }
    }
}
