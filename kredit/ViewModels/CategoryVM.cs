using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace kredit.ViewModels
{
    
    public class CategoryVM
    {
        public CategoryVM()
        {
            //default controller
        }

        //edit get 
        public CategoryVM(Models.Category _category)
        {
            IdKategori = _category.Idkategori;
            Nama_kategori = _category.Nama_kategori;
        }
        public string IdKategori { get; set; }
        public string Nama_kategori { get; set; }
    }
}