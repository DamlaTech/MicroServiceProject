using APP.Users.Domain;
using CORE.APP.Features;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace APP.Users.Features.Branches
{
    public class BranchUpdateRequest : Request, IRequest<CommandResponse>
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
    }

    public class BranchUpdateHandler : UserDbHandler, IRequestHandler<BranchUpdateRequest, CommandResponse>
    {
        public BranchUpdateHandler(UsersDb db) : base(db)
        {
        }

        public async Task<CommandResponse> Handle(BranchUpdateRequest request, CancellationToken cancellationToken)
        {
            // Aynı isimle başka bir branch varsa hata döner
            if (await _db.Branches.AnyAsync(b => b.Id != request.Id && b.Name.ToUpper() == request.Name.ToUpper().Trim(), cancellationToken))
                return Error("Branch with the same name already exists!");

            var entity = await _db.Branches.FindAsync(new object[] { request.Id }, cancellationToken);

            if (entity is null)
                return Error("Branch not found!");

            entity.Name = request.Name?.Trim();
            _db.Branches.Update(entity);
            await _db.SaveChangesAsync(cancellationToken);

            return Success("Branch updated successfully.", entity.Id);
        }
    }
}
