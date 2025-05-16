using CORE.APP.Domain;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.Users.Domain
{
    public class User: Entity
    {
        [Required(ErrorMessage="{0} is required")]
        [StringLength(250, MinimumLength =3, ErrorMessage = "{0} Must have minimum {2} max {3} Characters")]
        [Length(3,250, ErrorMessage ="{0} must have minimum {1} and maximum {2} characters.")]
        [MaxLength(250, ErrorMessage = "{0} must have minimum {1} characters.")]
        [MinLength(3, ErrorMessage = "{0} must have minimum {1} characters.")]

        // public string Title { get; set; }

        //public string FullName { get; set; }

        //public bool IsActive { get; set; } = true; // Kullanıcı aktif mi?

        //THE REFERENCE TO US

        //public DateTime? RegistrationDate { get; set; }

        // public string Title { get; set; }

        //public string FullName { get; set; }

        //public bool IsActive { get; set; } = true; // Kullanıcı aktif mi?

        //THE REFERENCE TO US

        public DateTime? RegistrationDate { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public bool IsDoctor { get; set; } // Bu kullanıcı doktor mu?
        public double DaysInSystem { get; set; }

        public List<UserSkill> UserSkill { get; private set; } = new List<UserSkill>();



        [NotMapped]
        public List<int> SkillIds
        {
            get => UserSkill.Select(tt => tt.SkillId).ToList();
            set => UserSkill = value?.Select(v => new UserSkill() { SkillId = v }).ToList();
        }

        public int? BranchId { get; set; }
        public Branch Branch { get; set; }
    }
}
