using APP.Users.Domain;
using CORE.APP.Features;
using MediatR;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using APP.Users.Features.Branches;

namespace APP.Users.Features.Users
{

    public class UserQueryRequest : Request, IRequest<IQueryable<UserQueryResponse>>
    {

    }

    public class UserQueryResponse : QueryResponse
    {
        // public string Title { get; set; }

        //public string FullName { get; set; }

        //public bool IsActive { get; set; } = true; // Kullanıcı aktif mi?

        //THE REFERENCE TO US

        public DateTime? RegistrationDate { get; set; }

        public string RegistrationDateF { get; set; }//For the formatted data

        public string FullName { get; set; }
        public string Email { get; set; }
        public bool IsDoctor { get; set; } // Bu kullanıcı doktor mu?
        public double DaysInSystem { get; set; }

        public String DaysInSystemF { get; set; }//Formatted
        public List<int> SkillIds { get; set; }

        public string SkillNames { get; set; }

        public BranchQueryResponse Branch { get; set; }

    }
    public class UserQueryHandler:UserDbHandler, IRequestHandler<UserQueryRequest, IQueryable<UserQueryResponse>>
    {
        public UserQueryHandler(UsersDb db) : base(db)
        {

        }

        public Task<IQueryable<UserQueryResponse>> Handle(UserQueryRequest request, CancellationToken cancellationToken)
        {
            var entityQuery = _db.User
            .Include(u => u.UserSkill)
                .ThenInclude(us => us.Skill)
            .OrderBy(u => u.FullName)
            .AsQueryable();

            var query = entityQuery.Select(t => new UserQueryResponse
            {
                Id = t.Id,
                FullName = t.FullName,
                Email = t.Email,
                IsDoctor = t.IsDoctor,
                RegistrationDate = t.RegistrationDate,
                RegistrationDateF = t.RegistrationDate.HasValue
                    ? t.RegistrationDate.Value.ToString("MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture)
                    : string.Empty,
                DaysInSystem = t.DaysInSystem,
                DaysInSystemF = t.DaysInSystem.ToString("N2", CultureInfo.InvariantCulture),

                // "TopicIds" = "SkillIds" mantığı
                SkillIds = t.UserSkill.Select(us => us.SkillId).ToList(),

                // "TopicNames" = "SkillNames" mantığı
                SkillNames = string.Join(", ", t.UserSkill.Select(us => us.Skill.Name)),

                Branch=t.Branch==null ? null: new BranchQueryResponse()
                {
                    Name = t.Branch.Name,
                    Id=t.Branch.Id
                }
            });

            return Task.FromResult(query);

        }
    }
}
