using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnThi.Models
{
    [Table("KhachHangs")]
    public class KhachHang
    {
        [Key]
        public String IDKhachHang { get; set; }
        public String TenKH { get; set; }
        [AllowHtml]
        public String DiaChi { get; set; }
        public String SoBan { get; set; }
    }
}