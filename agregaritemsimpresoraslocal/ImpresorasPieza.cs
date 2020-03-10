namespace agregaritemsimpresoraslocal
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ImpresorasPieza")]
    public partial class ImpresorasPieza
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ImpresorasPieza()
        {
            ImpresorasCambio = new HashSet<ImpresorasCambio>();
        }

        [Key]
        public int IdPieza { get; set; }

        public int FK_IdMarca { get; set; }

        public int FK_IdINT { get; set; }

        [Required]
        [StringLength(10)]
        public string CodigoPieza { get; set; }

        [Required]
        [StringLength(50)]
        public string DescripcionPieza { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ImpresorasCambio> ImpresorasCambio { get; set; }

        public virtual ImpresorasINT ImpresorasINT { get; set; }

        public virtual ImpresorasMarca ImpresorasMarca { get; set; }
    }
}
