using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace kredit.Models
{
    public class User
    {
        [Key]
        public string IdUser { get; set; }
        public string Nama { get; set; }
        public string Alamat { get; set; }
        public string Pekerjaan { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
        public string Created { get; set; }
        public string Updated { get; set; }
        public string Deleted { get; set; }
        public bool IsDelete { get; set; }
    }
}