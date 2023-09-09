﻿// <auto-generated />

using System.Diagnostics.CodeAnalysis;
using Hw10.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Homework10.Migrations
{
    [ExcludeFromCodeCoverage]
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Homework10.DbModels.SolvingExpression", b =>
                {
                    b.Property<int>("SolvingExpressionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("SolvingExpressionId"));

                    b.Property<string>("Expression")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("Result")
                        .HasColumnType("double precision");

                    b.HasKey("SolvingExpressionId");

                    b.ToTable("SolvingExpressions");
                });
#pragma warning restore 612, 618
        }
    }
}