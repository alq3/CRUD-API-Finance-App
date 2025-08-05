using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CRUDA0_01.Data;
using CRUDA0_01.Models.Entities;

namespace CRUDA0_01.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GoalController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<AccountController> _logger;
    //TO DO: Add Logging 
    public GoalController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Goal>>> GetGoals()
    {
        return await _context.Goals.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Goal>> GetGoal(Guid id)
    {
        var goal = await _context.Goals.FindAsync(id);
        if (goal == null) return NotFound();
        return goal;
    }
    
    // GET: api/goal/account/{accountId}
    [HttpGet("account/{accountId}")]
    public async Task<ActionResult<IEnumerable<Goal>>> GetGoalsForAccount(Guid accountId)
    {
        var goals = await _context.Goals
            .Where(g => g.AccountId == accountId)
            .ToListAsync();

        return Ok(goals);
    }


    [HttpPost]
    public async Task<ActionResult<Goal>> CreateGoal(Goal goal)
    {
        _context.Goals.Add(goal);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetGoal), new { id = goal.Id }, goal);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateGoal(Guid id, Goal updated)
    {
        if (id != updated.Id) return BadRequest();

        _context.Entry(updated).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGoal(Guid id)
    {
        var goal = await _context.Goals.FindAsync(id);
        if (goal == null) return NotFound();

        _context.Goals.Remove(goal);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}