using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Rockmart_login.Entities
{
    public partial class RockMartContext : DbContext
    {
        public RockMartContext()
        {
        }

        public RockMartContext(DbContextOptions<RockMartContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Business> Businesses { get; set; } = null!;
        public virtual DbSet<Cart> Carts { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Item> Items { get; set; } = null!;
        public virtual DbSet<ItemDetail> ItemDetails { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<Query> Queries { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=HPHLT-1421;Initial Catalog=RockMart;User ID=sa;Password=12345678;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Business>(entity =>
            {
                entity.ToTable("Business");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BusinessAddress)
                    .HasColumnType("text")
                    .HasColumnName("Business_Address");

                entity.Property(e => e.BusinessEmail)
                    .HasMaxLength(140)
                    .IsUnicode(false)
                    .HasColumnName("Business_Email");

                entity.Property(e => e.BusinessName)
                    .HasMaxLength(140)
                    .IsUnicode(false)
                    .HasColumnName("Business_Name");

                entity.Property(e => e.BusinessUsername)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("Business_Username");

                entity.Property(e => e.GstNumber)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("GST_number");

                entity.Property(e => e.Password)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Role)
                    .HasMaxLength(10)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Cart>(entity =>
            {
                entity.ToTable("Cart");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ItemCode).HasColumnName("item_code");

                entity.Property(e => e.ItemQuantity).HasColumnName("item_quantity");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.ItemCodeNavigation)
                    .WithMany(p => p.Carts)
                    .HasForeignKey(d => d.ItemCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Cart__item_code__33D4B598");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Carts)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Cart__user_id__32E0915F");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ItemCategory)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("item_category");
            });

            modelBuilder.Entity<Item>(entity =>
            {
                entity.ToTable("item");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ItemName)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("item_name");

                entity.Property(e => e.ItemPrice).HasColumnName("item_price");

                entity.Property(e => e.ItemQuantity).HasColumnName("item_quantity");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Items)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__item__user_id__2C3393D0");
            });

            modelBuilder.Entity<ItemDetail>(entity =>
            {
                entity.ToTable("itemDetails");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ItemCategory).HasColumnName("item_category");

                entity.Property(e => e.ItemCode).HasColumnName("item_code");

                entity.Property(e => e.ItemDescription)
                    .HasColumnType("text")
                    .HasColumnName("item_description");

                entity.Property(e => e.ItemImage)
                    .HasColumnType("text")
                    .HasColumnName("item_image");

                entity.HasOne(d => d.ItemCategoryNavigation)
                    .WithMany(p => p.ItemDetails)
                    .HasForeignKey(d => d.ItemCategory)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__itemDetai__item___3A81B327");

                entity.HasOne(d => d.ItemCodeNavigation)
                    .WithMany(p => p.ItemDetails)
                    .HasForeignKey(d => d.ItemCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__itemDetai__item___398D8EEE");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Dfo)
                    .HasColumnType("datetime")
                    .HasColumnName("dfo");

                entity.Property(e => e.ItemCode).HasColumnName("item_code");

                entity.Property(e => e.ItemQuantity).HasColumnName("item_quantity");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.ItemCodeNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.ItemCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Orders__item_cod__300424B4");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Orders__user_id__2F10007B");
            });

            modelBuilder.Entity<Query>(entity =>
            {
                entity.ToTable("Query");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Doq)
                    .HasColumnType("datetime")
                    .HasColumnName("doq");

                entity.Property(e => e.QueryDesc)
                    .HasColumnType("text")
                    .HasColumnName("query_desc");

                entity.Property(e => e.RecieverId).HasColumnName("reciever_id");

                entity.Property(e => e.SenderEmail)
                    .HasColumnType("text")
                    .HasColumnName("sender_email");

                entity.Property(e => e.SenderId).HasColumnName("sender_id");

                entity.Property(e => e.SenderPhone)
                    .HasColumnType("text")
                    .HasColumnName("sender_phone");

                entity.HasOne(d => d.Reciever)
                    .WithMany(p => p.QueryRecievers)
                    .HasForeignKey(d => d.RecieverId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Query__reciever___3E52440B");

                entity.HasOne(d => d.Sender)
                    .WithMany(p => p.QuerySenders)
                    .HasForeignKey(d => d.SenderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Query__sender_id__3D5E1FD2");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
