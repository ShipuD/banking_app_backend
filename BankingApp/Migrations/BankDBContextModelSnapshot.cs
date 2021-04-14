﻿// <auto-generated />
using System;
using BankingApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BankingApp.Migrations
{
    [DbContext(typeof(BankDBContext))]
    partial class BankDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.5")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BankingApp.BussinessLogic.Account", b =>
                {
                    b.Property<string>("AccountNo")
                        .HasMaxLength(5)
                        .HasColumnType("nvarchar(5)");

                    b.Property<double>("AccountBalance")
                        .HasColumnType("float");

                    b.Property<string>("CustomerName")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("LocalCurrencyCode")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("nvarchar(3)");

                    b.HasKey("AccountNo");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("BankingApp.BussinessLogic.Transaction", b =>
                {
                    b.Property<int>("TransactionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AccountNo")
                        .HasColumnType("nvarchar(5)");

                    b.Property<double>("Balance")
                        .HasColumnType("float");

                    b.Property<double?>("Credit")
                        .HasColumnType("float");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<double?>("Debit")
                        .HasColumnType("float");

                    b.Property<string>("Reference")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.HasKey("TransactionId");

                    b.HasIndex("AccountNo");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("BankingApp.BussinessLogic.Transaction", b =>
                {
                    b.HasOne("BankingApp.BussinessLogic.Account", "account")
                        .WithMany()
                        .HasForeignKey("AccountNo");

                    b.Navigation("account");
                });
#pragma warning restore 612, 618
        }
    }
}
