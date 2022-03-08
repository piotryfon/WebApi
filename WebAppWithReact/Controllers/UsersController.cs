using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebAppWithReact.Models;


namespace WebAppWithReact.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IConfiguration _configuration;

        public UsersController(ApplicationDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }
        [HttpGet]
        public JsonResult Get()
        {

            List<Users> table = _dbContext.Users.ToList();

            return new JsonResult(table);
        }
        [HttpPost]
        public async Task<IActionResult> Post(Users user, CancellationToken cancellationToken = default)
        {
            _dbContext.Users.Add(user);
            int insertedUser = await _dbContext.SaveChangesAsync(cancellationToken);

            if (insertedUser > 0)
            {
                return new JsonResult("Added Successfully");
            }

            return BadRequest("Error");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Users modifiedUser, CancellationToken cancellationToken)
        {
            Users user = await _dbContext.Users.SingleOrDefaultAsync(d => d.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            user.Name = modifiedUser.Name;
            user.EmailAddress = modifiedUser.EmailAddress;

            _dbContext.Update(user);

            int modUser = await _dbContext.SaveChangesAsync(cancellationToken);
            if (modUser > 0)
            {
                return new JsonResult("Modified Successfully");
            }

            return BadRequest("Error");
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            Users user = await _dbContext.Users.SingleOrDefaultAsync(d => d.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            _dbContext.Remove(user);

            int removeUser = await _dbContext.SaveChangesAsync(cancellationToken);
            if (removeUser > 0)
            {
                return new JsonResult("Remove Successfully");
            }

            return BadRequest("Error");
        }
    }
}
