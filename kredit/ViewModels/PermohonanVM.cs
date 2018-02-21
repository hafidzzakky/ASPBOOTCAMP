using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace kredit.ViewModels
{
    public class PermohonanVM
    {
        public PermohonanVM() { }

        public HttpPostedFileBase Idapprove { get; set; }
        public HttpPostedFileBase Ktpimg { get; set; }
        public HttpPostedFileBase Kkimg { get; set; }
        public HttpPostedFileBase Skkerjaimg { get; set; }
        public HttpPostedFileBase Tagihanlistrikimg { get; set; }
    }
}