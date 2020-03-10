namespace agregaritemsimpresoraslocal
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ImpresorasCambioView")]
    public partial class ImpresorasCambioView
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdCambio { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string NumeroLegajo { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(30)]
        public string Apellido { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(30)]
        public string Nombre { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(15)]
        public string Marca { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(30)]
        public string DescripcionINT { get; set; }

        [Key]
        [Column(Order = 6)]
        [StringLength(10)]
        public string CodigoPieza { get; set; }

        [Key]
        [Column(Order = 7)]
        [StringLength(50)]
        public string DescripcionPieza { get; set; }

        [Key]
        [Column(Order = 8)]
        [StringLength(30)]
        public string DescripcionFalla { get; set; }

        [Key]
        [Column(Order = 9)]
        public DateTime FechaCambio { get; set; }

        [StringLength(65)]
        public string Observaciones { get; set; }
    }
}
