using CORE.APP.Features;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APP.Users.Domain;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace APP.Users.Features.Users
{

    public class UserCreateRequest : Request, IRequest<CommandResponse>
    {
        //MVC Fluent Validation

        [MaxLength(250, ErrorMessage ="{0} must have maximum {1} characters!")]
        [MinLength(3, ErrorMessage = "{0} must have maximum {1} characters!")]

        public string FullName { get; set; }

        public DateTime? RegistrationDate { get; set; }


        [StringLength(500, ErrorMessage = "{0} must have maximum {1} characters!")]
        public string Email { get; set; }
        public bool IsDoctor { get; set; } // Bu kullanıcı doktor mu?

        [Range(0,1, ErrorMessage = "{0} must be between {1} and {2}")]
        public double DaysInSystem { get; set; }
        public List<int> SkillIds { get; set; }

        [JsonIgnore]

        public override int Id { get => base.Id; set => base.Id = value; }

        public int? BranchId { get; set; }
    }


    public class UserCreateHandler : UserDbHandler, IRequestHandler<UserCreateRequest, CommandResponse>
    {
        public UserCreateHandler(UsersDb db): base(db)
        {

        }

        public async Task<CommandResponse> Handle(UserCreateRequest request, CancellationToken cancellationToken)
        {
            if (await _db.User.AnyAsync(t=>t.FullName.ToUpper()==request.FullName.ToUpper().Trim() && t.DaysInSystem<1))
            {
                return Error("Not registered user");
            }

            var entity = new User()
            {
                DaysInSystem = request.DaysInSystem,
                Email = request.Email.Trim(),
                RegistrationDate = request.RegistrationDate,
                FullName = request.FullName.Trim(),
                SkillIds = request.SkillIds,
                BranchId=request.BranchId

            };

            _db.User.Add(entity);

            await _db.SaveChangesAsync(cancellationToken);

            return Success("User is created succesfully!");
        }
    }
}
