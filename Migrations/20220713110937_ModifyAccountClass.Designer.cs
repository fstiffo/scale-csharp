﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace scale_csharp.Migrations
{
    [DbContext(typeof(ScaleContext))]
    [Migration("20220713110937_ModifyAccountClass")]
    partial class ModifyAccountClass
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.7");

            modelBuilder.Entity("Account", b =>
                {
                    b.Property<int>("AccountId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Flow")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("AccountId");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("Apartment", b =>
                {
                    b.Property<int>("ApartmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Floor")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Tenant")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("ApartmentId");

                    b.ToTable("Apartments");
                });

            modelBuilder.Entity("Configuration", b =>
                {
                    b.Property<int>("ConfigurationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CleaningsPerMonth")
                        .HasColumnType("INTEGER");

                    b.Property<float>("MonthlyDues")
                        .HasColumnType("REAL");

                    b.Property<float>("StairsCleaningFee")
                        .HasColumnType("REAL");

                    b.Property<DateOnly>("ValidFrom")
                        .HasColumnType("TEXT");

                    b.HasKey("ConfigurationId");

                    b.ToTable("Configurations");
                });

            modelBuilder.Entity("JournalEntry", b =>
                {
                    b.Property<int>("JournalEntryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AccountId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ApartmentId")
                        .HasColumnType("INTEGER");

                    b.Property<float>("Credit")
                        .HasColumnType("REAL");

                    b.Property<DateOnly>("Date")
                        .HasColumnType("TEXT");

                    b.Property<float>("Debit")
                        .HasColumnType("REAL");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("JournalEntryId");

                    b.HasIndex("AccountId");

                    b.HasIndex("ApartmentId");

                    b.ToTable("JournalEntries");
                });

            modelBuilder.Entity("JournalEntry", b =>
                {
                    b.HasOne("Account", "Account")
                        .WithMany("JournalEntries")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Apartment", "Apartment")
                        .WithMany("JournalEntries")
                        .HasForeignKey("ApartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("Apartment");
                });

            modelBuilder.Entity("Account", b =>
                {
                    b.Navigation("JournalEntries");
                });

            modelBuilder.Entity("Apartment", b =>
                {
                    b.Navigation("JournalEntries");
                });
#pragma warning restore 612, 618
        }
    }
}
