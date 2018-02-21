using kredit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace kredit.ViewModels
{
    public class KreditDetailVM
    {
        public KreditDetailVM() { }
        public string Idunit { get; set; }
        public int IdApprove { get; set; }
        public string Merk { get; set; }
        public int Harga { get; set; }
        public int DP { get; set; }
        public int JumlahPinjam { get; set; }
        public int Angsuran { get; set; }
        public int Periode { get; set; }
        public int Jumlah { get; set; }
        public List<KreditDetail> KreditDetailList { get; set; }
    }
}