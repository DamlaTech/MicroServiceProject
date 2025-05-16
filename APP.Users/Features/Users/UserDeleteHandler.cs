using APP.Users.Domain;
using CORE.APP.Features;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace APP.Users.Features.Users
{
    public class UserDeleteRequest : Request, IRequest<CommandResponse>
    {

    }

    public class UserDeleteHandler : UserDbHandler, IRequestHandler<UserDeleteRequest, CommandResponse>
    {
        public UserDeleteHandler(UsersDb db) : base(db)
        {
        }

        public async Task<CommandResponse> Handle(UserDeleteRequest request, CancellationToken cancellationToken)
        {
            var entity = await _db.User
                .Include(u => u.UserSkill)
                .SingleOrDefaultAsync(u => u.Id == request.Id, cancellationToken);

            if (entity is null)
                return Error("User not found!");

            _db.UserSkills.RemoveRange(entity.UserSkill); // ilişkili verileri temizle

            _db.User.Remove(entity); // kullanıcıyı sil
            await _db.SaveChangesAsync(cancellationToken);

            return Success("User deleted successfully.", entity.Id);
        }
    }
}
