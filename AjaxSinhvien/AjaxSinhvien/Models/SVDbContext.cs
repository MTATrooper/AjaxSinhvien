namespace AjaxSinhvien.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class SVDbContext : DbContext
    {
        public SVDbContext()
            : base("name=SVDbContext")
        {
        }

        public virtual DbSet<DANTOC> DANTOCs { get; set; }
        public virtual DbSet<LOP> LOPs { get; set; }
        public virtual DbSet<SINHVIEN> SINHVIENs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DANTOC>()
                .HasMany(e => e.SINHVIENs)
                .WithOptional(e => e.DANTOC)
                .HasForeignKey(e => e.MADANTOC);

            modelBuilder.Entity<LOP>()
                .HasMany(e => e.SINHVIENs)
                .WithOptional(e => e.LOP)
                .HasForeignKey(e => e.MALOP);
        }
    }
}
