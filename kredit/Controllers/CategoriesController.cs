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
    public class CategoriesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Categories
        public async Task<ActionResult> Index()
        {
            var category = await db.Categories.Where(x => x.IsDeleted == false).ToListAsync();
            return View(category);
        }

        // GET: Categories/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = await db.Categories.FindAsync(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // GET: Categories/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ViewModels.CategoryVM _category)
        {
            if (ModelState.IsValid)
            {
                var currentdate = DateTime.UtcNow;
                var currentuser = await db.Users.Where(x => x.UserName == User.Identity.Name).SingleOrDefaultAsync();
                var Kategori = new Category()
                {
                    Idkategori = _category.Nama_kategori.Replace(" ","_").ToLower(),
                    Nama_kategori = _category.Nama_kategori,
                    Created = currentdate,
                    CreatedBy = currentuser

                };
                db.Categories.Add(Kategori);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(_category);
        }

        // GET: Categories/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = await db.Categories.FindAsync(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            var unitcategory = new ViewModels.CategoryVM(category);
            return View(unitcategory);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ViewModels.CategoryVM _category)
        {
            if (ModelState.IsValid)
            {
                var currentdate = DateTime.UtcNow;
                var currentuser = await db.Users.Where(x => x.UserName == User.Identity.Name).SingleOrDefaultAsync();

                var kategori_unit = await db.Categories.FindAsync(_category.IdKategori);

                kategori_unit.Nama_kategori = _category.Nama_kategori;
                kategori_unit.Updated = currentdate;
                kategori_unit.UpdatedBy = currentuser;

                db.Entry(kategori_unit).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(_category);
        }

        // GET: Categories/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = await db.Categories.FindAsync(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            //var unitcategory = new ViewModels.CategoryVM(category);
            return View(category);
        }

        // POST: Categories/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            var currentdate = DateTime.UtcNow;
            var currentuser = await db.Users.Where(x => x.UserName == User.Identity.Name).SingleOrDefaultAsync();
            Category category = await db.Categories.FindAsync(id);
            category.IsDeleted = true;
            category.Deleted = currentdate;
            category.DeletedBy = currentuser;
            db.Entry(category).State = EntityState.Modified;
            //db.Categories.Remove(category);
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
