﻿// <auto-generated />
using System;
using LifeMastery.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LifeMastery.Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240115155250_RegularPaymentAmountIsNullable")]
    partial class RegularPaymentAmountIsNullable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("LifeMastery.Core.Modules.Finance.Models.Expense", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric");

                    b.Property<int?>("CategoryId")
                        .HasColumnType("integer");

                    b.Property<DateOnly>("Date")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("date")
                        .HasDefaultValueSql("CURRENT_DATE");

                    b.Property<string>("Note")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Expenses");
                });

            modelBuilder.Entity("LifeMastery.Core.Modules.Finance.Models.ExpenseCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("ExpenseCategories");
                });

            modelBuilder.Entity("LifeMastery.Core.Modules.Finance.Models.Payment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateOnly>("Date")
                        .HasColumnType("date");

                    b.Property<int?>("RegularPaymentId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("RegularPaymentId");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("LifeMastery.Core.Modules.Finance.Models.RegularPayment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<decimal?>("Amount")
                        .HasColumnType("numeric");

                    b.Property<int?>("DeadlineDay")
                        .HasColumnType("integer");

                    b.Property<int?>("DeadlineMonth")
                        .HasColumnType("integer");

                    b.Property<bool>("IsAdvanced")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<byte>("Period")
                        .HasColumnType("smallint");

                    b.HasKey("Id");

                    b.ToTable("RegularPayments");
                });

            modelBuilder.Entity("LifeMastery.Core.Modules.Jobs.Models.Job", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("Deadline")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("EstimationMinutes")
                        .HasColumnType("integer");

                    b.Property<byte>("Group")
                        .HasColumnType("smallint");

                    b.Property<bool>("IsCompleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<byte?>("Priority")
                        .HasColumnType("smallint");

                    b.HasKey("Id");

                    b.ToTable("Jobs");
                });

            modelBuilder.Entity("LifeMastery.Core.Modules.WeightControl.Models.HealthInfo", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("UserId"));

                    b.Property<DateOnly>("BirthDate")
                        .HasColumnType("date");

                    b.Property<byte>("Gender")
                        .HasColumnType("smallint");

                    b.Property<int>("Height")
                        .HasColumnType("integer");

                    b.HasKey("UserId");

                    b.ToTable("HealthInfos");
                });

            modelBuilder.Entity("LifeMastery.Core.Modules.WeightControl.Models.WeightRecord", b =>
                {
                    b.Property<DateOnly>("Date")
                        .HasColumnType("date");

                    b.Property<double>("Weight")
                        .HasColumnType("double precision");

                    b.HasKey("Date");

                    b.ToTable("WeightRecords");
                });

            modelBuilder.Entity("LifeMastery.Core.Modules.Finance.Models.Expense", b =>
                {
                    b.HasOne("LifeMastery.Core.Modules.Finance.Models.ExpenseCategory", "Category")
                        .WithMany("Expenses")
                        .HasForeignKey("CategoryId");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("LifeMastery.Core.Modules.Finance.Models.Payment", b =>
                {
                    b.HasOne("LifeMastery.Core.Modules.Finance.Models.RegularPayment", null)
                        .WithMany("Payments")
                        .HasForeignKey("RegularPaymentId");
                });

            modelBuilder.Entity("LifeMastery.Core.Modules.Finance.Models.ExpenseCategory", b =>
                {
                    b.Navigation("Expenses");
                });

            modelBuilder.Entity("LifeMastery.Core.Modules.Finance.Models.RegularPayment", b =>
                {
                    b.Navigation("Payments");
                });
#pragma warning restore 612, 618
        }
    }
}
