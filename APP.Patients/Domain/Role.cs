using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CORE.APP.Domain;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.Identities.Domain
{
    public class Role : Entity
    {
        [Required, StringLength(10)]
        public string Name { get; set; }
        public List<Identity> identities { get; set; } = new List<Identity>();
    }
}
