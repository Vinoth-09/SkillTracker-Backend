using MediatR;
using Profile.Domain.Entities;
using System.Collections.Generic;

namespace Profile.Application.Features.Commands.UpdateProfile
{
    public class UpdateProfileCommand : IRequest<string>
    {
        public string EmpId { get; set; }

        public List<Skill> Skills { get; set; }
    }
}
