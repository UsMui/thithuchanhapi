using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace projectnhom.Entities;

public partial class T22netContext : DbContext
{
    public T22netContext()
    {
    }

    public T22netContext(DbContextOptions<T22netContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Admin> Admins { get; set; }

    public virtual DbSet<Brand> Brands { get; set; }

    public virtual DbSet<Car> Cars { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Contract> Contracts { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<TypeCar> TypeCars { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=.\\SQLEXPRESS;Initial Catalog=T22net;Integrated Security=True;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Admin>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__admins__3213E83FE40A589E");

            entity.ToTable("admins");

            entity.HasIndex(e => e.Email, "UQ__admins__AB6E616413DB51B8").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("password");
        });

        modelBuilder.Entity<Brand>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__brands__3213E83F4B6C30D4");

            entity.ToTable("brands");

            entity.HasIndex(e => e.Name, "UQ__brands__72E12F1B6638DA7C").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Thumbnail)
                .HasMaxLength(255)
                .HasColumnName("thumbnail");
        });

        modelBuilder.Entity<Car>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__cars__3213E83F69D55CCB");

            entity.ToTable("cars");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Bienso)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("bienso");
            entity.Property(e => e.BrandId).HasColumnName("brand_id");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.Giathue1ngay)
                .HasColumnType("decimal(12, 4)")
                .HasColumnName("giathue1ngay");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Namsanxuat).HasColumnName("namsanxuat");
            entity.Property(e => e.Sochongoi).HasColumnName("sochongoi");
            entity.Property(e => e.Sokhung)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("sokhung");
            entity.Property(e => e.Somay)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("somay");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.Thumbnail)
                .HasMaxLength(255)
                .HasColumnName("thumbnail");
            entity.Property(e => e.TypeCarId).HasColumnName("typeCar_id");

            entity.HasOne(d => d.Brand).WithMany(p => p.Cars)
                .HasForeignKey(d => d.BrandId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__cars__brand_id__7A672E12");

            entity.HasOne(d => d.TypeCar).WithMany(p => p.Cars)
                .HasForeignKey(d => d.TypeCarId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__cars__typeCar_id__7B5B524B");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__categori__3213E83F24F8F55D");

            entity.ToTable("categories");

            entity.HasIndex(e => e.Name, "UQ__categori__72E12F1BBCAE1597").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Contract>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__contract__3213E83F25680F7B");

            entity.ToTable("contracts");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .HasColumnName("address");
            entity.Property(e => e.CarsId).HasColumnName("cars_id");
            entity.Property(e => e.Cccd)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("CCCD");
            entity.Property(e => e.Contents)
                .HasMaxLength(255)
                .HasColumnName("contents");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.Giatridatcoc)
                .HasColumnType("decimal(12, 4)")
                .HasColumnName("giatridatcoc");
            entity.Property(e => e.Giatrihopdong)
                .HasColumnType("decimal(12, 4)")
                .HasColumnName("giatrihopdong");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Ngaykyhopdong)
                .HasColumnType("date")
                .HasColumnName("ngaykyhopdong");
            entity.Property(e => e.Ngaythue)
                .HasColumnType("date")
                .HasColumnName("ngaythue");
            entity.Property(e => e.Ngaytra)
                .HasColumnType("date")
                .HasColumnName("ngaytra");
            entity.Property(e => e.NumberContract)
                .HasMaxLength(255)
                .HasColumnName("numberContract");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.Tel)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("tel");
            entity.Property(e => e.Thumbnail)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("thumbnail");
            entity.Property(e => e.UsersId).HasColumnName("users_id");

            entity.HasOne(d => d.Cars).WithMany(p => p.Contracts)
                .HasForeignKey(d => d.CarsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__contracts__cars___18EBB532");

            entity.HasOne(d => d.Users).WithMany(p => p.Contracts)
                .HasForeignKey(d => d.UsersId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__contracts__users__19DFD96B");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__products__3213E83F4275CEE4");

            entity.ToTable("products");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(12, 4)")
                .HasColumnName("price");
            entity.Property(e => e.Qty).HasColumnName("qty");
            entity.Property(e => e.Thumbnail)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("thumbnail");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__products__catego__164452B1");
        });

        modelBuilder.Entity<TypeCar>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__typeCars__3213E83FEA40B12C");

            entity.ToTable("typeCars");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__users__3213E83FEF84E410");

            entity.ToTable("users");

            entity.HasIndex(e => e.Email, "UQ__users__AB6E616486B3EE2D").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Birthday)
                .HasColumnType("date")
                .HasColumnName("birthday");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.JobTitle)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("job_title");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.RoleTitle)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("role_title");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
