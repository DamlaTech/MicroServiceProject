using APP.Identities.Domain;
using CORE.APP.Features;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using MediatR;
using System.Globalization;
using APP.Identities.Features.Roles;
using Microsoft.EntityFrameworkCore;

namespace APP.Identities.Features.Identities
{
    public class IdentityQueryRequest : Request, IRequest<IQueryable<IdentityQueryResponse>>
    {
        [JsonIgnore]
        public override int Id { get => base.Id; set => base.Id = value; }

        [Required, StringLength(30, MinimumLength = 3)]
        public string IdentityName { get; set; }

        [Required, StringLength(10, MinimumLength = 3)]
        public string Password { get; set; }

    }

    public class IdentityQueryResponse : QueryResponse
    {
        public string IdentityName { get; set; }

        public string Password { get; set; }

        public bool IsActive { get; set; }

        public String Active { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName { get; set; }

        public int RoleId { get; set; }

        //Way 1 for role
        public string RoleName { get; set; }

        //For Qualifications
        public List<int> QualificationIds { get; set; }

        //Way 1 qualname
        public string QualificationNames { get; set; }

        public List<IdentityQualification> IdentityQualifications { get; set; } = new List<IdentityQualification>();
    }

    public class IdentityQueryHandler : IdentityDbHandler, IRequestHandler<IdentityQueryRequest, IQueryable<IdentityQueryResponse>>
    {
        public IdentityQueryHandler(IdentitiesDb db) : base(db)
        {
        }
        public Task<IQueryable<IdentityQueryResponse>> Handle(IdentityQueryRequest request, CancellationToken cancellationToken)
        {
            var entityQuery = _db.Identities.Include(u => u.Role).Include(u => u.IdentityQualifications).ThenInclude(us => us.Qualification)
               .OrderByDescending(u => u.IsActive).ThenBy(u => u.IdentityName).AsQueryable();

            // projection:
            var query = entityQuery.Select(u => new IdentityQueryResponse()
            {
                Active = u.IsActive ? "Active" : "Not Active",
                FirstName = u.FirstName,
                FullName = u.FirstName + " " + u.LastName,
                Id = u.Id,
                IsActive = u.IsActive,
                LastName = u.LastName,
                IdentityName = u.IdentityName,
                Password = u.Password,
                RoleId = u.Role.Id,

                // Way 1:
                RoleName = u.Role.Name,

                // Way 1:
                QualificationNames = string.Join(", ", u.IdentityQualifications.Select(us => us.Qualification.Name)),


            });

            return Task.FromResult(query);
        }
    }
}