using CRUDA0_01.Models.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;

namespace CRUDA0_01.Data;

public class ApplicationDbContext : DbContext
{ 
   public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
   
   public DbSet<Account> Accounts { get; set; } = null!;
   public DbSet<Goal> Goals { get; set; } = null!;
   public DbSet<Pocket> Pockets { get; set; } = null!;
   public DbSet<Transaction> Transactions { get; set; } = null!;
   
  

}
 