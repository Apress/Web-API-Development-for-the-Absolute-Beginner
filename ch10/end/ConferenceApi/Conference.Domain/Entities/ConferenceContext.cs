using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Conference.Domain.Entities;

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
