namespace agregaritemsimpresoraslocal
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ImpresorasMarca")]
    public partial class ImpresorasMarca
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ImpresorasMarca()
        {
            ImpresorasCambio = new HashSet<ImpresorasCambio>();
            ImpresorasINT = new HashSet<ImpresorasINT>();
            ImpresorasPieza = new HashSet<ImpresorasPieza>();
        }

        [Key]
        public int IdMarca { get; set; }

        [Required]
        [StringLength(15)]
        public string Marca { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ImpresorasCambio> ImpresorasCambio { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ImpresorasINT> ImpresorasINT { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ImpresorasPieza> ImpresorasPieza { get; set; }
    }
}
