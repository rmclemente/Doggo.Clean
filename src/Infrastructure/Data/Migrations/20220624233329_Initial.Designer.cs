﻿// <auto-generated />
using System;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20220624233329_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("doggo")
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Domain.Entities.Breed", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<int>("Id"));

                    b.Property<int>("BreedType")
                        .HasColumnType("integer")
                        .HasColumnName("breed_type");

                    b.Property<DateTimeOffset?>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(150)
                        .IsUnicode(false)
                        .HasColumnType("character varying(150)")
                        .HasColumnName("created_by");

                    b.Property<Guid>("ExternalId")
                        .HasColumnType("uuid")
                        .HasColumnName("external_id");

                    b.Property<string>("Family")
                        .IsRequired()
                        .HasMaxLength(150)
                        .IsUnicode(false)
                        .HasColumnType("character varying(150)")
                        .HasColumnName("family");

                    b.Property<DateTimeOffset?>("LastUpdateAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("last_update_at");

                    b.Property<string>("LastUpdateBy")
                        .HasMaxLength(150)
                        .IsUnicode(false)
                        .HasColumnType("character varying(150)")
                        .HasColumnName("last_update_by");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .IsUnicode(false)
                        .HasColumnType("character varying(150)")
                        .HasColumnName("name");

                    b.Property<string>("Origin")
                        .IsRequired()
                        .HasMaxLength(150)
                        .IsUnicode(false)
                        .HasColumnType("character varying(150)")
                        .HasColumnName("origin");

                    b.HasKey("Id")
                        .HasName("pk_breeds");

                    b.HasIndex("ExternalId")
                        .HasDatabaseName("ix_breeds_external_id");

                    b.ToTable("breeds", "doggo");
                });
#pragma warning restore 612, 618
        }
    }
}
