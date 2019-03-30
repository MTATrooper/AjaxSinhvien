namespace AjaxSinhvien.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SINHVIEN")]
    public partial class SINHVIEN
    {
        public int ID { get; set; }

        [StringLength(50)]
        public string TENSV { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NGAYSINH { get; set; }

        [StringLength(50)]
        [Column(TypeName = "ntext")]
        public string QUEQUAN { get; set; }

        public int? MADANTOC { get; set; }

        public int? MALOP { get; set; }

        public virtual DANTOC DANTOC { get; set; }

        public virtual LOP LOP { get; set; }
    }
}
