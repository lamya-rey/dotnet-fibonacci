using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Fibonacci
{
    public partial class FibonacciDataContext : DbContext
    {
        public FibonacciDataContext()
        {
        }

        public FibonacciDataContext(DbContextOptions<FibonacciDataContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TFibonacci> TFibonaccis { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<TFibonacci>(entity =>
            {
                entity.HasKey(e => e.FibId)
                    .HasName("PK_Fibonacci");

                entity.ToTable("T_Fibonacci", "sch_fib");

                entity.Property(e => e.FibId)
                    .HasColumnName("FIB_Id")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.FibInput).HasColumnName("FIB_Input");

                entity.Property(e => e.FibOutput).HasColumnName("FIB_Output");
                entity.Property(e => e.FibCreatedTimestamp).HasColumnName("FIB_CreatedTimestamp");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}