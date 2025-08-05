namespace CRUDA0_01.Models.Entities;

public class Goal
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime? DateGoal { get; set; }
    public decimal CurrentAmount { get; set; } = 0;
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }    
    public Guid AccountId { get; set; }
    public Account Account { get; set; } = null!;

}