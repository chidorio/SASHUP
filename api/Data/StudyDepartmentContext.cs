using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using api.Models;

namespace api.Data;

public partial class StudyDepartmentContext : DbContext
{
    public StudyDepartmentContext()
    {
    }

    public StudyDepartmentContext(DbContextOptions<StudyDepartmentContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Autorization> Autorizations { get; set; }

    public virtual DbSet<Cabinet> Cabinets { get; set; }

    public virtual DbSet<Change> Changes { get; set; }

    public virtual DbSet<DayWeek> DayWeeks { get; set; }

    public virtual DbSet<Group> Groups { get; set; }

    public virtual DbSet<NumberPair> NumberPairs { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Schedule> Schedules { get; set; }

    public virtual DbSet<StatusGroup> StatusGroups { get; set; }

    public virtual DbSet<StatusTeacher> StatusTeachers { get; set; }

    public virtual DbSet<Subject> Subjects { get; set; }

    public virtual DbSet<Teacher> Teachers { get; set; }

    public virtual DbSet<TypeEmployment> TypeEmployments { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionString:DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Autorization>(entity =>
        {
            entity.ToTable("Autorization");

            entity.Property(e => e.IdRole).HasColumnName("Id_Role");
            entity.Property(e => e.Image).HasMaxLength(50);
            entity.Property(e => e.Login).HasMaxLength(250);
            entity.Property(e => e.Password).HasMaxLength(250);
            entity.Property(e => e.RefreshToken).HasMaxLength(250);
            entity.Property(e => e.RefreshTokenExp).HasColumnType("datetime");

            entity.HasOne(d => d.IdRoleNavigation).WithMany(p => p.Autorizations)
                .HasForeignKey(d => d.IdRole)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Autorization_Role");
        });

        modelBuilder.Entity<Cabinet>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Cabinet");

            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Type).HasMaxLength(50);
        });

        modelBuilder.Entity<Change>(entity =>
        {
            entity.Property(e => e.Date).HasColumnType("date");
            entity.Property(e => e.IdCabinets).HasColumnName("Id_Cabinets");
            entity.Property(e => e.IdGroup).HasColumnName("Id_Group");
            entity.Property(e => e.IdNumberPair).HasColumnName("Id_Number_pair");
            entity.Property(e => e.IdSubject).HasColumnName("Id_Subject");
            entity.Property(e => e.IdTeacher).HasColumnName("Id_Teacher");

            entity.HasOne(d => d.IdCabinetsNavigation).WithMany(p => p.Changes)
                .HasForeignKey(d => d.IdCabinets)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Changes_Cabinets");

            entity.HasOne(d => d.IdGroupNavigation).WithMany(p => p.Changes)
                .HasForeignKey(d => d.IdGroup)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Changes_Groups");

            entity.HasOne(d => d.IdNumberPairNavigation).WithMany(p => p.Changes)
                .HasForeignKey(d => d.IdNumberPair)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Changes_Number_pair");

            entity.HasOne(d => d.IdSubjectNavigation).WithMany(p => p.Changes)
                .HasForeignKey(d => d.IdSubject)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Changes_Subjects");

            entity.HasOne(d => d.IdTeacherNavigation).WithMany(p => p.Changes)
                .HasForeignKey(d => d.IdTeacher)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Changes_Teachers");
        });

        modelBuilder.Entity<DayWeek>(entity =>
        {
            entity.ToTable("Day_week");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Group>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Group");

            entity.Property(e => e.IdStatusGroup).HasColumnName("Id_Status_group");
            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasOne(d => d.IdStatusGroupNavigation).WithMany(p => p.Groups)
                .HasForeignKey(d => d.IdStatusGroup)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Groups_Status_group");
        });

        modelBuilder.Entity<NumberPair>(entity =>
        {
            entity.ToTable("Number_pair");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("Role");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Schedule>(entity =>
        {
            entity.ToTable("Schedule");

            entity.Property(e => e.IdCabinets).HasColumnName("Id_Cabinets");
            entity.Property(e => e.IdDayWeek).HasColumnName("Id_Day_week");
            entity.Property(e => e.IdGroup).HasColumnName("Id_Group");
            entity.Property(e => e.IdNumberPair).HasColumnName("Id_Number_pair");
            entity.Property(e => e.IdSubject).HasColumnName("Id_Subject");
            entity.Property(e => e.IdTeacher).HasColumnName("Id_Teacher");

            entity.HasOne(d => d.IdCabinetsNavigation).WithMany(p => p.Schedules)
                .HasForeignKey(d => d.IdCabinets)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Schedule_Cabinets");

            entity.HasOne(d => d.IdDayWeekNavigation).WithMany(p => p.Schedules)
                .HasForeignKey(d => d.IdDayWeek)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Schedule_Day_week");

            entity.HasOne(d => d.IdGroupNavigation).WithMany(p => p.Schedules)
                .HasForeignKey(d => d.IdGroup)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Schedule_Groups");

            entity.HasOne(d => d.IdNumberPairNavigation).WithMany(p => p.Schedules)
                .HasForeignKey(d => d.IdNumberPair)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Schedule_Number_pair");

            entity.HasOne(d => d.IdSubjectNavigation).WithMany(p => p.Schedules)
                .HasForeignKey(d => d.IdSubject)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Schedule_Subjects");

            entity.HasOne(d => d.IdTeacherNavigation).WithMany(p => p.Schedules)
                .HasForeignKey(d => d.IdTeacher)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Schedule_Teachers");
        });

        modelBuilder.Entity<StatusGroup>(entity =>
        {
            entity.ToTable("Status_group");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<StatusTeacher>(entity =>
        {
            entity.ToTable("Status_teacher");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Subject>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Subject");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Teacher>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Teacher");

            entity.Property(e => e.Family).HasMaxLength(50);
            entity.Property(e => e.IdStatusTeacher).HasColumnName("Id_Status_teacher");
            entity.Property(e => e.IdTypeEmployment).HasColumnName("Id_Type_employment");
            entity.Property(e => e.Image).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Patronymic).HasMaxLength(50);

            entity.HasOne(d => d.IdStatusTeacherNavigation).WithMany(p => p.Teachers)
                .HasForeignKey(d => d.IdStatusTeacher)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Teachers_Status_teacher");

            entity.HasOne(d => d.IdTypeEmploymentNavigation).WithMany(p => p.Teachers)
                .HasForeignKey(d => d.IdTypeEmployment)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Teachers_Type_employment");
        });

        modelBuilder.Entity<TypeEmployment>(entity =>
        {
            entity.ToTable("Type_employment");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
