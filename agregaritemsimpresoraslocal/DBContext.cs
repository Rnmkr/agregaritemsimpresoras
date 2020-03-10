namespace agregaritemsimpresoraslocal
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DBContext : DbContext
    {
        public DBContext()
        // : base("data source=VM-FORREST;initial catalog=Produccion;user id=BUBBASQL;password=12345678;MultipleActiveResultSets=True;App=EntityFramework")
         : base("data source=BUBBA;initial catalog=Produccion;persist security info=True;user id=BUBBASQL;password=12345678;MultipleActiveResultSets=True;App=EntityFramework")
        // : base("data source=localhost;initial catalog=PRODUCCION; integrated security=True; MultipleActiveResultSets=True;App=EntityFramework")
        {
        }

        public virtual DbSet<ImpresorasCambio> ImpresorasCambio { get; set; }
        public virtual DbSet<ImpresorasFalla> ImpresorasFalla { get; set; }
        public virtual DbSet<ImpresorasINT> ImpresorasINT { get; set; }
        public virtual DbSet<ImpresorasMarca> ImpresorasMarca { get; set; }
        public virtual DbSet<ImpresorasPieza> ImpresorasPieza { get; set; }
        public virtual DbSet<Personal> Personal { get; set; }
        public virtual DbSet<ImpresorasCambioReducidoView> ImpresorasCambioReducidoView { get; set; }
        public virtual DbSet<ImpresorasCambioView> ImpresorasCambioView { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ImpresorasCambio>()
                .Property(e => e.SectorCambio)
                .IsUnicode(false);

            modelBuilder.Entity<ImpresorasCambio>()
                .Property(e => e.Observaciones)
                .IsUnicode(false);

            modelBuilder.Entity<ImpresorasCambio>()
                .Property(e => e.EstadoCambio)
                .IsUnicode(false);

            modelBuilder.Entity<ImpresorasCambio>()
                .Property(e => e.SupervisorModificacion)
                .IsUnicode(false);

            modelBuilder.Entity<ImpresorasFalla>()
                .Property(e => e.DescripcionFalla)
                .IsUnicode(false);

            modelBuilder.Entity<ImpresorasFalla>()
                .HasMany(e => e.ImpresorasCambio)
                .WithRequired(e => e.ImpresorasFalla)
                .HasForeignKey(e => e.FK_IdFalla)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ImpresorasINT>()
                .Property(e => e.DescripcionINT)
                .IsUnicode(false);

            modelBuilder.Entity<ImpresorasINT>()
                .HasMany(e => e.ImpresorasCambio)
                .WithRequired(e => e.ImpresorasINT)
                .HasForeignKey(e => e.FK_IdINT)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ImpresorasINT>()
                .HasMany(e => e.ImpresorasFalla)
                .WithRequired(e => e.ImpresorasINT)
                .HasForeignKey(e => e.FK_IdINT)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ImpresorasINT>()
                .HasMany(e => e.ImpresorasPieza)
                .WithRequired(e => e.ImpresorasINT)
                .HasForeignKey(e => e.FK_IdINT)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ImpresorasMarca>()
                .Property(e => e.Marca)
                .IsUnicode(false);

            modelBuilder.Entity<ImpresorasMarca>()
                .HasMany(e => e.ImpresorasCambio)
                .WithRequired(e => e.ImpresorasMarca)
                .HasForeignKey(e => e.FK_IdMarca)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ImpresorasMarca>()
                .HasMany(e => e.ImpresorasINT)
                .WithRequired(e => e.ImpresorasMarca)
                .HasForeignKey(e => e.FK_IdMarca)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ImpresorasMarca>()
                .HasMany(e => e.ImpresorasPieza)
                .WithRequired(e => e.ImpresorasMarca)
                .HasForeignKey(e => e.FK_IdMarca)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ImpresorasPieza>()
                .Property(e => e.CodigoPieza)
                .IsUnicode(false);

            modelBuilder.Entity<ImpresorasPieza>()
                .Property(e => e.DescripcionPieza)
                .IsUnicode(false);

            modelBuilder.Entity<ImpresorasPieza>()
                .HasMany(e => e.ImpresorasCambio)
                .WithRequired(e => e.ImpresorasPieza)
                .HasForeignKey(e => e.FK_IdPieza)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Personal>()
                .Property(e => e.NumeroLegajo)
                .IsUnicode(false);

            modelBuilder.Entity<Personal>()
                .Property(e => e.NumeroAcceso)
                .IsFixedLength();

            modelBuilder.Entity<Personal>()
                .Property(e => e.Apellido)
                .IsUnicode(false);

            modelBuilder.Entity<Personal>()
                .Property(e => e.Nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Personal>()
                .Property(e => e.SegundoNombre)
                .IsUnicode(false);

            modelBuilder.Entity<Personal>()
                .Property(e => e.FechaIngreso)
                .IsUnicode(false);

            modelBuilder.Entity<Personal>()
                .Property(e => e.Sector)
                .IsUnicode(false);

            modelBuilder.Entity<Personal>()
                .Property(e => e.Domicilio)
                .IsUnicode(false);

            modelBuilder.Entity<Personal>()
                .Property(e => e.DNI)
                .IsUnicode(false);

            modelBuilder.Entity<Personal>()
                .Property(e => e.Telefono)
                .IsUnicode(false);

            modelBuilder.Entity<Personal>()
                .Property(e => e.Celular)
                .IsUnicode(false);

            modelBuilder.Entity<Personal>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<Personal>()
                .Property(e => e.Observaciones)
                .IsUnicode(false);

            modelBuilder.Entity<Personal>()
                .HasMany(e => e.ImpresorasCambio)
                .WithRequired(e => e.Personal)
                .HasForeignKey(e => e.FK_IdTecnico)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ImpresorasCambioReducidoView>()
                .Property(e => e.DescripcionINT)
                .IsUnicode(false);

            modelBuilder.Entity<ImpresorasCambioReducidoView>()
                .Property(e => e.CodigoPieza)
                .IsUnicode(false);

            modelBuilder.Entity<ImpresorasCambioReducidoView>()
                .Property(e => e.DescripcionPieza)
                .IsUnicode(false);

            modelBuilder.Entity<ImpresorasCambioReducidoView>()
                .Property(e => e.DescripcionFalla)
                .IsUnicode(false);

            modelBuilder.Entity<ImpresorasCambioReducidoView>()
                .Property(e => e.Observaciones)
                .IsUnicode(false);

            modelBuilder.Entity<ImpresorasCambioView>()
                .Property(e => e.NumeroLegajo)
                .IsUnicode(false);

            modelBuilder.Entity<ImpresorasCambioView>()
                .Property(e => e.Apellido)
                .IsUnicode(false);

            modelBuilder.Entity<ImpresorasCambioView>()
                .Property(e => e.Nombre)
                .IsUnicode(false);

            modelBuilder.Entity<ImpresorasCambioView>()
                .Property(e => e.Marca)
                .IsUnicode(false);

            modelBuilder.Entity<ImpresorasCambioView>()
                .Property(e => e.DescripcionINT)
                .IsUnicode(false);

            modelBuilder.Entity<ImpresorasCambioView>()
                .Property(e => e.CodigoPieza)
                .IsUnicode(false);

            modelBuilder.Entity<ImpresorasCambioView>()
                .Property(e => e.DescripcionPieza)
                .IsUnicode(false);

            modelBuilder.Entity<ImpresorasCambioView>()
                .Property(e => e.DescripcionFalla)
                .IsUnicode(false);

            modelBuilder.Entity<ImpresorasCambioView>()
                .Property(e => e.Observaciones)
                .IsUnicode(false);
        }
    }
}
