using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OnThi.Models
{
    [Table("SanPhams")]
    public class SanPham
    {
        [Key]
        public String IDSanPham { get; set; }
        public String TenSanPham { get; set; }
        public String SoLuong { get; set; }
    }
}