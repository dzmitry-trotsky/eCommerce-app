using API.Errors;
using Infrastructure.Data.Context;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ErrorsController : BaseController
    {
        private readonly ApplicationContext _context;

        public ErrorsController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpGet("notfound")]
        public IActionResult GetNotFoundRequest()
        {
            var thing = _context.Products.Find(100);

            if(thing == null)
                return NotFound(new ApiResponse(404));

            return Ok();
        }
        [HttpGet("servererror")]
        public IActionResult GetServerError()
        {
            var thing = _context.Products.Find(100);

            var thingToReturn = thing.ToString();

            return Ok();
        }
        [HttpGet("badrequest")]
        public IActionResult GetBadRequest()
        {
            return BadRequest(new ApiResponse(400));
        }
    }
}
