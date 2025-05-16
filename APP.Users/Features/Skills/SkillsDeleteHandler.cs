using APP.Users.Domain;
using CORE.APP.Features;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.Users.Features.Skills
{

    public class SkillsDeleteRequest: Request, IRequest<CommandResponse>
    {

    }

    public class SkillsDeleteHandler : UserDbHandler, IRequestHandler<SkillsDeleteRequest, CommandResponse>
    {

        public SkillsDeleteHandler(UsersDb db): base(db)
        {

        }

        public async Task<CommandResponse> Handle(SkillsDeleteRequest request, CancellationToken cancellationToken)
        {
            var entity = await _db.Skills.FindAsync(request.Id, cancellationToken);

            if (entity is null)
                return Error("Skill is not found");

            //way 2
            _db.Skills.Remove(entity);

            await _db.SaveChangesAsync(cancellationToken);

            return Success("Skill is succesfully deleted", entity.Id);
        }
    }
}
