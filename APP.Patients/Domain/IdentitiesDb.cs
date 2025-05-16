using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.Identities.Domain
{
    public class IdentitiesDb : DbContext
    {
        public DbSet<Identity> Identities { get; set; }

        public DbSet<Qualification> Qualifications { get; set; }

        public DbSet<IdentityQualification> IdentityQualifications { get; set; }

        public IdentitiesDb(DbContextOptions options): base(options)
        {

        }


    }
}
