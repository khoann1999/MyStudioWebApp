using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyStudioWebApi.Models;

namespace MyStudioWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly MyStudioAppContext _context;

        public AccountsController(MyStudioAppContext context)
        {
            _context = context;
        }


        // POST: api/Accounts
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost("Login")]
        public async Task<ActionResult<Account>> LoginAccount(Account user)
        {
            var account = await _context.Account.FirstOrDefaultAsync(result => result.UserName.Equals(user.UserName) && result.Password.Equals(user.Password));

            if (account == null)
            {
                return NotFound();
            }

            return account;
        }

    }
}
