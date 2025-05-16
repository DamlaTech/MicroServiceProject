using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace APP.Users.Domain
{
    public class UsersDb : DbContext
    {
        public DbSet<Skill> Skills { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<UserSkill> UserSkills { get; set; } 

        public DbSet<Branch> Branches { get; set; }

        public UsersDb(DbContextOptions<UsersDb> options) : base(options)
        {
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = "UsersDb.db" };
            var connectionString = connectionStringBuilder.ToString();
            var connection = new SqliteConnection(connectionString);

            optionsBuilder.UseSqlite(connection);
        }
    }
}
