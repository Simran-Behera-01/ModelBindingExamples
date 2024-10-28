using Microsoft.AspNetCore.Mvc;
using ModelBindingExamples.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ModelBindingExamples.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccntController : ControllerBase
    {
        private static List<Accnt> _accntList = new List<Accnt>
        {
            new Accnt{ Id = 101,Name = "Simran" ,Type = "Savings",Branch="Shapur"},
            new Accnt{ Id = 102,Name = "Sumit" ,Type = "Current",Branch="Lothkunta"},
            new Accnt{ Id = 103,Name = "Shivansh" ,Type = "Savings",Branch="Chintal"},
            new Accnt{ Id = 104,Name = "Syra" ,Type = "Savings",Branch="Chintal"}
        };
        //from query
         [HttpGet]
          public async Task<ActionResult<Accnt>> Get([FromQuery] string type, [FromQuery] string branch)
          {
              var acnts = _accntList.FindAll(acnt => acnt.Type == type && acnt.Branch == branch);

              return Ok(acnts);
          }
        //from route
        [HttpGet("{id}")]
        public async Task<ActionResult<Accnt>> Get([FromRoute] int id)
        {
            var acnt = _accntList.FirstOrDefault(acnt => acnt.Id == id);
            return Ok(acnt);
        }
        //from header
        [HttpGet("Headers")]
        public async Task<ActionResult<Accnt>> GetHeader([FromHeader(Name = "Custom-Header")] string header)
        {
            var value = header;
            return Ok(value);
        }
        //from form
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] Accnt acnt)
        {
            _accntList.Add(acnt);
            return Ok(_accntList);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] Accnt acnt)
        {
            var accnt = _accntList.FirstOrDefault(accont => accont.Id == acnt.Id);
            accnt.Name = acnt.Name;
            accnt.Type = acnt.Type;
            accnt.Branch = acnt.Branch;
            return Ok(_accntList);
        }
        [HttpPost("Upload")]
        public async Task<IActionResult> Post([FromForm] IFormFile file)
        {
            if(file==null)
            {
                return BadRequest();
            }
            return Ok(new
            {
                message = "Image added successfully"
            });
        }

    }
}
