using APP.Users.Domain;
using CORE.APP.Features;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace APP.Users.Features.Branches
{
    public class BranchCreateRequest : Request, IRequest<CommandResponse>
    {
        [JsonIgnore]
        public override int Id { get => base.Id; set => base.Id = value; }

        [Required, StringLength(100)]
        public string Name { get; set; }
    }

    public class BranchCreateHandler : UserDbHandler, IRequestHandler<BranchCreateRequest, CommandResponse>
    {
        public BranchCreateHandler(UsersDb db) : base(db)
        {
        }

        public async Task<CommandResponse> Handle(BranchCreateRequest request, CancellationToken cancellationToken)
        {
            if (await _db.Branches.AnyAsync(b => b.Name.ToUpper() == request.Name.ToUpper().Trim(), cancellationToken))
                return Error("Branch with the same name already exists!");

            var entity = new Branch()
            {
                Name = request.Name?.Trim()
            };

            _db.Branches.Add(entity);
            await _db.SaveChangesAsync(cancellationToken);

            return Success("Branch created successfully.", entity.Id);
        }
    }
}
