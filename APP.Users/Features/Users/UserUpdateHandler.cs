using CORE.APP.Features;
using MediatR;
using System.ComponentModel.DataAnnotations;
using APP.Users.Domain;
using Microsoft.EntityFrameworkCore;

namespace APP.Users.Features.Users
{
    public class UserUpdateRequest : Request, IRequest<CommandResponse>
    {
        [MaxLength(250, ErrorMessage = "{0} must have maximum {1} characters!")]
        [MinLength(3, ErrorMessage = "{0} must have minimum {1} characters!")]
        public string FullName { get; set; }

        public DateTime? RegistrationDate { get; set; }

        [StringLength(500, ErrorMessage = "{0} must have maximum {1} characters!")]
        public string Email { get; set; }

        public bool IsDoctor { get; set; }

        [Range(0, 1, ErrorMessage = "{0} must be between {1} and {2}")]
        public double DaysInSystem { get; set; }

        public List<int> SkillIds { get; set; }
        public int? BranchId { get; set; }
    }

    public class UserUpdateHandler : UserDbHandler, IRequestHandler<UserUpdateRequest, CommandResponse>
    {
        public UserUpdateHandler(UsersDb db) : base(db)
        {
        }

        public async Task<CommandResponse> Handle(UserUpdateRequest request, CancellationToken cancellationToken)
        {
            if (await _db.User.AnyAsync(t => t.Id != request.Id && t.FullName.ToUpper() == request.FullName.ToUpper().Trim(), cancellationToken))
                return Error("Another user with the same name exists!");

            var entity = await _db.User
                .Include(u => u.UserSkill)
                .SingleOrDefaultAsync(u => u.Id == request.Id, cancellationToken);

            if (entity is null)
                return Error("User does not exist!");

            entity.FullName = request.FullName.Trim();
            entity.Email = request.Email?.Trim();
            entity.RegistrationDate = request.RegistrationDate;
            entity.IsDoctor = request.IsDoctor;
            entity.DaysInSystem = request.DaysInSystem;
            entity.SkillIds = request.SkillIds;
            entity.BranchId = request.BranchId;

            _db.User.Update(entity);
            await _db.SaveChangesAsync(cancellationToken);

            return Success("User updated successfully.", entity.Id);
        }
    }
}
