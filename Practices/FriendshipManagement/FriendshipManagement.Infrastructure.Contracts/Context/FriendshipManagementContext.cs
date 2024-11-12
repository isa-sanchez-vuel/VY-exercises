﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using FriendshipManagement.Infrastructure.Contracts.Entities;

namespace FriendshipManagement.Infrastructure.Contracts.Context
{
    public partial class FriendshipManagementContext : DbContext
    {
        public FriendshipManagementContext()
        {
        }

        public FriendshipManagementContext(DbContextOptions<FriendshipManagementContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Friendship> Friendships { get; set; }
        public virtual DbSet<Pony> Ponies { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=P-0502\\SQLEXPRESS;Initial Catalog=FriendshipManagement;Persist Security Info=True;User ID=isa;Password=isa");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pony>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Species)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Talent)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}