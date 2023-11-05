using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TableCRUD.Models;

namespace TableCRUD.Data;

public partial class PubsContext : DbContext
{
    public PubsContext()
    {
    }

    public PubsContext(DbContextOptions<PubsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Discount> Discounts { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Job> Jobs { get; set; }

    public virtual DbSet<PubInfo> PubInfos { get; set; }

    public virtual DbSet<Publisher> Publishers { get; set; }

    public virtual DbSet<Roysched> Royscheds { get; set; }

    public virtual DbSet<Sale> Sales { get; set; }

    public virtual DbSet<Store> Stores { get; set; }

    public virtual DbSet<Title> Titles { get; set; }

    public virtual DbSet<Titleauthor> Titleauthors { get; set; }

    public virtual DbSet<Titleview> Titleviews { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=pubs;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("SQL_Latin1_General_CP1_CI_AS");

        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(e => e.AuId).HasName("UPKCL_auidind");

            entity.Property(e => e.Phone)
                .HasDefaultValueSql("('UNKNOWN')")
                .IsFixedLength();
            entity.Property(e => e.State).IsFixedLength();
            entity.Property(e => e.Zip).IsFixedLength();
        });

        modelBuilder.Entity<Discount>(entity =>
        {
            entity.Property(e => e.StorId).IsFixedLength();

            entity.HasOne(d => d.Stor).WithMany().HasConstraintName("FK__discounts__stor___0F975522");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmpId)
                .HasName("PK_emp_id")
                .IsClustered(false);

            entity.ToTable("employee", tb => tb.HasTrigger("employee_insupd"));

            entity.HasIndex(e => new { e.Lname, e.Fname, e.Minit }, "employee_ind").IsClustered();

            entity.Property(e => e.EmpId).IsFixedLength();
            entity.Property(e => e.HireDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.JobId).HasDefaultValueSql("(1)");
            entity.Property(e => e.JobLvl).HasDefaultValueSql("(10)");
            entity.Property(e => e.Minit).IsFixedLength();
            entity.Property(e => e.PubId)
                .HasDefaultValueSql("('9952')")
                .IsFixedLength();

            entity.HasOne(d => d.Job).WithMany(p => p.Employees)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__employee__job_id__1BFD2C07");

            entity.HasOne(d => d.Pub).WithMany(p => p.Employees)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__employee__pub_id__1ED998B2");
        });

        modelBuilder.Entity<Job>(entity =>
        {
            entity.HasKey(e => e.JobId).HasName("PK__jobs__117F9D94");

            entity.Property(e => e.JobDesc).HasDefaultValueSql("('New Position - title not formalized yet')");
        });

        modelBuilder.Entity<PubInfo>(entity =>
        {
            entity.HasKey(e => e.PubId).HasName("UPKCL_pubinfo");

            entity.Property(e => e.PubId).IsFixedLength();

            entity.HasOne(d => d.Pub).WithOne(p => p.PubInfo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__pub_info__pub_id__173876EA");
        });

        modelBuilder.Entity<Publisher>(entity =>
        {
            entity.HasKey(e => e.PubId).HasName("UPKCL_pubind");

            entity.Property(e => e.PubId).IsFixedLength();
            entity.Property(e => e.Country).HasDefaultValueSql("('USA')");
            entity.Property(e => e.State).IsFixedLength();
        });

        modelBuilder.Entity<Roysched>(entity =>
        {
            entity.HasOne(d => d.Title).WithMany()
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__roysched__title___0DAF0CB0");
        });

        modelBuilder.Entity<Sale>(entity =>
        {
            entity.HasKey(e => new { e.StorId, e.OrdNum, e.TitleId }).HasName("UPKCL_sales");

            entity.Property(e => e.StorId).IsFixedLength();

            entity.HasOne(d => d.Stor).WithMany(p => p.Sales)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__sales__stor_id__0AD2A005");

            entity.HasOne(d => d.Title).WithMany(p => p.Sales)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__sales__title_id__0BC6C43E");
        });

        modelBuilder.Entity<Store>(entity =>
        {
            entity.HasKey(e => e.StorId).HasName("UPK_storeid");

            entity.Property(e => e.StorId).IsFixedLength();
            entity.Property(e => e.State).IsFixedLength();
            entity.Property(e => e.Zip).IsFixedLength();
        });

        modelBuilder.Entity<Title>(entity =>
        {
            entity.HasKey(e => e.TitleId).HasName("UPKCL_titleidind");

            entity.Property(e => e.PubId).IsFixedLength();
            entity.Property(e => e.Pubdate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Type)
                .HasDefaultValueSql("('UNDECIDED')")
                .IsFixedLength();

            entity.HasOne(d => d.Pub).WithMany(p => p.Titles).HasConstraintName("FK__titles__pub_id__014935CB");
        });

        modelBuilder.Entity<Titleauthor>(entity =>
        {
            entity.HasKey(e => new { e.AuId, e.TitleId }).HasName("UPKCL_taind");

            entity.HasOne(d => d.Au).WithMany(p => p.Titleauthors)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__titleauth__au_id__0519C6AF");

            entity.HasOne(d => d.Title).WithMany(p => p.Titleauthors)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__titleauth__title__060DEAE8");
        });

        modelBuilder.Entity<Titleview>(entity =>
        {
            entity.ToView("titleview");

            entity.Property(e => e.PubId).IsFixedLength();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
