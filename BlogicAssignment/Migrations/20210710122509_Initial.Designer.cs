﻿// <auto-generated />
using System;
using BlogicAssignment.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BlogicAssignment.Migrations
{
    [DbContext(typeof(BlogicAssignmentDbContext))]
    [Migration("20210710122509_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.7")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BlogicAssignment.Models.Advisor", b =>
                {
                    b.Property<int>("AdvisorID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("BirthNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AdvisorID");

                    b.ToTable("Advisors");
                });

            modelBuilder.Entity("BlogicAssignment.Models.AdvisorContract", b =>
                {
                    b.Property<int>("AdvisorID")
                        .HasColumnType("int");

                    b.Property<int>("ContractID")
                        .HasColumnType("int");

                    b.HasKey("AdvisorID", "ContractID");

                    b.HasIndex("ContractID");

                    b.ToTable("AdvisorContracts");
                });

            modelBuilder.Entity("BlogicAssignment.Models.Client", b =>
                {
                    b.Property<int>("ClientID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("BirthNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ClientID");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("BlogicAssignment.Models.Contract", b =>
                {
                    b.Property<int>("ContractID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ClientID")
                        .HasColumnType("int");

                    b.Property<DateTime>("ContractEndDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ContractEnterDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ContractValidSinceDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("EvidenceNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Institution")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SupervisorID")
                        .HasColumnType("int");

                    b.HasKey("ContractID");

                    b.HasIndex("ClientID");

                    b.HasIndex("SupervisorID");

                    b.ToTable("Contracts");
                });

            modelBuilder.Entity("BlogicAssignment.Models.AdvisorContract", b =>
                {
                    b.HasOne("BlogicAssignment.Models.Advisor", "Advisor")
                        .WithMany("AdvisedContracts")
                        .HasForeignKey("AdvisorID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("BlogicAssignment.Models.Contract", "Contract")
                        .WithMany("Advisors")
                        .HasForeignKey("ContractID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Advisor");

                    b.Navigation("Contract");
                });

            modelBuilder.Entity("BlogicAssignment.Models.Contract", b =>
                {
                    b.HasOne("BlogicAssignment.Models.Client", "Client")
                        .WithMany("Contracts")
                        .HasForeignKey("ClientID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BlogicAssignment.Models.Advisor", "Supervisor")
                        .WithMany("SupervisedContracts")
                        .HasForeignKey("SupervisorID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");

                    b.Navigation("Supervisor");
                });

            modelBuilder.Entity("BlogicAssignment.Models.Advisor", b =>
                {
                    b.Navigation("AdvisedContracts");

                    b.Navigation("SupervisedContracts");
                });

            modelBuilder.Entity("BlogicAssignment.Models.Client", b =>
                {
                    b.Navigation("Contracts");
                });

            modelBuilder.Entity("BlogicAssignment.Models.Contract", b =>
                {
                    b.Navigation("Advisors");
                });
#pragma warning restore 612, 618
        }
    }
}
