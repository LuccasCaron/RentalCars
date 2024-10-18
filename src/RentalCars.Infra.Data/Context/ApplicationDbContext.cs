using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RentalCars.Domain.Entities;

namespace RentalCars.Infra.Data.Context;

public class ApplicationDbContext : IdentityDbContext
{

    #region Constructor

    public ApplicationDbContext() { }
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    #endregion

    #region Tables

    public DbSet<Car> Cars { get; set; }

    #endregion

    #region Methods

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }

    #endregion

}
