using APP.Users.Domain;
using APP.Users.Features.Users;
using CORE.APP.Features;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace APP.Users.Features.Branches
{
    public class BranchQueryRequest : Request, IRequest<IQueryable<BranchQueryResponse>>
    {
    }

    public class BranchQueryResponse : QueryResponse
    {
        public string Name { get; set; }
        public string UserNames { get; set; }
        public List<string> UserNameList { get; set; }
        public int UserCount { get; set; }
        public List<UserQueryResponse> Users { get; set; }
    }

    public class BranchQueryHandler : UserDbHandler, IRequestHandler<BranchQueryRequest, IQueryable<BranchQueryResponse>>
    {
        public BranchQueryHandler(UsersDb db) : base(db)
        {
        }

        public Task<IQueryable<BranchQueryResponse>> Handle(BranchQueryRequest request, CancellationToken cancellationToken)
        {
            var query = _db.Branches
                .Include(b => b.Users)
                .OrderBy(b => b.Name)
                .Select(b => new BranchQueryResponse()
                {
                    Id = b.Id,
                    Name = b.Name,
                    UserCount = b.Users.Count,
                    UserNames = string.Join(", ", b.Users.Select(u => u.FullName)),
                    UserNameList = b.Users.Select(u => u.FullName).ToList(),
                    Users = b.Users.Select(u => new UserQueryResponse()
                    {
                        Id = u.Id,
                        FullName = u.FullName,
                        Email = u.Email
                    }).ToList()
                });

            return Task.FromResult(query);
        }
    }
}
