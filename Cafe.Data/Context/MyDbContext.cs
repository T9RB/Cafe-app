using Cafe.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace Cafe.Data.Context;

public partial class MyDbContext : DbContext
{
    public MyDbContext()
    {
    }

    public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Dish> Dishes { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDish> OrderDishes { get; set; }

    public virtual DbSet<Post> Posts { get; set; }

    public virtual DbSet<StatusesUser> StatusesUsers { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserShift> UserShifts { get; set; }

    public virtual DbSet<UsersOrder> UsersOrders { get; set; }

    public virtual DbSet<WorkShift> WorkShifts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var currentDirectory = Directory.GetCurrentDirectory();
        var basePath = currentDirectory.Replace(@"\Cafe\bin\Debug\net8.0", "");
        var dbPath = Path.Combine(basePath, "Cafe.Data", "identifier.sqlite");

        optionsBuilder.UseSqlite($"Data Source={dbPath};");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Dish>(entity =>
        {
            entity.HasKey(e => e.IdDish).HasName("Dishes_pk");

            entity.Property(e => e.IdDish);
            entity.Property(e => e.Name).HasMaxLength(30);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.IdOrder).HasName("Orders_pk");

            entity.Property(e => e.IdOrder);
            entity.Property(e => e.DateAndTime).HasColumnType("timestamp without time zone");
            entity.Property(e => e.Place).HasMaxLength(20);
            entity.Property(e => e.Status).HasMaxLength(30);
            entity.Property(e => e.TypePayment).HasMaxLength(30);
        });

        modelBuilder.Entity<OrderDish>(entity =>
        {
            entity.HasKey(e => e.IdList).HasName("OrderDish_pk");

            entity.ToTable("OrderDish");

            entity.Property(e => e.IdList);

            entity.HasOne(d => d.IdDishNavigation).WithMany(p => p.OrderDishes)
                .HasForeignKey(d => d.IdDish)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("OrderDish_Dishes_IdDish_fk");

            entity.HasOne(d => d.IdOrderNavigation).WithMany(p => p.OrderDishes)
                .HasForeignKey(d => d.IdOrder)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("OrderDish_Orders_IdOrder_fk");
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(e => e.IdPost).HasName("Posts_pk");

            entity.Property(e => e.IdPost);
            entity.Property(e => e.Name).HasMaxLength(20);
        });

        modelBuilder.Entity<StatusesUser>(entity =>
        {
            entity.HasKey(e => e.IdStatus).HasName("StatusesUser_pk");

            entity.ToTable("StatusesUser");

            entity.Property(e => e.IdStatus);
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.IdUser).HasName("Users_pk");

            entity.Property(e => e.IdUser);
            entity.Property(e => e.EmplContract).HasMaxLength(500);
            entity.Property(e => e.Fname)
                .HasMaxLength(50)
                .HasColumnName("FName");
            entity.Property(e => e.Lname)
                .HasMaxLength(50)
                .HasColumnName("LName");
            entity.Property(e => e.Login).HasMaxLength(30);
            entity.Property(e => e.Password).HasMaxLength(30);
            entity.Property(e => e.Photo).HasMaxLength(500);
            entity.Property(e => e.Sname)
                .HasMaxLength(50)
                .HasColumnName("SName");

            entity.HasOne(d => d.IdPostNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.IdPost)
                .OnDelete(DeleteBehavior.ClientCascade)
                .HasConstraintName("Users_Posts_IdPost_fk");

            entity.HasOne(d => d.IdStatusNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.IdStatus)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Users_StatusesUser_IdStatus_fk");
        });

        modelBuilder.Entity<UserShift>(entity =>
        {
            entity.HasKey(e => e.IdList).HasName("UserShift_pk");

            entity.ToTable("UserShift");

            entity.Property(e => e.IdList);
            entity.Property(e => e.Place).HasMaxLength(50);

            entity.HasOne(d => d.IdShiftNavigation).WithMany(p => p.UserShifts)
                .HasForeignKey(d => d.IdShift)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("UserShift_WorkShifts_IdShift_fk");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.UserShifts)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("UserShift_Users_IdUser_fk");
        });

        modelBuilder.Entity<UsersOrder>(entity =>
        {
            entity.HasKey(e => e.IdList).HasName("UsersOrders_pk");

            entity.Property(e => e.IdList);

            entity.HasOne(d => d.IdOrderNavigation).WithMany(p => p.UsersOrders)
                .HasForeignKey(d => d.IdOrder)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("UsersOrders_Orders_IdOrder_fk");

            entity.HasOne(d => d.IdUserNavigation)
                .WithMany(p => p.UsersOrders)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientCascade)
                .HasConstraintName("UsersOrders_Users_IdUser_fk");
        });

        modelBuilder.Entity<WorkShift>(entity =>
        {
            entity.HasKey(e => e.IdShift).HasName("WorkShifts_pk");

            entity.Property(e => e.IdShift);
            entity.Property(e => e.End).HasColumnType("timestamp without time zone");
            entity.Property(e => e.Start).HasColumnType("timestamp without time zone");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
