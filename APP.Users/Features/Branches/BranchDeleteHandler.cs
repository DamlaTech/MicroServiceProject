using APP.Users.Domain;
using CORE.APP.Features;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace APP.Users.Features.Branches
{
    public class BranchDeleteRequest : Request, IRequest<CommandResponse>
    {
    }

    public class BranchDeleteHandler : UserDbHandler, IRequestHandler<BranchDeleteRequest, CommandResponse>
    {
        public BranchDeleteHandler(UsersDb db) : base(db)
        {
        }

        public async Task<CommandResponse> Handle(BranchDeleteRequest request, CancellationToken cancellationToken)
        {
            var entity = await _db.Branches
                .Include(b => b.Users)
                .SingleOrDefaultAsync(b => b.Id == request.Id, cancellationToken);

            if (entity is null)
                return Error("Branch not found!");

            if (entity.Users.Any())
                return Error("Branch can't be deleted because it has associated users!");

            _db.Branches.Remove(entity);
            await _db.SaveChangesAsync(cancellationToken);

            return Success("Branch deleted successfully.", entity.Id);
        }
    }
}
