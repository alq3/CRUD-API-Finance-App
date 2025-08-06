using System.ComponentModel.DataAnnotations;
using CRUDA0_01.Models.Enums;

namespace CRUDA0_01.Models.Entities;
public class Account
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public decimal Balance { get; set; }
    
    [EnumDataType(typeof(AccountType))] // allow only values from enum
    public AccountType Type { get; set; } = AccountType.Bank;
    
    [EnumDataType(typeof(Currency))]
    public Currency Currency { get; set; } = Currency.EUR;
    
    public List<Transaction>? Transactions { get; set; } = new();
    public List<Goal>? Goals { get; set; } = new();
    public List<Pocket>? Pockets { get; set; } = new();

    
}