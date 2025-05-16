using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Microsoft.EntityFrameworkCore;

using APP.Users.Features.Skills;
using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Authorization;

namespace API.Users.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="Admin")]
    public class SkillsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SkillsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //GET: api/Topics
        [HttpGet]
        [HttpGet, AllowAnonymous]
        public async Task<IActionResult> Get()
        {
            IQueryable<SkillsQueryResponse> query= await _mediator.Send(new SkillsQueryRequest());
            List<SkillsQueryResponse> list = await query.ToListAsync();
            if (list.Count > 0)//list.Any()
                return Ok(list);
            return NoContent();
        }


        //GET: api/Topics
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Get(int id)
        {
            var query = await _mediator.Send(new SkillsQueryRequest());
            var item = await query.SingleOrDefaultAsync(t => t.Id == id);

            if (item is not null)
                return Ok(item);
            return NoContent();

        }

        //POST:api/Topics
        [HttpPost]
        public async Task<IActionResult> Post(SkillsCreateRequest request)
        {
            if (ModelState.IsValid)
            {
                var response = await _mediator.Send(request);

                if (response.IsSuccessful)
                    return Ok(response);

                ModelState.AddModelError("SkillsPost", response.Message);
            }

            return BadRequest(ModelState);

        }

        //POST:api/Skills
        [HttpPut]
        public async Task<IActionResult> Put(SkillsUpdateRequest request)
        {
            if (ModelState.IsValid)
            {
                var response = await _mediator.Send(request);
                if (response.IsSuccessful)
                {
                    return Ok(response);

                }  
                ModelState.AddModelError("SkillsPut", response.Message);

            }

                 return BadRequest(ModelState);
               
        }

        //DELETE:api/Skills
        [HttpDelete("{id}")]

        public async Task<ActionResult> Delete(int id)
        {
            var response=await _mediator.Send(new SkillsDeleteRequest { Id = id });

            if (response.IsSuccessful)
                return Ok(response);

            return BadRequest(response);
        }

        
    }
}
