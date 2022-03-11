using FluentValidation;
using Profile.Application.Features.Commands.AddProfile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profile.Application.Features.Commands.UpdateProfile
{
    public class UpdatePorfileCommandValidator : AbstractValidator<UpdateProfileCommand>
    {
        public UpdatePorfileCommandValidator()
        {
            
        }
    }
}
