using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using TennisCourtBookingApp.Common.Utility;

namespace TennisCourtBookingApp.Repository.Models
{
    public partial class TestSPC5Context : DbContext
    {
        public TestSPC5Context()
        {
        }

        public TestSPC5Context(DbContextOptions<TestSPC5Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Candidate> Candidates { get; set; } = null!;
        public virtual DbSet<City> Cities { get; set; } = null!;
        public virtual DbSet<Course> Courses { get; set; } = null!;
        public virtual DbSet<CourseStudent> CourseStudents { get; set; } = null!;
        public virtual DbSet<Emp> Emps { get; set; } = null!;
        public virtual DbSet<Employe> Employes { get; set; } = null!;
        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<Grade> Grades { get; set; } = null!;
        public virtual DbSet<Member> Members { get; set; } = null!;
        public virtual DbSet<MigrationHistory> MigrationHistories { get; set; } = null!;
        public virtual DbSet<State> States { get; set; } = null!;
        public virtual DbSet<Student> Students { get; set; } = null!;
        public virtual DbSet<Studenttt> Studenttts { get; set; } = null!;
        public virtual DbSet<Teacher> Teachers { get; set; } = null!;
        public virtual DbSet<TennisCourt> TennisCourts { get; set; } = null!;
        public virtual DbSet<TennisCourtBooking> TennisCourtBookings { get; set; } = null!;
        public virtual DbSet<TennisCourtBookingRole> TennisCourtBookingRoles { get; set; } = null!;
        public virtual DbSet<TennisCourtBookingUser> TennisCourtBookingUsers { get; set; } = null!;
        public virtual DbSet<UserMaster> UserMasters { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(AppCommon.ConnectionString).UseLazyLoadingProxies();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Candidate>(entity =>
            {
                entity.ToTable("candidate");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.FatherName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StudentGender)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StudentName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.HasKey(e => e.Cid);

                entity.ToTable("City");

                entity.Property(e => e.Cid).HasColumnName("CId");

                entity.Property(e => e.CityName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Sid).HasColumnName("SId");

                entity.HasOne(d => d.SidNavigation)
                    .WithMany(p => p.Cities)
                    .HasForeignKey(d => d.Sid)
                    .HasConstraintName("FK_City_State");
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.ToTable("Course");

                entity.Property(e => e.CourseName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CourseStudent>(entity =>
            {
                entity.HasKey(e => e.StuCourseId);

                entity.ToTable("CourseStudent");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.CourseStudents)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK_CourseStudent_Course");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.CourseStudents)
                    .HasForeignKey(d => d.StudentId)
                    .HasConstraintName("FK_CourseStudent_Student");
            });

            modelBuilder.Entity<Emp>(entity =>
            {
                entity.HasKey(e => e.Empno);

                entity.ToTable("EMP");

                entity.Property(e => e.Empno).HasColumnName("EMPNO");

                entity.Property(e => e.Comm).HasColumnName("COMM");

                entity.Property(e => e.Deptno).HasColumnName("DEPTNO");

                entity.Property(e => e.Ename)
                    .HasMaxLength(50)
                    .HasColumnName("ENAME");

                entity.Property(e => e.Job)
                    .HasMaxLength(50)
                    .HasColumnName("JOB");

                entity.Property(e => e.Sal)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("SAL");
            });

            modelBuilder.Entity<Employe>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Employe");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EmpCity)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EmpDob).HasColumnType("date");

                entity.Property(e => e.EmpName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EmpState)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.EmpId);

                entity.ToTable("Employee");

                entity.Property(e => e.EmpId).ValueGeneratedNever();

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EmpCity)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EmpDob)
                    .HasColumnType("date")
                    .HasColumnName("EmpDOB");

