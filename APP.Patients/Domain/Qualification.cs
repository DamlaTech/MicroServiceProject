using CORE.APP.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.Identities.Domain
{
    public class Qualification: Entity
    {
        [Required]
        public string Name { get; set; }

        public List<IdentityQualification> IdentityQualifications { get; set; } = new List<IdentityQualification>();

        //When a skill is created we can assign users to that skill
        [NotMapped]
        public List<int> IdentityIds
        {
            get => IdentityQualifications?.Select(us => us.IdentityId).ToList();
            set => IdentityQualifications = value?.Select(v => new IdentityQualification() { IdentityId = v }).ToList();
        }
    }
}
