﻿// <auto-generated />
using System;
using InzynierkaApiReact.Server.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace InzynierkaApiReact.Server.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20241117221146_Creation")]
    partial class Creation
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("InzynierkaApiReact.Server.Models.Planogram", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateOnly>("Date")
                        .HasColumnType("date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Picture")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Planogram");
                });

            modelBuilder.Entity("InzynierkaApiReact.Server.Models.Product", b =>
                {
                    b.Property<string>("EanCode")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("CastoCode")
                        .HasColumnType("int");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PlanogramId")
                        .HasColumnType("int");

                    b.Property<string>("ProductPageLink")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EanCode");

                    b.HasIndex("PlanogramId");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("InzynierkaApiReact.Server.Models.ProductLocalization", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Alley")
                        .HasColumnType("int");

                    b.Property<int>("NumberOnTheShelf")
                        .HasColumnType("int");

                    b.Property<string>("ProductEanCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("ProductEanCode");

                    b.ToTable("ProductLocalization");
                });

            modelBuilder.Entity("InzynierkaApiReact.Server.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("bit");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("InzynierkaApiReact.Server.Models.Product", b =>
                {
                    b.HasOne("InzynierkaApiReact.Server.Models.Planogram", "Planogram")
                        .WithMany()
                        .HasForeignKey("PlanogramId");

                    b.Navigation("Planogram");
                });

            modelBuilder.Entity("InzynierkaApiReact.Server.Models.ProductLocalization", b =>
                {
                    b.HasOne("InzynierkaApiReact.Server.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductEanCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });
#pragma warning restore 612, 618
        }
    }
}
