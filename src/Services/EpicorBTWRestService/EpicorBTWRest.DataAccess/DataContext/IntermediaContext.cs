using EpicorBTWRest.DataAccess.Models.TimeReport;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace EpicorBTWRest.DataAccess.DataContext
{
    public class IntermediaContext : DbContext
    {
        public IntermediaContext()
        {

        }

        public IntermediaContext(DbContextOptions opt) : base(opt)
        {

        }

        public DbSet<Epicor_Labor> Epicor_Labors { get; set; }
        public DbSet<Epicor_UD05> Epicor_UD05s { get; set; }

        public DbSet<ControlPerdidos> ControlPerdidos { get; set; }

        protected override void OnModelCreating(ModelBuilder model)
        {
            model.Entity<Epicor_Labor>().HasKey(x => x.epicorlaborid);

            model.Entity<Epicor_Labor>().Property(e => e.ClockinTime).HasConversion<decimal>();

            model.Entity<Epicor_Labor>().Property(e => e.ClockOutTime).HasConversion<decimal>();

            model.Entity<Epicor_UD05>().HasKey(x=> x.epicorud05id);
            model.Entity<ControlPerdidos>().HasKey(x => x.ContolPerdidosid);
            base.OnModelCreating(model);
        }

    }
}
