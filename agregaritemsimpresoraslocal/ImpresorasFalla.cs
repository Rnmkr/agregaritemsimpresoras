namespace agregaritemsimpresoraslocal
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ImpresorasFalla")]
    public partial class ImpresorasFalla
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ImpresorasFalla()
        {
            ImpresorasCambio = new HashSet<ImpresorasCambio>();
        }

        [Key]
        public int IdFalla { get; set; }

        public int FK_IdINT { get; set; }

        [Required]
        [StringLength(30)]
        public string DescripcionFalla { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ImpresorasCambio> ImpresorasCambio { get; set; }

        public virtual ImpresorasINT ImpresorasINT { get; set; }
    }
}
