using Profile.Application.Features.Commands.AddProfile;
using EventBus.Messaging.Events;
using SkillTracker.Entities;
using Profile.Domain.Entities;

namespace Profile.Application.Mappings
{
    public class MappingProfile : AutoMapper.Profile
    {
        public MappingProfile()
        {
            CreateMap<PersonalInfoEntity, AddProfileCommand>().ReverseMap(); 
            CreateMap<AddProfileEvent, AddProfileCommand>().ReverseMap();
            CreateMap<ProfileEntity, AddProfileCommand>().ReverseMap();
            CreateMap<SkillEntity, Skill>().ReverseMap();
        }
    }
}
