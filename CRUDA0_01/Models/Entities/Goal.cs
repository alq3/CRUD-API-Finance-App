using System.Text.Json.Serialization;

namespace CRUDA0_01.Models.Entities;

public class Goal
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime? DateGoal { get; set; }
    public decimal CurrentAmount { get; set; } = 0;
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }    
    public Guid AccountId { get; set; } //foreign key in db 
    
    [JsonIgnore] // used for defining relationship in c# code but Json will ignore so requests dont break 
    //navigation property
    public Account? Account { get; set; } 

}