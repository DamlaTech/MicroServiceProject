using APP.Users.Domain;
using CORE.APP.Features;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace APP.Users.Features.Skills
{

    public class SkillsCreateRequest:Request,IRequest<CommandResponse>
    {
        [Required]
        [StringLength(100)]

        public string Name { get; set; }
    }
    class SkillsCreateHandler : UserDbHandler, IRequestHandler<SkillsCreateRequest, CommandResponse>
    {
        public SkillsCreateHandler(UsersDb db) : base(db)
        {

        }

        public async Task<CommandResponse> Handle(SkillsCreateRequest request, CancellationToken cancellationToken)
        {
            if (_db.Skills.Any())
                if (await _db.Skills.AnyAsync(t => t.Name.ToUpper() == request.Name.ToUpper().Trim()))
                    return Error("Skills with the same name exist!");

            var entity = new Skill()
            {
                Name = request.Name.Trim()
            };

            _db.Skills.Add(entity);
            await _db.SaveChangesAsync(cancellationToken);
            return Success("Skill created succesfully");
    
        }

    }
}
