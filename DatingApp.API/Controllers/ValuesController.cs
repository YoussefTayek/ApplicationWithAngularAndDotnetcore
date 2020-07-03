using System.Collections.Generic;
using DatingApp.API.Data;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;

namespace DatingApp.API.Controllers
{
   
   
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ValuesController : ControllerBase
    {
         private readonly DataContext dbContext;
        public ValuesController(DataContext _dbContext)
        {
            this.dbContext = _dbContext;
        }

         
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetValues()
        {
            var values = await this.dbContext.Values.ToListAsync();
            return  Ok(values);
        }

        [AllowAnonymous]
        [HttpGet("{id}", Name = "Get")]
        public async Task<IActionResult> GetById(int id)
        {
            var value = await this.dbContext.Values.FirstOrDefaultAsync(x=>x.Id==id);
            return Ok(value);
        }

        // POST: api/Values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
