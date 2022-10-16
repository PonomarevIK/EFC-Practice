using Microsoft.EntityFrameworkCore;

namespace EFC_Practice;

public partial class sample_hr_databaseContext : DbContext
{
    public sample_hr_databaseContext()
    {
    }

    public sample_hr_databaseContext(DbContextOptions<sample_hr_databaseContext> options) : base(options)
    {
    }

    public virtual DbSet<Country>    Countries { get; set; } = null!;
    public virtual DbSet<Department> Departments { get; set; } = null!;
    public virtual DbSet<Dependent>  Dependents { get; set; } = null!;
    public virtual DbSet<Employee>   Employees { get; set; } = null!;
    public virtual DbSet<Job>        Jobs { get; set; } = null!;
    public virtual DbSet<Location>   Locations { get; set; } = null!;
    public virtual DbSet<Region>     Regions { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=sample_hr_database;Trusted_Connection=True;");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Country>(entity =>
        {
            entity.ToTable("countries");

            entity.Property(e => e.CountryId)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("country_id")
                .IsFixedLength();

            entity.Property(e => e.CountryName)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("country_name");

            entity.Property(e => e.RegionId).HasColumnName("region_id");

            entity.HasOne(d => d.Region)
                .WithMany(p => p.Countries)
                .HasForeignKey(d => d.RegionId)
                .HasConstraintName("FK__countries__regio__286302EC");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.ToTable("departments");

            entity.Property(e => e.DepartmentId).HasColumnName("department_id");

            entity.Property(e => e.DepartmentName)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("department_name");

            entity.Property(e => e.LocationId).HasColumnName("location_id");

            entity.HasOne(d => d.Location)
                .WithMany(p => p.Departments)
                .HasForeignKey(d => d.LocationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__departmen__locat__35BCFE0A");
        });

        modelBuilder.Entity<Dependent>(entity =>
        {
            entity.ToTable("dependents");

            entity.Property(e => e.DependentId).HasColumnName("dependent_id");

            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");

            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("first_name");

            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("last_name");

            entity.Property(e => e.Relationship)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("relationship");

            entity.HasOne(d => d.Employee)
                .WithMany(p => p.Dependents)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__dependent__emplo__412EB0B6");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.ToTable("employees");

            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");

            entity.Property(e => e.DepartmentId).HasColumnName("department_id");

            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email");

            entity.Property(e => e.FirstName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("first_name");

            entity.Property(e => e.HireDate)
                .HasColumnType("date")
                .HasColumnName("hire_date");

            entity.Property(e => e.JobId).HasColumnName("job_id");

            entity.Property(e => e.LastName)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("last_name");

            entity.Property(e => e.ManagerId).HasColumnName("manager_id");

            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("phone_number");

            entity.Property(e => e.Salary)
                .HasColumnType("decimal(8, 2)")
                .HasColumnName("salary");

            entity.HasOne(d => d.Department)
                .WithMany(p => p.Employees)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__employees__depar__3D5E1FD2");

            entity.HasOne(d => d.Job)
                .WithMany(p => p.Employees)
                .HasForeignKey(d => d.JobId)
                .HasConstraintName("FK__employees__job_i__3C69FB99");

            entity.HasOne(d => d.Manager)
                .WithMany(p => p.InverseManager)
                .HasForeignKey(d => d.ManagerId)
                .HasConstraintName("FK__employees__manag__3E52440B");
        });

        modelBuilder.Entity<Job>(entity =>
        {
            entity.ToTable("jobs");

            entity.Property(e => e.JobId).HasColumnName("job_id");

            entity.Property(e => e.JobTitle)
                .HasMaxLength(35)
                .IsUnicode(false)
                .HasColumnName("job_title");

            entity.Property(e => e.MaxSalary)
                .HasColumnType("decimal(8, 2)")
                .HasColumnName("max_salary");

            entity.Property(e => e.MinSalary)
                .HasColumnType("decimal(8, 2)")
                .HasColumnName("min_salary");
        });

        modelBuilder.Entity<Location>(entity =>
        {
            entity.ToTable("locations");

            entity.Property(e => e.LocationId).HasColumnName("location_id");

            entity.Property(e => e.City)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("city");

            entity.Property(e => e.CountryId)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("country_id")
                .IsFixedLength();

            entity.Property(e => e.PostalCode)
                .HasMaxLength(12)
                .IsUnicode(false)
                .HasColumnName("postal_code");

            entity.Property(e => e.StateProvince)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("state_province");

            entity.Property(e => e.StreetAddress)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("street_address");

            entity.HasOne(d => d.Country)
                .WithMany(p => p.Locations)
                .HasForeignKey(d => d.CountryId)
                .HasConstraintName("FK__locations__count__2E1BDC42");
        });

        modelBuilder.Entity<Region>(entity =>
        {
            entity.ToTable("regions");

            entity.Property(e => e.RegionId).HasColumnName("region_id");

            entity.Property(e => e.RegionName)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("region_name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
