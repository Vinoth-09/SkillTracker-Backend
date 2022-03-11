using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Profile.Application.Contracts;
using Profile.Domain.Entities;
using SkillTracker.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Profile.Application.Features.Commands.UpdateProfile
{
    public class UpdateProfileCommandHandler : IRequestHandler<UpdateProfileCommand, string>
    {
        private readonly IProfileRepository _profileRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateProfileCommandHandler> _logger;

        public UpdateProfileCommandHandler( IMapper mapper, ILogger<UpdateProfileCommandHandler> logger, IProfileRepository profileRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _profileRepository = profileRepository;
        }


        public async Task<string> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
        {
            ProfileEntity profileInfo = new ProfileEntity
            {
                EmpId = request.EmpId,
                skills = request.Skills
            };
            await _profileRepository.UpdateAsync(profileInfo);

            _logger.LogInformation($"Profile {request.EmpId} is successfully updated.");

            return request.EmpId;
        }
    }
}
