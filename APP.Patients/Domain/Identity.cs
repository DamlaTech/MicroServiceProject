using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CORE.APP.Domain;

namespace APP.Identities.Domain
{
    public class Identity: Entity
    {
        [Required, StringLength(30, MinimumLength =3)]
        public string IdentityName { get; set; }

        [Required, StringLength(10, MinimumLength = 3)]
        public string Password { get; set; }

        public bool IsActive { get; set; }

        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }

        public int RoleId { get; set; }

        public Role Role { get; set; }

        public List<IdentityQualification> IdentityQualifications { get; set; } = new List<IdentityQualification>();

        [NotMapped]
        public List<int> QualificationIds
        {
            get => IdentityQualifications?.Select(us => us.QualificationId).ToList();
            set => IdentityQualifications=value?.Select(v => new IdentityQualification() { QualificationId = v }).ToList();
        }

        //--REFRESH TOKEN
        public string RefreshToken { get; set; }

        public DateTime? RefreshTokenExpiration { get; set; }
    }
}
