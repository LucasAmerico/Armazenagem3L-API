﻿// <auto-generated />
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
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("Frete")
                        .HasColumnType("numeric");

                    b.Property<int>("MotoristaId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Cargas", "Armazenagem3L");
                });

            modelBuilder.Entity("Armazenagem3L_API.Models.CargaProduto", b =>
                {
                    b.Property<int>("CargaId")
                        .HasColumnType("integer");

                    b.Property<int>("ProdutoId")
                        .HasColumnType("integer");

                    b.Property<int>("Qtd")
                        .HasColumnType("integer");

                    b.HasKey("CargaId", "ProdutoId");

                    b.HasIndex("ProdutoId");

                    b.ToTable("CargaProdutos", "Armazenagem3L");
                });

            modelBuilder.Entity("Armazenagem3L_API.Models.CargasRecusada", b =>
                {
                    b.Property<int>("CargaId")
                        .HasColumnType("integer");

                    b.Property<int>("MotoristaId")
                        .HasColumnType("integer");

                    b.HasKey("CargaId", "MotoristaId");

                    b.HasIndex("MotoristaId");

                    b.ToTable("CargasRecusadas", "Armazenagem3L");
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

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("Peso")
                        .HasColumnType("decimal(8, 3)");

                    b.Property<decimal>("Preco")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<int>("Qtd")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Produtos", "Armazenagem3L");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Nome = "Playstation 5",
                            Peso = 1m,
                            Preco = 1m,
                            Qtd = 300
                        },
                        new
                        {
                            Id = 2,
                            Nome = "Mouse",
                            Peso = 1m,
                            Preco = 1m,
                            Qtd = 300
                        },
                        new
                        {
                            Id = 3,
                            Nome = "Teclado",
                            Peso = 1m,
                            Preco = 1m,
                            Qtd = 300
                        },
                        new
                        {
                            Id = 4,
                            Nome = "Monitor",
                            Peso = 1m,
                            Preco = 1m,
                            Qtd = 300
                        },
                        new
                        {
                            Id = 5,
                            Nome = "Dualshock 4",
                            Peso = 1m,
                            Preco = 1m,
                            Qtd = 300
                        },
                        new
                        {
                            Id = 6,
                            Nome = "Dualsense",
                            Peso = 1m,
                            Preco = 1m,
                            Qtd = 300
                        });
                });

            modelBuilder.Entity("Armazenagem3L_API.Models.CargaProduto", b =>
                {
                    b.HasOne("Armazenagem3L_API.Models.Carga", "Carga")
                        .WithMany()
                        .HasForeignKey("CargaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Armazenagem3L_API.Models.Produto", "Produto")
                        .WithMany()
                        .HasForeignKey("ProdutoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Carga");

                    b.Navigation("Produto");
                });

            modelBuilder.Entity("Armazenagem3L_API.Models.CargasRecusada", b =>
                {
                    b.HasOne("Armazenagem3L_API.Models.Carga", "Carga")
                        .WithMany()
                        .HasForeignKey("CargaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Armazenagem3L_API.Models.Motorista", "Motorista")
                        .WithMany()
                        .HasForeignKey("MotoristaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Carga");

                    b.Navigation("Motorista");
                });
#pragma warning restore 612, 618
        }
    }
}
