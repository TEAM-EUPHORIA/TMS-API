using Microsoft.AspNetCore.Mvc;

namespace TMS.API.Controllers
{
    [ApiController, Route("[controller]")]
    public class MyBaseController : ControllerBase
    {
        protected readonly object errorIdResponse = new
        {
            message = "Provide a valid id"
        };
        protected readonly object errorObjectRespones = new
        {
            message = "Provide a valid Object"
        };
        protected readonly string ProblemResponse = "we are sorry,some thing went wrong";
        protected readonly AppDbContext _context;
        public MyBaseController(AppDbContext context)
        {
            _context = context;
        }

        protected IActionResult BadId()
        {
            return BadRequest(errorIdResponse);
        }
        protected IActionResult BadObject()
        {
            return BadRequest(errorObjectRespones);
        }
    }
}
