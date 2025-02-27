using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using BusinessObjects;
namespace DataAccessObjects;

public partial class MystoreContext : DbContext
{
    public MystoreContext()
    {
    }

    public MystoreContext(DbContextOptions<MystoreContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AccountMember> AccountMembers { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            //var connectionString = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetConnectionString("ConnectionStrings");
            optionsBuilder.UseSqlServer(GetConnectionString());
        }
    }
    private string GetConnectionString()
    {
        IConfiguration config = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", true, true)
                    .Build();
        var strConn = config["ConnectionStrings:DefaultConnectionStringDB"];

        return strConn;
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AccountMember>(entity =>
        {
            entity.HasKey(e => e.MemberId);

            entity.ToTable("AccountMember");

            entity.Property(e => e.MemberId)
                .HasMaxLength(20)
                .IsFixedLength()
                .HasColumnName("MemberID");
            entity.Property(e => e.EmailAddress)
                .HasMaxLength(100)
                .IsFixedLength();
            entity.Property(e => e.FullName)
                .HasMaxLength(80)
                .IsFixedLength();
            entity.Property(e => e.MemberPassword)
                .HasMaxLength(80)
                .IsFixedLength();
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.CategoryName)
                .HasMaxLength(15)
                .IsFixedLength();
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.ProductName)
                .HasMaxLength(40)
                .IsFixedLength();
            entity.Property(e => e.UnitPrice).HasColumnType("money");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Products_Categories");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
