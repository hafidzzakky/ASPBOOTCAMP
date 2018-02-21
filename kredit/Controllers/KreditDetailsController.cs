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
    [Authorize(Roles = "User")]
    public class KreditDetailsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: KreditDetails
        public async Task<ActionResult> Index()
        {
            var currentuser = await db.Users.Where(x => x.UserName == User.Identity.Name).SingleOrDefaultAsync();
            var kreditDetail = db.KreditDetail.Include(k => k.Permohonan).Include(k => k.TransaksiKredit).Where(x => x.CreatedBy.Id == currentuser.Id && x.Permohonan.IsnotValid == true);
            return View(await kreditDetail.ToListAsync());
        }

        // GET: KreditDetails/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KreditDetail kreditDetail = await db.KreditDetail.Include(x => x.Permohonan).Include(x => x.TransaksiKredit).Include(x => x.Unit).Include(x => x.Unit.NamaKategori).Where(x => x.IdKreditDetail == id).SingleOrDefaultAsync();
            //KreditDetail kreditDetail = await db.KreditDetail.FindAsync(id);
            if (kreditDetail == null)
            {
                return HttpNotFound();
            }
            return View(kreditDetail);
        }
        // GET: KreditDetails/Create
        public async Task<ActionResult> Create()
        {
            ViewBag.IdApprove = new SelectList(db.Permohonan, "IdApprove", "IdApprove");
            ViewBag.IdKredit = new SelectList(db.TransaksiKredit, "IdKredit", "IdKredit");

            var unit_category = await db.Categories.Where(x => x.IsDeleted == false).Select(i => new SelectListItem()
            {
                Text = i.Nama_kategori,
                Value = i.Idkategori,
                Selected = false
            }).ToArrayAsync();
            ViewBag.Category = unit_category;

            var unit = await db.Units.Where(x => x.IsDeleted == false).Select(i => new SelectListItem()
            {
                Text = i.Namabarang,
                Value = i.Idunit,
                Selected = false
            }).ToArrayAsync();
            ViewBag.Unit = unit;

            return View();
        }

        // POST: KreditDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ViewModels.KreditDetailVM _kreditdetail)
        {
            if (ModelState.IsValid)
            {
                var currentdate = DateTime.UtcNow;
                var currentuser = await db.Users.Where(x => x.UserName == User.Identity.Name).SingleOrDefaultAsync();
                var kredit = new TransaksiKredit();
                kredit.IdApprove = _kreditdetail.IdApprove;
                kredit.Created = currentdate;
                kredit.CreatedBy = currentuser;
                db.TransaksiKredit.Add(kredit);
                await db.SaveChangesAsync();

                var inputkredit = new KreditDetail()
                {
                    IdKredit = kredit.IdKredit,
                    Idunit = _kreditdetail.Idunit,
                    IdApprove = _kreditdetail.IdApprove,
                    DP = _kreditdetail.DP,
                    JumlahPinjam = _kreditdetail.JumlahPinjam,
                    Angsuran = _kreditdetail.Angsuran,
                    Periode = _kreditdetail.Periode,
                    Created = currentdate,
                    CreatedBy = currentuser,
                    Jumlah = _kreditdetail.Jumlah

                };
                db.KreditDetail.Add(inputkredit);
                await db.SaveChangesAsync();

                //update status valid idApprove
                var idpermohonan = await db.Permohonan.FindAsync(_kreditdetail.IdApprove);
                idpermohonan.IsnotValid = true;
                db.Entry(idpermohonan).State = EntityState.Modified;
                await db.SaveChangesAsync();

                //update stok di barang
                var unit = await db.Units.FindAsync(_kreditdetail.Idunit);
                unit.Stok = unit.Stok - _kreditdetail.Jumlah;
                db.Entry(idpermohonan).State = EntityState.Modified;
                await db.SaveChangesAsync();

                //viewbag
                var unit_category = await db.Categories.Where(x => x.IsDeleted == false).Select(i => new SelectListItem()
                {
                    Text = i.Nama_kategori,
                    Value = i.Idkategori,
                    Selected = false
                }).ToArrayAsync();
                ViewBag.Category = unit_category;

                return RedirectToAction("Index");
            }

            //ViewBag.IdApprove = new SelectList(db.Permohonan, "IdApprove", "Kode", kreditDetail.IdApprove);
            //ViewBag.IdKredit = new SelectList(db.TransaksiKredit, "IdKredit", "IdKredit", kreditDetail.IdKredit);
            return View(_kreditdetail);
        }

        
        public JsonResult SaveOrder(ViewModels.KreditDetailVM _kreditdetail)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                var currentdate = DateTime.UtcNow;
                var currentuser = db.Users.Where(x => x.UserName == User.Identity.Name).SingleOrDefault();
                var kredit = new TransaksiKredit();
                kredit.IdApprove = _kreditdetail.IdApprove;
                kredit.Created = currentdate;
                kredit.CreatedBy = currentuser;
                db.TransaksiKredit.Add(kredit);
                db.SaveChanges();

                foreach (var item in _kreditdetail.KreditDetailList)
                {
                    var inputkredit = new KreditDetail()
                    {
                        IdKredit = kredit.IdKredit,
                        Idunit = item.Idunit,
                        IdApprove = _kreditdetail.IdApprove,
                        DP = item.DP,
                        JumlahPinjam = item.JumlahPinjam,
                        Angsuran = item.Angsuran,
                        Periode = item.Periode,
                        Created = currentdate,
                        CreatedBy = currentuser,
                        Jumlah = item.Jumlah
                    };

                    db.KreditDetail.Add(inputkredit);
                    db.SaveChanges();
                    //update stok di barang
                    var unit = db.Units.Find(item.Idunit);
                    unit.Stok = unit.Stok - item.Jumlah;
                    db.Entry(unit).State = EntityState.Modified;
                    db.SaveChanges();
                }

                //db.SaveChanges();
                status = true;

                //update status valid idApprove
                var idpermohonan = db.Permohonan.Find(_kreditdetail.IdApprove);
                idpermohonan.IsnotValid = true;
                db.Entry(idpermohonan).State = EntityState.Modified;
                db.SaveChanges();
            }
            else
            {
                status = false;
            }
            return new JsonResult { Data = new { status = status} };
        }

        // GET: KreditDetails/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KreditDetail kreditDetail = await db.KreditDetail.FindAsync(id);
            if (kreditDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdApprove = new SelectList(db.Permohonan, "IdApprove", "Kode", kreditDetail.IdApprove);
            ViewBag.IdKredit = new SelectList(db.TransaksiKredit, "IdKredit", "IdKredit", kreditDetail.IdKredit);
            return View(kreditDetail);
        }

        // POST: KreditDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IdKreditDetail,IdKredit,IdApprove,DP,JumlahPinjam,Angsuran,Periode,IsDeleted,Created,Updated,Deleted,Approved")] KreditDetail kreditDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kreditDetail).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.IdApprove = new SelectList(db.Permohonan, "IdApprove", "Kode", kreditDetail.IdApprove);
            ViewBag.IdKredit = new SelectList(db.TransaksiKredit, "IdKredit", "IdKredit", kreditDetail.IdKredit);
            return View(kreditDetail);
        }

        // GET: KreditDetails/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KreditDetail kreditDetail = await db.KreditDetail.FindAsync(id);
            if (kreditDetail == null)
            {
                return HttpNotFound();
            }
            return View(kreditDetail);
        }

        // POST: KreditDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            KreditDetail kreditDetail = await db.KreditDetail.FindAsync(id);
            db.KreditDetail.Remove(kreditDetail);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> GetHargaUnit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //var unit = await db.Units.FindAsync(id);
            var unit = await db.Units.Where(x => x.Idunit == id && x.IsDeleted == false).ToListAsync();
            if (unit == null)
            {
                return HttpNotFound();
            }
            var i = unit.Select(x=> new { x.Harga, x.Stok});
            return Json(i, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> GetStokUnit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var permohonan = await db.Units.FindAsync(id);
            if (permohonan == null)
            {
                return HttpNotFound();
            }
            var i = permohonan.Stok;
            return Json(i, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> GetMerkUnit(string id)
        {
            var merk = await db.Units.Where(x=>x.IsDeleted==false && x.Idkategori == id).ToListAsync();
            if (merk == null)
            {
                return HttpNotFound();
            }
            var idmerk = merk.Select(x => new { idmerk = x.Idkategori, idbarang= x.Idunit, unit = x.Namabarang }).ToList();
            return Json(idmerk, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> GetKategoriUnit()
        {
            var merk = await db.Categories.Where(x => x.IsDeleted == false).ToListAsync();
            if (merk == null)
            {
                return HttpNotFound();
            }
            var idmerk = merk.Select(x => new { idmerk = x.Idkategori, merk = x.Nama_kategori }).ToList();
            return Json(idmerk, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> GetIdApprove()
        {
            var currentuser = await db.Users.Where(x => x.UserName == User.Identity.Name).SingleOrDefaultAsync();
            var idpermohonan = await db.Permohonan.Where(x => x.CreatedBy.Id == currentuser.Id && x.IsDeleted == false && x.IsnotValid == false && x.status==status.Accepted && x.statussurvey==status.Accepted).ToListAsync();
            if (idpermohonan == null)
            {
                return HttpNotFound();
            }
            var id = idpermohonan.Select(x=>x.IdApprove).ToList();
            return Json(id, JsonRequestBehavior.AllowGet);
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
