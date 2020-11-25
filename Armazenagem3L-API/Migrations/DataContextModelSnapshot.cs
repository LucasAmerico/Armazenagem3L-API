﻿// <auto-generated />
using System;
using Armazenagem3L_API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Armazenagem3L_API.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityByDefaultColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("Armazenagem3L_API.Models.Carga", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("Endereco")
                        .HasColumnType("text");

                    b.Property<decimal>("Frete")
                        .HasColumnType("numeric");

                    b.Property<int>("MotoristaId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("MotoristaId");

                    b.ToTable("Cargas", "Armazenagem3L");
                });

            modelBuilder.Entity("Armazenagem3L_API.Models.Funcionario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("Nome")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Funcionarios", "Armazenagem3L");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Nome = "Lauro"
                        });
                });

            modelBuilder.Entity("Armazenagem3L_API.Models.Motorista", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("Nome")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Motoristas", "Armazenagem3L");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Nome = "Bino"
                        });
                });

            modelBuilder.Entity("Armazenagem3L_API.Models.Produto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<int?>("CargaId")
                        .HasColumnType("integer");

                    b.Property<string>("Nome")
                        .HasColumnType("text");

                    b.Property<decimal>("Peso")
                        .HasColumnType("numeric");

                    b.Property<decimal>("Preco")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.HasIndex("CargaId");

                    b.ToTable("Produtos", "Armazenagem3L");
                });

            modelBuilder.Entity("Armazenagem3L_API.Models.Carga", b =>
                {
                    b.HasOne("Armazenagem3L_API.Models.Motorista", "Motorista")
                        .WithMany()
                        .HasForeignKey("MotoristaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Motorista");
                });

            modelBuilder.Entity("Armazenagem3L_API.Models.Produto", b =>
                {
                    b.HasOne("Armazenagem3L_API.Models.Carga", null)
                        .WithMany("Produtos")
                        .HasForeignKey("CargaId");
                });

            modelBuilder.Entity("Armazenagem3L_API.Models.Carga", b =>
                {
                    b.Navigation("Produtos");
                });
#pragma warning restore 612, 618
        }
    }
}
