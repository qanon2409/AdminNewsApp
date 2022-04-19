using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AdminNewsApp.Models
{
    public partial class PROYECTO2TIContext : DbContext
    {
        public PROYECTO2TIContext()
        {
        }

        public PROYECTO2TIContext(DbContextOptions<PROYECTO2TIContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Autor> Autors { get; set; } = null!;
        public virtual DbSet<Categorium> Categoria { get; set; } = null!;
        public virtual DbSet<Fuente> Fuentes { get; set; } = null!;
        public virtual DbSet<Noticia> Noticias { get; set; } = null!;
        public virtual DbSet<Pai> Pais { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
     
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Autor>(entity =>
            {
                entity.ToTable("AUTOR");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE");

                entity.HasMany(d => d.Idnot2s)
                    .WithMany(p => p.Idautors)
                    .UsingEntity<Dictionary<string, object>>(
                        "AutorEscribeNot",
                        l => l.HasOne<Noticia>().WithMany().HasForeignKey("Idnot2").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_NOTES"),
                        r => r.HasOne<Autor>().WithMany().HasForeignKey("Idautor").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_AUTORMOD"),
                        j =>
                        {
                            j.HasKey("Idautor", "Idnot2");

                            j.ToTable("AutorEscribeNot");

                            j.IndexerProperty<int>("Idautor").HasColumnName("IDAutor");

                            j.IndexerProperty<int>("Idnot2").HasColumnName("IDNOT2");
                        });
            });

            modelBuilder.Entity<Categorium>(entity =>
            {
                entity.ToTable("CATEGORIA");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE");
            });

            modelBuilder.Entity<Fuente>(entity =>
            {
                entity.ToTable("FUENTE");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Idpais).HasColumnName("IDPAIS");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE");

                entity.HasOne(d => d.IdpaisNavigation)
                    .WithMany(p => p.Fuentes)
                    .HasForeignKey(d => d.Idpais)
                    .HasConstraintName("FK_PAIS_FUENTE");
            });

            modelBuilder.Entity<Noticia>(entity =>
            {
                entity.ToTable("NOTICIAS");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Descripcion)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPCION");

                entity.Property(e => e.Idcat).HasColumnName("IDCAT");

                entity.Property(e => e.Idfuente).HasColumnName("IDFUENTE");

                entity.Property(e => e.Idpais).HasColumnName("IDPAIS");

                entity.Property(e => e.Titulo)
                    .IsUnicode(false)
                    .HasColumnName("TITULO");

                entity.Property(e => e.Urlnoticia)
                    .IsUnicode(false)
                    .HasColumnName("URLNOTICIA");

                entity.HasOne(d => d.IdcatNavigation)
                    .WithMany(p => p.Noticia)
                    .HasForeignKey(d => d.Idcat)
                    .HasConstraintName("FK_NOT_CAT");

                entity.HasOne(d => d.IdfuenteNavigation)
                    .WithMany(p => p.Noticia)
                    .HasForeignKey(d => d.Idfuente)
                    .HasConstraintName("FK_NOT_FUENTE");

                entity.HasOne(d => d.IdpaisNavigation)
                    .WithMany(p => p.Noticia)
                    .HasForeignKey(d => d.Idpais)
                    .HasConstraintName("FK_NOT_PAIS");
            });

            modelBuilder.Entity<Pai>(entity =>
            {
                entity.ToTable("PAIS");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
