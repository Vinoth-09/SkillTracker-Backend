using System.ComponentModel.DataAnnotations;

namespace Profile.Domain.Entities
{
    public class Skill
    {
        //[Required(ErrorMessage = "Skill Id is required")]
        //public int SkillId { get; set; }

        public bool IsTechnical { get; set; }

        [Required(ErrorMessage = "Skill Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Proficiency is required")]
        [Range(1, 20, ErrorMessage = "Invalid Proficiency")]
        public int Proficiency { get; set; }
    }
}
