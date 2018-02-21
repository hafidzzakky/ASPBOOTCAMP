using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace kredit.Models
{
    public class Category : Metadata
    {
        [Key]
        public string Idkategori { get; set; }
        public string Nama_kategori { get; set; }
    }

    public class Unit : Metadata
    {
        [Key]
        public string Idunit { get; set; }
        public string Idkategori { get; set; }
        public string Namabarang { get; set; }
        public int Harga { get; set; }
        public int Stok { get; set; }
        public Category NamaKategori { get; set; }
    }

    public class Permohonan : Metadata
    {
        [Key]
        public int IdApprove { get; set; }
        public string Kode { get; set; }
        public string ktpimg { get; set; }
        public string kkimg { get; set; }
        public string skkerjaimg { get; set; }
        public string tagihanlistrikimg { get; set; }
        public status status { get; set; }
        public status statussurvey { get; set; }
        public bool IsnotValid { get; set; }

    }

    public class TransaksiKredit : Metadata
    {
        [Key]
        public int IdKredit { get; set; }
        public int IdApprove { get; set; }
    }

    public class KreditDetail : Metadata
    {
        [Key]
        public int IdKreditDetail { get; set; }
        public string Idunit { get; set; }
        public int IdKredit { get; set; }
        public int IdApprove { get; set; }
        public int Jumlah { get; set; }
        public int DP { get; set; }
        public int JumlahPinjam { get; set; }
        public int Angsuran { get; set; }
        public int Periode { get; set; }
        public Permohonan Permohonan { get; set; }
        public TransaksiKredit TransaksiKredit { get; set; }
        public Unit Unit { get; set; }
    }

    public class Metadata
    {
        public bool IsDeleted { get; set; }
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset Updated { get; set; }
        public DateTimeOffset Deleted { get; set; }
        public DateTimeOffset Approved { get; set; }
        public virtual ApplicationUser CreatedBy { get; set; }
        public virtual ApplicationUser UpdatedBy { get; set; }
        public virtual ApplicationUser DeletedBy { get; set; }
        public virtual ApplicationUser ApprovedBy { get; set; }
    }

    public enum status
    {
        Waiting,
        Accepted,
        Rejected
    }

}