                entity.Property(e => e.EmpName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EmpState)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Member>(entity =>
            {
                entity.HasKey(e => e.Empno)
                    .HasName("PK_MEMBERS");

                entity.ToTable("MEMBER");

                entity.Property(e => e.Empno)
                    .ValueGeneratedNever()
                    .HasColumnName("EMPNO");

                entity.Property(e => e.Comm).HasColumnName("COMM");

                entity.Property(e => e.Ename)
                    .HasMaxLength(50)
                    .HasColumnName("ENAME");

                entity.Property(e => e.Mgr).HasColumnName("MGR");

                entity.Property(e => e.Sal).HasColumnName("SAL");
            });

            modelBuilder.Entity<MigrationHistory>(entity =>
            {
                entity.HasKey(e => new { e.MigrationId, e.ContextKey })
                    .HasName("PK_dbo.__MigrationHistory");

                entity.ToTable("__MigrationHistory");

                entity.Property(e => e.MigrationId).HasMaxLength(150);

                entity.Property(e => e.ContextKey).HasMaxLength(300);

                entity.Property(e => e.ProductVersion).HasMaxLength(32);
            });

            modelBuilder.Entity<State>(entity =>
            {
                entity.HasKey(e => e.Sid);

                entity.ToTable("State");

                entity.Property(e => e.Sid).HasColumnName("SId");

                entity.Property(e => e.StateName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.ToTable("Student");

                entity.Property(e => e.Role)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StuCity)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StuEmail)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StuFname)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("StuFName");

                entity.Property(e => e.StuLname)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("StuLName");

                entity.Property(e => e.StuPassword)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StuState)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Studenttt>(entity =>
            {
                entity.HasKey(e => e.StudentId)
                    .HasName("PK_dbo.Students");

                entity.HasIndex(e => e.GradeGradeId, "IX_Grade_GradeId");

                entity.Property(e => e.StudentId).HasColumnName("StudentID");

                entity.Property(e => e.DateOfBirth).HasColumnType("datetime");

                entity.Property(e => e.GradeGradeId).HasColumnName("Grade_GradeId");

                entity.Property(e => e.Height).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<Teacher>(entity =>
            {
                entity.HasKey(e => e.Tid);

                entity.ToTable("TEACHER");

                entity.Property(e => e.Tid)
                    .ValueGeneratedNever()
                    .HasColumnName("TID");

                entity.Property(e => e.Doj).HasColumnName("DOJ");

                entity.Property(e => e.Subject).HasColumnName("SUBJECT");

                entity.Property(e => e.Tname)
                    .HasMaxLength(50)
                    .HasColumnName("TNAME");
            });

            modelBuilder.Entity<TennisCourt>(entity =>
            {
                entity.ToTable("TennisCourt");

                entity.Property(e => e.CreatedOn).HasColumnType("date");

                entity.Property(e => e.TennisCourtAddress)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TennisCourtName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedOn).HasColumnType("date");
            });

            modelBuilder.Entity<TennisCourtBooking>(entity =>
            {
                entity.HasKey(e => e.BookingId)
                    .HasName("PK_TennisCourtBookings");

                entity.ToTable("TennisCourtBooking");

                entity.Property(e => e.BookingDate).HasColumnType("date");
            });

            modelBuilder.Entity<TennisCourtBookingRole>(entity =>
            {
                entity.HasKey(e => e.RoleId);

                entity.ToTable("TennisCourtBookingRole");

                entity.Property(e => e.RoleName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TennisCourtBookingUser>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK_TennisCourtBookingUsers");

                entity.ToTable("TennisCourtBookingUser");

                entity.Property(e => e.Address).IsUnicode(false);

                entity.Property(e => e.CreatedOn).HasColumnType("date");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Image).IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedOn).HasColumnType("date");

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.TennisCourtBookingUsers)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_TennisCourtBookingUsers_TennisCourtBookingRole");
            });

            modelBuilder.Entity<UserMaster>(entity =>
            {
                entity.ToTable("UserMaster");

                entity.Property(e => e.City)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Fname)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("FName");

                entity.Property(e => e.Lname)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("LName");

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Role)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.State)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StuImage).HasMaxLength(50);

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.UserMasters)
                    .HasForeignKey(d => d.StudentId)
                    .HasConstraintName("FK_UserMaster_Student");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
