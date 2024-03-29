﻿// <auto-generated />
using System;
using BidService.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BidService.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240129135647_Bid_DB")]
    partial class Bid_DB
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BidService.Models.Bid", b =>
                {
                    b.Property<Guid>("BidId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ArtId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("BidAmount")
                        .HasColumnType("int");

                    b.Property<Guid>("BidderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("datetime2");

                    b.HasKey("BidId");

                    b.ToTable("Bids");
                });
#pragma warning restore 612, 618
        }
    }
}
