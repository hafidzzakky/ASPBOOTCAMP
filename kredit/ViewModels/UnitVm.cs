using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace kredit.ViewModels
{
    public class UnitVm
    {
        public UnitVm() { }

        public UnitVm(Models.Unit _unit)
        {
            Idunit = _unit.Idunit;
            Namabarang = _unit.Namabarang;
            Harga = _unit.Harga;
            Stok = _unit.Stok;
        }

        //yang akan tampil 
        public string Idunit { get; set; }
        [Required(ErrorMessage = "Nama Barang Tidak Boleh Kosong")]
        public string Namabarang { get; set; }
        [Required(ErrorMessage = "Harga Tidak Boleh Kosong")]
        public int Harga { get; set; }
        public string Category { get; set; }
        public int Stok { get; set; }
    }
}