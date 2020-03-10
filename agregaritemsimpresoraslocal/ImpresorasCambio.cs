namespace agregaritemsimpresoraslocal
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ImpresorasCambio")]
    public partial class ImpresorasCambio
    {
        [Key]
        public int IdCambio { get; set; }

        public int FK_IdTecnico { get; set; }

        public DateTime FechaCambio { get; set; }

        public int FK_IdMarca { get; set; }

        public int FK_IdINT { get; set; }

        public int FK_IdPieza { get; set; }

        public int FK_IdFalla { get; set; }

        [StringLength(16)]
        public string SectorCambio { get; set; }

        [StringLength(65)]
        public string Observaciones { get; set; }

        [Required]
        [StringLength(10)]
        public string EstadoCambio { get; set; }

        [StringLength(30)]
        public string SupervisorModificacion { get; set; }

        public DateTime? FechaModificacion { get; set; }

        public virtual ImpresorasFalla ImpresorasFalla { get; set; }

        public virtual ImpresorasINT ImpresorasINT { get; set; }

        public virtual ImpresorasMarca ImpresorasMarca { get; set; }

        public virtual ImpresorasPieza ImpresorasPieza { get; set; }

        public virtual Personal Personal { get; set; }
    }
}
