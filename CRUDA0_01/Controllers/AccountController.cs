using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CRUDA0_01.Data;
using CRUDA0_01.Models.Entities;

namespace CRUDA0_01.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<AccountController> _logger;
    
    public AccountController(ApplicationDbContext context, ILogger<AccountController> logger)
    {
        this._context = context;
        this._logger = logger;
    }
    
    /*
     * task : asynchronous 
     * ActionResult: HTTP response(can return status code) containing a body/list of something 
     * IEnumerable: More flexible than list
     * Account: data Type
     */
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Account>>> GetAccounts()
    {
        _logger.LogInformation("This is all accounts");
        return await _context.Accounts.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Account>> GetAccount(Guid id)
    {
        var account = await _context.Accounts.FindAsync(id);
        if (account == null)
        {
            _logger.LogInformation("Account not found ");
            return NotFound();
        }
        _logger.LogInformation("This is account {@account}", account);
        return account; //returns an account
    }

    [HttpPost]
    public async Task<ActionResult<Account>> CreateAccount(Account account)
    {
        _context.Accounts.Add(account);
        await _context.SaveChangesAsync();
        _logger.LogInformation("Account created with ID: {account}", account.Id);
        return CreatedAtAction(nameof(GetAccount), new { id = account.Id }, account);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAccount(Guid id, Account updated)
    {
        if (id != updated.Id) return BadRequest();

        _context.Entry(updated).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        _logger.LogInformation("Account updated: {updated}", updated.Id);
        return NoContent(); //context returned just updated
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAccount(Guid id)
    {
        var account = await _context.Accounts
            .Include(a => a.Transactions)
            .Include(a => a.Goals)
            .Include(a => a.Pockets)
            .FirstOrDefaultAsync(a => a.Id == id);

        if (account == null) return NotFound();
        _logger.LogInformation("Account not found: {account}", account.Id);
        _context.Transactions.RemoveRange(account.Transactions);
        _context.Goals.RemoveRange(account.Goals);
        _context.Pockets.RemoveRange(account.Pockets);

        _context.Accounts.Remove(account);
        await _context.SaveChangesAsync();
        _logger.LogInformation("Account deleted: {account}", account.Id);
        return NoContent();
    }
}