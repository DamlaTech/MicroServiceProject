using APP.Users.Domain;
using CORE.APP.Features;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace APP.Users.Features.Skills
{
    public class SkillsUpdateRequest : Request, IRequest<CommandResponse>
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        public int Id { get; set; }
    }

    public class SkillsUpdateHandler : UserDbHandler, IRequestHandler<SkillsUpdateRequest, CommandResponse>
    {
        public SkillsUpdateHandler(UsersDb db) : base(db)
        {
        }

        public async Task<CommandResponse> Handle(SkillsUpdateRequest request, CancellationToken cancellationToken)
        {
            if (await _db.Skills.AnyAsync(t => t.Id != request.Id && t.Name.ToLower() == request.Name.ToLower().Trim(), cancellationToken))
                return Error("Error with the same name exist");

            // Way 3
            var entity = await _db.Skills.FindAsync(new object[] { request.Id }, cancellationToken);

            if (entity is null)
                return Error("Skill is not found");

            entity.Name = request.Name.Trim();

            _db.Skills.Update(entity);
            await _db.SaveChangesAsync(cancellationToken);
            return Success("Skills update successful", entity.Id);
        }
    }
}
