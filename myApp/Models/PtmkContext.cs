using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace myApp.Models;

public partial class PtmkContext : DbContext
{
    public PtmkContext()
    {
    }

    public PtmkContext(DbContextOptions<PtmkContext> options)
        : base(options)
    {
    }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Host=localhost;Database=ptmk;Username=postgres;Password=123");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("user_pk");

            entity.ToTable("User");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("nextval('user_seq'::regclass)")
                .HasColumnName("id");
            entity.Property(e => e.BirthDay)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("birth_day");
            entity.Property(e => e.Fio).HasColumnName("fio");
            entity.Property(e => e.Gender).HasColumnName("gender");
        });
        modelBuilder.HasSequence("user_seq");

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
