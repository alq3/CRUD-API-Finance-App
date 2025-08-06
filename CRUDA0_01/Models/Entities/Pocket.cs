using System.Text.Json.Serialization;
using CRUDA0_01.Models.Enums;

namespace CRUDA0_01.Models.Entities;

public class Pocket
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Currency? Currency { get; set; } 
    public string? Title { get; set; }
    public string? Summary { get; set; }
    public Guid AccountId { get; set; }  
  
    [JsonIgnore]
    public Account? Account { get; set; }
}