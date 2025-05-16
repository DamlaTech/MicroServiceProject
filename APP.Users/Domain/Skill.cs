using CORE.APP.Domain;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace APP.Users.Domain
{
    public class Skill : Entity
    {
        [Required]
        [StringLength(125)]
        public string Name { get; set; }

        public List<UserSkill> UserSkill { get; set; } = new List<UserSkill>();

        [NotMapped]

        public List<int> UserIds
        {
            get => UserSkill.Select(tt => tt.UserId).ToList();
            set => UserSkill = value.Select(v => new UserSkill() { SkillId = v }).ToList();
        }

    }
}

