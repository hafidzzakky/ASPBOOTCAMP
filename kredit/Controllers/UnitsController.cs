using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using kredit.Models;

namespace kredit.Controllers
{
    [Authorize(Roles = "Admin")]    
    public class UnitsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Units
        public async Task<ActionResult> Index()
        {
            var unit = await db.Units.Where(x => x.IsDeleted == false).Include("NamaKategori").ToListAsync();
            return View(unit);
        }

        // GET: Units/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Unit unit = await db.Units.Include(x=>x.NamaKategori).Where(x=>x.Idunit==id).SingleOrDefaultAsync();
            //Unit unit = await db.Units.FindAsync(id);
            if (unit == null)
            {
                return HttpNotFound();
            }
            return View(unit);
        }

        // GET: Units/Create
        public async Task<ActionResult> Create()
        {
            var unit_category = await db.Categories.Where(x => x.IsDeleted == false).Select(i => new SelectListItem()
            {
                Text = i.Nama_kategori,
                Value = i.Idkategori,
                Selected = false
            }).ToArrayAsync();
            ViewBag.Category = unit_category;
            return View();
        }

        // POST: Units/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ViewModels.UnitVm _unit)
        {
            if (ModelState.IsValid)
            {
                var currenttime = DateTime.UtcNow;
                var currentuser = await db.Users.Where(x => x.UserName == User.Identity.Name).SingleOrDefaultAsync();
                var input = new Unit()
                    {
                        Idunit = _unit.Namabarang.Replace(" ", "_").ToLower(),
                        Namabarang = _unit.Namabarang,
                        Harga = _unit.Harga,
                        Created = currenttime,
                        CreatedBy = currentuser,
                        Idkategori =  _unit.Category,
                        Stok = _unit.Stok
                    };
                db.Units.Add(input);
                var result = await db.SaveChangesAsync();
                if(result>0)
                return RedirectToAction("Index");
            }
            var unit_category = await db.Categories.Where(x => x.IsDeleted == false).Select(i => new SelectListItem()
            {
                Text = i.Nama_kategori,
                Value = i.Idkategori,
                Selected = false
            }).ToArrayAsync();
            ViewBag.Category = unit_category;
            return View(_unit);
        }

        // GET: Units/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var unit = await db.Units.FindAsync(id);

            if (unit == null)
            {
                return HttpNotFound();
            }

            var unit_category = await db.Categories.Where(x => x.IsDeleted == false).Select(i => new SelectListItem()
            {
                Text = i.Nama_kategori,
                Value = i.Idkategori,
                Selected = false
            }).ToArrayAsync();
            foreach(var item in unit_category)
            {
                if(unit.Idkategori == item.Value)
                {
                    item.Selected = true;
                }
            }
            ViewBag.Category = unit_category;
            var unitvm = new ViewModels.UnitVm(unit);
            return View(unitvm);
        }

        // POST: Units/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ViewModels.UnitVm _unit)
        {
            if (ModelState.IsValid)
            {
                var currentdate = DateTime.UtcNow;
                var currentuser = await db.Users.Where(x => x.UserName == User.Identity.Name).SingleOrDefaultAsync();

                var unitupdate = await db.Units.FindAsync(_unit.Idunit);
                unitupdate.Namabarang = _unit.Namabarang;
                unitupdate.Harga = _unit.Harga;
                unitupdate.Idkategori = _unit.Category;
                unitupdate.Updated = currentdate;
                unitupdate.UpdatedBy = currentuser;
                unitupdate.Stok = _unit.Stok;
                
                db.Entry(unitupdate).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(_unit);
        }

        // GET: Units/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Unit unit = await db.Units.FindAsync(id);
            if (unit == null)
            {
                return HttpNotFound();
            }
            return View(unit);
        }

        // POST: Units/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            var currentdate = DateTime.UtcNow;
            var currentuser = await db.Users.Where(x => x.UserName == User.Identity.Name).SingleOrDefaultAsync();
            Unit unit = await db.Units.FindAsync(id);
            unit.IsDeleted = true;
            unit.Deleted = currentdate;
            unit.DeletedBy = currentuser;
            db.Entry(unit).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
