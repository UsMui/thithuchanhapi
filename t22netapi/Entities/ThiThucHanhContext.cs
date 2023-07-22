using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace t22netapi.Entities;

public partial class ThiThucHanhContext : DbContext
{
    public ThiThucHanhContext()
    {
    }

    public ThiThucHanhContext(DbContextOptions<ThiThucHanhContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Classroom> Classrooms { get; set; }

    public virtual DbSet<Exam> Exams { get; set; }

    public virtual DbSet<Subject> Subjects { get; set; }

    public virtual DbSet<Teacher> Teachers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=.\\SQLEXPRESS;Initial Catalog=ThiThucHanh;Integrated Security=True;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Classroom>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__classroo__3213E83F642EF681");

            entity.ToTable("classrooms");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Exam>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__exams__3213E83F45C7AC6B");

            entity.ToTable("exams");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Classroomid).HasColumnName("classroomid");
            entity.Property(e => e.Examdate)
                .HasColumnType("datetime")
                .HasColumnName("examdate");
            entity.Property(e => e.Examduration).HasColumnName("examduration");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Starttime).HasColumnName("starttime");
            entity.Property(e => e.Status)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("status");
            entity.Property(e => e.Subjectid).HasColumnName("subjectid");
            entity.Property(e => e.Teacherid).HasColumnName("teacherid");

            entity.HasOne(d => d.Classroom).WithMany(p => p.Exams)
                .HasForeignKey(d => d.Classroomid)
                .HasConstraintName("FK__exams__classroom__164452B1");

            entity.HasOne(d => d.Subject).WithMany(p => p.Exams)
                .HasForeignKey(d => d.Subjectid)
                .HasConstraintName("FK__exams__subjectid__173876EA");

            entity.HasOne(d => d.Teacher).WithMany(p => p.Exams)
                .HasForeignKey(d => d.Teacherid)
                .HasConstraintName("FK__exams__teacherid__182C9B23");
        });

        modelBuilder.Entity<Subject>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__subjects__3213E83FB14687AB");

            entity.ToTable("subjects");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Teacher>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__teachers__3213E83F0DDFB3B1");

            entity.ToTable("teachers");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
