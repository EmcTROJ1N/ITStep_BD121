using System;
using System.Collections.Generic;
using Exam.Models;
using Microsoft.EntityFrameworkCore;

namespace Exam.Data;

public partial class PhoneBookDbContext : DbContext
{
    public PhoneBookDbContext()
    {
    }

    public PhoneBookDbContext(DbContextOptions<PhoneBookDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Contact> Contacts { get; set; }

    public virtual DbSet<Phone> Phones { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=PhoneBookDB;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Categori__3213E83F45C9011C");
        });

        modelBuilder.Entity<Contact>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Contacts__3213E83FDC005A4C");

            entity.HasOne(d => d.Category).WithMany(p => p.Contacts).HasConstraintName("FK__Contacts__Catego__2F10007B");

            entity.HasOne(d => d.Phone).WithMany(p => p.Contacts)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Contacts__PhoneI__2E1BDC42");
        });

        modelBuilder.Entity<Phone>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Phones__3213E83F497B01CD");
        });
        
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
