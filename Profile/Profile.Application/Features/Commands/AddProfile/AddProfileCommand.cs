using MediatR;
using Profile.Domain.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Profile.Application.Features.Commands.AddProfile
{
    public class AddProfileCommand : IRequest<string>
    {
        [Required(ErrorMessage="Name is required.")]
        [MaxLength(30, ErrorMessage = "Max 30 characters allowed for Name.")]
        public string Name { get; set; }

        [Required(ErrorMessage="Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Assocate Id is required.")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Invalid Assocate Id.")]
        public string EmpId { get; set; }

        [Required(ErrorMessage = "Mobile number is required.")]
        public string Mobile { get; set; }

        [Required(ErrorMessage = "Skills are required.")]
        public List<Skill> Skills { get; set; }
    }
}
