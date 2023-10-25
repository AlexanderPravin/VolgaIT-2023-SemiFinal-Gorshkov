﻿// <auto-generated />
using System;
using Infrastructure.VolgaIT.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.VolgaIT.Migrations
{
    [DbContext(typeof(VolgaContext))]
    partial class VolgaContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Domain.VolgaIT.Entities.RentInfo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<double?>("FinalPrice")
                        .HasColumnType("double precision");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("uuid");

                    b.Property<double>("PriceOfUnit")
                        .HasColumnType("double precision");

                    b.Property<string>("PriceType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("TimeEnd")
                        .HasColumnType("text");

                    b.Property<string>("TimeStart")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("TransportId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.HasIndex("TransportId");

                    b.HasIndex("UserId");

                    b.ToTable("RentInfos");
                });

            modelBuilder.Entity("Domain.VolgaIT.Entities.Transport", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool>("CanBeRented")
                        .HasColumnType("boolean");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double?>("DayPrice")
                        .HasColumnType("double precision");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Identifier")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsRentedNow")
                        .HasColumnType("boolean");

                    b.Property<double>("Latitude")
                        .HasColumnType("double precision");

                    b.Property<double>("Longitude")
                        .HasColumnType("double precision");

                    b.Property<double?>("MinutePrice")
                        .HasColumnType("double precision");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("uuid");

                    b.Property<string>("TransportType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("Transports");
                });

            modelBuilder.Entity("Domain.VolgaIT.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("Balance")
                        .HasColumnType("integer");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Domain.VolgaIT.Entities.RentInfo", b =>
                {
                    b.HasOne("Domain.VolgaIT.Entities.User", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.VolgaIT.Entities.Transport", "Transport")
                        .WithMany("RentHistory")
                        .HasForeignKey("TransportId")
                        .OnDelete(DeleteBehavior.ClientCascade);

                    b.HasOne("Domain.VolgaIT.Entities.User", "CurrentUser")
                        .WithMany("RentHistory")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientCascade);

                    b.Navigation("CurrentUser");

                    b.Navigation("Owner");

                    b.Navigation("Transport");
                });

            modelBuilder.Entity("Domain.VolgaIT.Entities.Transport", b =>
                {
                    b.HasOne("Domain.VolgaIT.Entities.User", "Owner")
                        .WithMany("OwnedTransport")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("Domain.VolgaIT.Entities.Transport", b =>
                {
                    b.Navigation("RentHistory");
                });

            modelBuilder.Entity("Domain.VolgaIT.Entities.User", b =>
                {
                    b.Navigation("OwnedTransport");

                    b.Navigation("RentHistory");
                });
#pragma warning restore 612, 618
        }
    }
}
