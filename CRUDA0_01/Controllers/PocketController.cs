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
    
    public PocketController(ApplicationDbContext context)
    {
        _context = context;
    }

    //get all tasks 
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Pocket>>> GetPockets()
    {
        _logger.LogInformation("Get all Pockets");
        return await _context.Pockets.ToListAsync();
    }

    //get a specific task
    [HttpGet("{id}")]
    public async Task<ActionResult<Pocket>> GetPocket(Guid id)
    {
        var pocket = await _context.Pockets.FindAsync(id);
        if (pocket == null)
        {
            _logger.LogInformation("Pocket not found: {id}", id);
            return NotFound();
        }
        _logger.LogInformation("Pocket found: {id}", pocket.Id);
        return pocket;
    }

    // create a pocket(POST)
    [HttpPost]
    public async Task<ActionResult<Pocket>> CreatePocket(Pocket pocket)
    {
        // If no currency was specified, inherit from Account
        if (pocket.Currency == null)
        {
            var account = await _context.Accounts.FindAsync(pocket.AccountId);
            if (account == null)
            {
                _logger.LogInformation("Account not found: {account}", pocket.AccountId);
                return BadRequest("Invalid AccountId");
            }
            pocket.Currency = account.Currency;
        }

        _context.Pockets.Add(pocket);
        await _context.SaveChangesAsync();
        _logger.LogInformation("Pocket created: {pocket}", pocket.Id);
        return CreatedAtAction(nameof(GetPocket), new { id = pocket.Id }, pocket);
    }

    // (PUT) update a pocket 
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePocket(Guid id, Pocket updated)
    {
        if (id != updated.Id)
        {
            _logger.LogInformation("Pocket update not found: {id}", updated.Id);
            return BadRequest();
        }

        _context.Entry(updated).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        _logger.LogInformation("Pocket updated: {id}", updated.Id);
        return NoContent();
    }

    // (DELETE)
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePocket(Guid id)
    {
        var pocket = await _context.Pockets.FindAsync(id);
        if (pocket == null)
        {
            _logger.LogInformation("Pocket not found: {id}", id);
            return NotFound();
        }

        _context.Pockets.Remove(pocket);
        await _context.SaveChangesAsync();
        _logger.LogInformation("Pocket deleted: {id}", pocket.Id);
        return NoContent();
    }

    // (GET) get all pockets for a specific account 
    [HttpGet("account/{accountId}")]
    public async Task<ActionResult<IEnumerable<Pocket>>> GetPocketsByAccount(Guid accountId)
    {
        var pockets = await _context.Pockets
            .Where(p => p.AccountId == accountId)
            .ToListAsync();
        _logger.LogInformation("Get all Pockets for {account}", accountId);
        return Ok(pockets);
    }
}
