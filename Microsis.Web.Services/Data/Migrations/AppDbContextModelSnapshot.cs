using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsis.Web.Services.Data;

#nullable disable

namespace Microsis.Web.Services.Data.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Microsis.Names.Models.Foto", b =>
            {
                b.Property<Guid>("ID")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("uniqueidentifier");

                b.Property<string>("Author")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("Description")
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("Filename")
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnType("nvarchar(255)");

                b.Property<long>("FileSize")
                    .HasColumnType("bigint");

                b.Property<DateTime>("LastUpdate")
                    .HasColumnType("datetime2");

                b.Property<string>("LocalPath")
                    .IsRequired()
                    .HasMaxLength(500)
                    .HasColumnType("nvarchar(500)");

                b.Property<string>("Title")
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnType("nvarchar(200)");

                b.Property<bool>("Visible")
                    .HasColumnType("bit");

                b.HasKey("ID");

                b.ToTable("Foto");
            });

            modelBuilder.Entity("Microsis.Names.Models.News", b =>
            {
                b.Property<Guid>("ID")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("uniqueidentifier");

                b.Property<string>("Author")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("Contenuto")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<DateTime>("DataPubblicazione")
                    .HasColumnType("datetime2");

                b.Property<string>("Descrizione")
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("GalleriaFoto")
                    .HasColumnType("nvarchar(max)");

                b.Property<DateTime>("LastUpdate")
                    .HasColumnType("datetime2");

                b.Property<string>("Titolo")
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnType("nvarchar(200)");

                b.Property<bool>("Visible")
                    .HasColumnType("bit");

                b.HasKey("ID");

                b.ToTable("News");
            });

            modelBuilder.Entity("Microsis.Names.Models.ProgettoUE", b =>
            {
                b.Property<Guid>("ID")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("uniqueidentifier");

                b.Property<string>("Abstract")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("Author")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("EntiCoinvolti")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("ImagePath")
                    .HasColumnType("nvarchar(max)");

                b.Property<DateTime>("LastUpdate")
                    .HasColumnType("datetime2");

                b.Property<string>("Tab_Name")
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnType("nvarchar(50)");

                b.Property<string>("Titolo")
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnType("nvarchar(200)");

                b.Property<bool>("Visible")
                    .HasColumnType("bit");

                b.HasKey("ID");

                b.ToTable("ProgettiUE");
            });

            modelBuilder.Entity("Microsis.Names.Models.Servizio", b =>
            {
                b.Property<Guid>("ID")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("uniqueidentifier");

                b.Property<string>("Author")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("Descrizione")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("GalleriaFoto")
                    .HasColumnType("nvarchar(max)");

                b.Property<DateTime>("LastUpdate")
                    .HasColumnType("datetime2");

                b.Property<string>("Titolo")
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnType("nvarchar(200)");

                b.Property<bool>("Visible")
                    .HasColumnType("bit");

                b.HasKey("ID");

                b.ToTable("Servizi");
            });

            modelBuilder.Entity("Microsis.Names.Models.User", b =>
            {
                b.Property<Guid>("ID")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("uniqueidentifier");

                b.Property<string>("Email")
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnType("nvarchar(100)");

                b.Property<bool>("IsActive")
                    .HasColumnType("bit");

                b.Property<DateTime?>("LastLogin")
                    .HasColumnType("datetime2");

                b.Property<DateTime>("LastUpdate")
                    .HasColumnType("datetime2");

                b.Property<string>("NomeEsteso")
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnType("nvarchar(100)");

                b.Property<string>("Password")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("Role")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.HasKey("ID");

                b.HasIndex("Email")
                    .IsUnique();

                b.ToTable("Users");
            });
#pragma warning restore 612, 618
        }
    }
}
