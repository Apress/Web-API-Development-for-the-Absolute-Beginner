using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ConferenceApi.Domain;

public partial class ConferenceContext : DbContext
{
    public ConferenceContext()
    {
    }

    public ConferenceContext(DbContextOptions<ConferenceContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Speaker> Speakers { get; set; }

    public virtual DbSet<Talk> Talks { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ConferenceDemo");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Talk>(entity =>
        {
            entity.HasIndex(e => e.SpeakerId, "IX_Talks_SpeakerId");

            entity.HasOne(d => d.Speaker).WithMany(p => p.Talks).HasForeignKey(d => d.SpeakerId);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
