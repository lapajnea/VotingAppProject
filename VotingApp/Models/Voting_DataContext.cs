using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace VotingApp.Models
{
    public partial class Voting_DataContext : DbContext
    {
        public Voting_DataContext()
        {
        }

        public Voting_DataContext(DbContextOptions<Voting_DataContext> options)
            : base(options)
        {
        }

        public virtual DbSet<VotingAnswer> VotingAnswers { get; set; } = null!;
        public virtual DbSet<VotingEvent> VotingEvents { get; set; } = null!;
        public virtual DbSet<VotingQuestion> VotingQuestions { get; set; } = null!;
        public virtual DbSet<VotingResult> VotingResults { get; set; } = null!;
        public virtual DbSet<VotingUser> VotingUsers { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-LEJU2KM\\SQLExpress;Database=Voting_Data;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VotingAnswer>(entity =>
            {
                entity.Property(e => e.VotingAnswerText)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .IsFixedLength();
            });

            modelBuilder.Entity<VotingEvent>(entity =>
            {
                entity.Property(e => e.DateEnd).HasColumnType("date");

                entity.Property(e => e.DateStart).HasColumnType("date");

                entity.Property(e => e.Title)
                    .HasMaxLength(20)
                    .IsFixedLength();
            });

            modelBuilder.Entity<VotingQuestion>(entity =>
            {
                entity.Property(e => e.QuestionText)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .IsFixedLength();
            });

            modelBuilder.Entity<VotingUser>(entity =>
            {
                entity.Property(e => e.Sex)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
