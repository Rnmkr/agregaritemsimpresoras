namespace agregaritemsimpresoraslocal
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ImpresorasINT")]
    public partial class ImpresorasINT
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ImpresorasINT()
        {
            ImpresorasCambio = new HashSet<ImpresorasCambio>();
            ImpresorasFalla = new HashSet<ImpresorasFalla>();
            ImpresorasPieza = new HashSet<ImpresorasPieza>();
        }

        [Key]
        public int IdINT { get; set; }

        public int FK_IdMarca { get; set; }

        [Required]
        [StringLength(30)]
        public string DescripcionINT { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ImpresorasCambio> ImpresorasCambio { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ImpresorasFalla> ImpresorasFalla { get; set; }

        public virtual ImpresorasMarca ImpresorasMarca { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ImpresorasPieza> ImpresorasPieza { get; set; }
    }
}
