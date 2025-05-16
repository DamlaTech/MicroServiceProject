#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MediatR;
using CORE.APP.Features;
using APP.Identities.Features.Identities;
using Microsoft.AspNetCore.Authorization;

//Generated from Custom Template.
namespace API.Identities.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class IdentitiesController : ControllerBase
    {
        private readonly ILogger<IdentitiesController> _logger;
        private readonly IMediator _mediator;

        public IdentitiesController(ILogger<IdentitiesController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        // GET: api/Identities
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var response = await _mediator.Send(new IdentityQueryRequest());
                var list = await response.ToListAsync();
                if (list.Any())
                    return Ok(list);
                return NoContent();
            }
            catch (Exception exception)
            {
                _logger.LogError("IdentitiesGet Exception: " + exception.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new CommandResponse(false, "An exception occured during IdentitiesGet.")); 
            }
        }

        // GET: api/Identities/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var response = await _mediator.Send(new IdentityQueryRequest());
                var item = await response.SingleOrDefaultAsync(r => r.Id == id);
                if (item is not null)
                    return Ok(item);
                return NoContent();
            }
            catch (Exception exception)
            {
                _logger.LogError("IdentitiesGetById Exception: " + exception.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new CommandResponse(false, "An exception occured during IdentitiesGetById.")); 
            }
        }


        [HttpPost, Route("/api/[action]")] // api/Token
        [AllowAnonymous]
        public async Task<IActionResult> Token(TokenRequest request)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _mediator.Send(request);
                    if (response.IsSuccessful)
                        return Ok(response);
                    ModelState.AddModelError("UsersToken", response.Message);
                }
                return BadRequest(new CommandResponse(false, string.Join("|", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage))));
            }
            catch (Exception exception)
            {
                _logger.LogError("UsersToken Exception: " + exception.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new CommandResponse(false, "An exception occured during UsersToken."));
            }
        }

        //[HttpPost, Route("/api/[action]"), AllowAnonymous] // api/RefreshToken
        //public async Task<IActionResult> RefreshToken(RefreshTokenRequest request)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var response = await _mediator.Send(request);
        //        if (response.IsSuccessful)
        //            return Ok(response);
        //        ModelState.AddModelError("UsersRefreshToken", response.Message);
        //    }
        //    return BadRequest(new CommandResponse(false, string.Join("|", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage))));
        //}


        //// POST: api/Identities
        //      [HttpPost]
        //      public async Task<IActionResult> Post(IdentityCreateRequest request)
        //      {
        //          try
        //          {
        //              if (ModelState.IsValid)
        //              {
        //                  var response = await _mediator.Send(request);
        //                  if (response.IsSuccessful)
        //                  {
        //                      //return CreatedAtAction(nameof(Get), new { id = response.Id }, response);
        //                      return Ok(response);
        //                  }
        //                  ModelState.AddModelError("IdentitiesPost", response.Message);
        //              }
        //              return BadRequest(new CommandResponse(false, string.Join("|", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage))));
        //          }
        //          catch (Exception exception)
        //          {
        //              _logger.LogError("IdentitiesPost Exception: " + exception.Message);
        //              return StatusCode(StatusCodes.Status500InternalServerError, new CommandResponse(false, "An exception occured during IdentitiesPost.")); 
        //          }
        //      }

        //      // PUT: api/Identities
        //      [HttpPut]
        //      public async Task<IActionResult> Put(IdentityUpdateRequest request)
        //      {
        //          try
        //          {
        //              if (ModelState.IsValid)
        //              {
        //                  var response = await _mediator.Send(request);
        //                  if (response.IsSuccessful)
        //                  {
        //                      //return NoContent();
        //                      return Ok(response);
        //                  }
        //                  ModelState.AddModelError("IdentitiesPut", response.Message);
        //              }
        //              return BadRequest(new CommandResponse(false, string.Join("|", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage))));
        //          }
        //          catch (Exception exception)
        //          {
        //              _logger.LogError("IdentitiesPut Exception: " + exception.Message);
        //              return StatusCode(StatusCodes.Status500InternalServerError, new CommandResponse(false, "An exception occured during IdentitiesPut.")); 
        //          }
        //      }

        //// DELETE: api/Identities/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    try
        //    {
        //        var response = await _mediator.Send(new IdentityDeleteRequest() { Id = id });
        //        if (response.IsSuccessful)
        //        {
        //            //return NoContent();
        //            return Ok(response);
        //        }
        //        ModelState.AddModelError("IdentitiesDelete", response.Message);
        //        return BadRequest(new CommandResponse(false, string.Join("|", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage))));
        //    }
        //    catch (Exception exception)
        //    {
        //        _logger.LogError("IdentitiesDelete Exception: " + exception.Message);
        //        return StatusCode(StatusCodes.Status500InternalServerError, new CommandResponse(false, "An exception occured during IdentitiesDelete.")); 
        //    }
        //}
    }
}
