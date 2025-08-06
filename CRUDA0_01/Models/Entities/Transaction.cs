using System.Text.Json.Serialization;

namespace CRUDA0_01.Models.Entities;

public class Transaction
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime Date { get; set; }
    public decimal Amount { get; set; }
    public string? Summary { get; set; }
    public Guid AccountId { get; set; }  
    
    [JsonIgnore]
    public Account? Account { get; set; }
}