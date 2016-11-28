namespace Assignment2MOT
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class MOTContext : DbContext
    {
        public MOTContext()
            : base("name=MOTContext")
        {
        }

        public virtual DbSet<CentreTime> CentreTimes { get; set; }
        public virtual DbSet<MOTCentre> MOTCentres { get; set; }
        public virtual DbSet<VechAppoint> VechAppoints { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MOTCentre>()
                .HasMany(e => e.CentreTimes)
                .WithRequired(e => e.MOTCentre)
                .HasForeignKey(e => e.MOTCentresCentreId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MOTCentre>()
                .HasMany(e => e.VechAppoints)
                .WithRequired(e => e.MOTCentre)
                .HasForeignKey(e => e.MOTCentresCentreId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<VechAppoint>()
                .Property(e => e.VechRegNo)
                .IsFixedLength();
        }
    }
}
