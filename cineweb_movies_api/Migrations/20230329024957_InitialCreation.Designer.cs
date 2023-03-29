﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using cineweb_movies_api.Context;

#nullable disable

namespace cinewebmoviesapi.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20230329024957_InitialCreation")]
    partial class InitialCreation
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("cineweb_movies_api.Entities.Cliente", b =>
                {
                    b.Property<int>("IdCliente")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("CPF")
                        .HasColumnType("longtext");

                    b.Property<string>("NomeCliente")
                        .HasColumnType("longtext");

                    b.HasKey("IdCliente");

                    b.ToTable("cliente");
                });

            modelBuilder.Entity("cineweb_movies_api.Entities.Filme", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("Active")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("Data")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Genero")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("HomeMovie")
                        .HasColumnType("tinyint(1)");

                    b.Property<byte[]>("Poster")
                        .HasColumnType("longblob");

                    b.Property<string>("Sinopse")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("filme");
                });

            modelBuilder.Entity("cineweb_movies_api.Entities.Ingresso", b =>
                {
                    b.Property<int>("IdIngresso")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("FilmeId")
                        .HasColumnType("int");

                    b.Property<decimal>("Preco")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Quantidade")
                        .HasColumnType("int");

                    b.HasKey("IdIngresso");

                    b.HasIndex("FilmeId")
                        .IsUnique();

                    b.ToTable("ingresso");
                });

            modelBuilder.Entity("cineweb_movies_api.Entities.Pedido", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("CodigoPedido")
                        .HasColumnType("longtext");

                    b.Property<int>("FilmeId")
                        .HasColumnType("int");

                    b.Property<int>("IdCliente")
                        .HasColumnType("int");

                    b.Property<int>("IdIngresso")
                        .HasColumnType("int");

                    b.Property<decimal>("ValorTotal")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("pedido");
                });

            modelBuilder.Entity("cineweb_movies_api.Entities.Ingresso", b =>
                {
                    b.HasOne("cineweb_movies_api.Entities.Filme", "Filme")
                        .WithOne("Ingresso")
                        .HasForeignKey("cineweb_movies_api.Entities.Ingresso", "FilmeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Filme");
                });

            modelBuilder.Entity("cineweb_movies_api.Entities.Filme", b =>
                {
                    b.Navigation("Ingresso");
                });
#pragma warning restore 612, 618
        }
    }
}
