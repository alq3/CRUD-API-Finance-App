using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CRUDA0_01.Data;
using CRUDA0_01.Models.Entities;

namespace CRUDA0_01.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PocketController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<AccountController> _logger;
    //TO DO: Add Logging 
    public PocketController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/pocket
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Pocket>>> GetPockets()
    {
        return await _context.Pockets.ToListAsync();
    }

    // GET: api/pocket/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Pocket>> GetPocket(Guid id)
    {
        var pocket = await _context.Pockets.FindAsync(id);
        if (pocket == null) return NotFound();
        return pocket;
    }

    // POST: api/pocket
    [HttpPost]
    public async Task<ActionResult<Pocket>> CreatePocket(Pocket pocket)
    {
        // If no currency was specified, inherit from Account
        if (pocket.Currency == null)
        {
            var account = await _context.Accounts.FindAsync(pocket.AccountId);
            if (account == null) return BadRequest("Invalid AccountId");
            pocket.Currency = account.Currency;
        }

        _context.Pockets.Add(pocket);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetPocket), new { id = pocket.Id }, pocket);
    }

    // PUT: api/pocket/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePocket(Guid id, Pocket updated)
    {
        if (id != updated.Id) return BadRequest();

        _context.Entry(updated).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/pocket/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePocket(Guid id)
    {
        var pocket = await _context.Pockets.FindAsync(id);
        if (pocket == null) return NotFound();

        _context.Pockets.Remove(pocket);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // GET: api/pocket/account/{accountId}
    [HttpGet("account/{accountId}")]
    public async Task<ActionResult<IEnumerable<Pocket>>> GetPocketsByAccount(Guid accountId)
    {
        var pockets = await _context.Pockets
            .Where(p => p.AccountId == accountId)
            .ToListAsync();

        return Ok(pockets);
    }
}
