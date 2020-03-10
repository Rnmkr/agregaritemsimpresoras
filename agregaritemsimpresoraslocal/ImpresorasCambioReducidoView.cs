namespace agregaritemsimpresoraslocal
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ImpresorasCambioReducidoView")]
    public partial class ImpresorasCambioReducidoView
    {
        [Key]
        [Column(Order = 0)]
        public DateTime FechaCambio { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(30)]
        public string DescripcionINT { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(10)]
        public string CodigoPieza { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(50)]
        public string DescripcionPieza { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(30)]
        public string DescripcionFalla { get; set; }

        [StringLength(65)]
        public string Observaciones { get; set; }
    }
}
