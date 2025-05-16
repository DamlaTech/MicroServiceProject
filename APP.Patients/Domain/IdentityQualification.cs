using CORE.APP.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.Identities.Domain
{
    public class IdentityQualification: Entity//user skill
    {
        public int IdentityId { get; set; }

        public Identity Identity { get; set; }

        public int QualificationId { get; set; }

        public Qualification Qualification { get; set; }

    }
}
