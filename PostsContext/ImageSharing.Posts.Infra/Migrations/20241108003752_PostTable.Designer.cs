﻿// <auto-generated />
using System;
using ImageSharing.Posts.Infra.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ImageSharing.Posts.Infra.Migrations
{
    [DbContext(typeof(PostDbContext))]
    [Migration("20241108003752_PostTable")]
    partial class PostTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ImageSharing.Posts.Domain.Models.Post", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string[]>("ImagePath")
                        .IsRequired()
                        .HasColumnType("text[]")
                        .HasColumnName("image_path");

                    b.Property<string>("Subtitle")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("subtitle");

                    b.Property<string[]>("Tags")
                        .HasMaxLength(255)
                        .HasColumnType("text[]")
                        .HasColumnName("tags");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.ToTable("posts", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
