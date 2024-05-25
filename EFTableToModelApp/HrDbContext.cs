using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace EFTableToModelApp;

public partial class HrDbContext : DbContext
{
    public HrDbContext()
    {
        Database.EnsureDeleted();
        Database.EnsureCreated();
    }

    public HrDbContext(DbContextOptions<HrDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Company> Companies { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Position> Positions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-ISC66B9\\MSSQLSERVER2022;Initial Catalog=hr_db;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Company>(entity =>
        {
            entity.HasIndex(e => e.CountryId, "IX_Companies_CountryId");

            entity.HasOne(d => d.Country).WithMany(p => p.Companies).HasForeignKey(d => d.CountryId);
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasIndex(e => e.CompanyId, "IX_Employees_CompanyId");

            entity.HasIndex(e => e.PositionId, "IX_Employees_PositionId");

            entity.HasOne(d => d.Company).WithMany(p => p.Employees).HasForeignKey(d => d.CompanyId);

            entity.HasOne(d => d.Position).WithMany(p => p.Employees).HasForeignKey(d => d.PositionId);
        });

        // ONE TO ONE

        //modelBuilder.Entity<Country>()
        //            .HasOne(c => c.Capital)
        //            .WithOne(cp => cp.Country)
        //            .HasForeignKey<Capital>(cp => cp.CountryId);

        modelBuilder.Entity<Capital>()
                    .HasOne(cp => cp.Country)
                    .WithOne(c => c.Capital)
                    .HasForeignKey<Capital>(c => c.CountryId);
        //modelBuilder.Entity<Country>().ToTable("Country");
        //modelBuilder.Entity<Capital>().ToTable("Country");

        modelBuilder.Entity<Employee>()
                    .HasOne(e => e.Company)
                    .WithMany(c => c.Employees)
                    .HasForeignKey(e => e.CompanyId);

        //modelBuilder.Entity<Employee>()
        //            .HasMany(e => e.Projects)
        //            .WithMany(p => p.Employees)
        //            .UsingEntity(ep => ep.ToTable("ProjectsForEmployees"));

        modelBuilder
            .Entity<Project>()
            .HasMany(e => e.Employees)
            .WithMany(p => p.Projects)
            .UsingEntity<EmployeeProject>(
                l => l.HasOne(ep => ep.Employee)
                      .WithMany(e => e.Middle)
                      .HasForeignKey(ep => ep.EmployeeId),
                r => r.HasOne(ep => ep.Project)
                      .WithMany(p => p.Middle)
                      .HasForeignKey(ep => ep.ProjectId),
                m =>
                {
                    m.Property(ep => ep.FinishDate).HasDefaultValueSql("GETDATE()");
                    m.HasKey(i => new { i.EmployeeId, i.ProjectId });
                });


        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
