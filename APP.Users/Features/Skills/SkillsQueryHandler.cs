using CORE.APP.Features;
using APP.Users.Features;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using APP.Users.Domain;
using APP.Users.Features.Users;
using System.Globalization;
using Microsoft.EntityFrameworkCore;


namespace APP.Users.Features.Skills
{
    public class SkillsQueryRequest : Request, IRequest<IQueryable<SkillsQueryResponse>>
    {
        // Gerekirse filtreleme için ek özellikler ekleyebilirsin.
    }

    public class SkillsQueryResponse : QueryResponse
    {
        public int Id { get; set; }  // Eğer QueryResponse'da Id yoksa ekleyebilirsin.
        public string Name { get; set; }
    }

    public class SkillsQueryHandler : UserDbHandler, IRequestHandler<SkillsQueryRequest, IQueryable<SkillsQueryResponse>>
    {
        public SkillsQueryHandler(UsersDb db) : base(db)
        {
        }

        public  Task<IQueryable<SkillsQueryResponse>> Handle(SkillsQueryRequest request, CancellationToken cancellationToken)
        {
            IQueryable<SkillsQueryResponse> query = _db.Skills.OrderBy(t => t.Name).Select(t => new SkillsQueryResponse()
            {
                Id = t.Id,
                Name = t.Name
            });


            return Task.FromResult(query);

        }
    }
}
