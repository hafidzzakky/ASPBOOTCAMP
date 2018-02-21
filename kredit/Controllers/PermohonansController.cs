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
    [Authorize(Roles = "User, Admin")]
    public class PermohonansController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        //public async Task<string> GenerateKode(string id)
        //{
        //    var counter = 1;
        //    var idku = await db.Permohonan.Where(x => x.Kode == id).ToListAsync();
        //    if(idku == null)
        //    if ()
        //    var tempIdApprove = id.Replace(" ", "_").ToLower();
        //    var idapprove = await db.Permohonan.Where(x => x.Kode == id).ToListAsync();
        //    if (idapprove == null)
        //    {
        //        var sameIdApprove = idapprove.Find(x => x.Kode == tempIdApprove);
        //        if (sameIdApprove == null)
        //        {
        //            return tempIdApprove;
        //        }
        //        else
        //        {
        //            var notfound = true;
        //            var counter = 1;
        //            while (notfound)
        //            {
        //                tempIdApprove = id.Replace(" ", "_").ToLower() + "-" + counter;
        //                var checkidapprove = idapprove.Find(x => x.Kode == tempIdApprove);
        //                if (checkidapprove == null)
        //                {
        //                    notfound = false;
        //                }
        //                counter++;
        //            }
        //            return tempIdApprove;
        //        }
        //    }
        //    else
        //    {
        //        return tempIdApprove;
        //    }
        //}

        //GET: Permohonans
        public async Task<ActionResult> Index()
        {
            var currentuser = await db.Users.Where(x => x.UserName == User.Identity.Name).SingleOrDefaultAsync();
            var permohonan = await db.Permohonan.Where(x => x.IsDeleted == false && x.CreatedBy.Id == currentuser.Id).OrderByDescending(x=>x.Created).ToListAsync();
            return View(permohonan);
        }

        // GET: Permohonans/Details/5
        public async Task<ActionResult> Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Permohonan permohonan = await db.Permohonan.FindAsync(id);
            if (permohonan == null)
            {
                return HttpNotFound();
            }
            return View(permohonan);
        }

        // GET: Permohonans/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Permohonans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ViewModels.PermohonanVM _permohonan)
        {
            if (ModelState.IsValid)
            {
                var currentdate = DateTime.UtcNow;
                var currentuser = await db.Users.Where(x => x.UserName == User.Identity.Name).SingleOrDefaultAsync();
                var permohonankredit = new Permohonan()
                {
                    Kode = "permohonan - "+currentuser.noktp,
                    ktpimg = Helpers.FileHelperUpload.UploadKtp(_permohonan.Ktpimg),
                    kkimg = Helpers.FileHelperUpload.UploadKK(_permohonan.Kkimg),
                    skkerjaimg = Helpers.FileHelperUpload.UploadSKKerja(_permohonan.Skkerjaimg),
                    tagihanlistrikimg = Helpers.FileHelperUpload.UploadTagihanListrik(_permohonan.Tagihanlistrikimg),
                    Created = currentdate,
                    CreatedBy = currentuser,
                    status = status.Waiting
                };
                db.Permohonan.Add(permohonankredit);
                var result = await db.SaveChangesAsync();
                if(result>0)
                    return RedirectToAction("Index");
            }

            return View(_permohonan);
        }

        // GET: Permohonans/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Permohonan permohonan = await db.Permohonan.FindAsync(id);
            if (permohonan == null)
            {
                return HttpNotFound();
            }
            return View(permohonan);
        }

        // POST: Permohonans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Idapprove,ktpimg,kkimg,skkerjaimg,tagihanlistrikimg,Isapproved,IsDeleted,Created,Updated,Deleted,Approved")] Permohonan permohonan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(permohonan).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(permohonan);
        }

        // GET: Permohonans/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Permohonan permohonan = await db.Permohonan.FindAsync(id);
            if (permohonan == null)
            {
                return HttpNotFound();
            }
            return View(permohonan);
        }

        // POST: Permohonans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            Permohonan permohonan = await db.Permohonan.FindAsync(id);
            db.Permohonan.Remove(permohonan);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<ActionResult> cekrecord()
        {
            var notfound = true;
            var currentuser = await db.Users.Where(x => x.UserName == User.Identity.Name).SingleOrDefaultAsync();
            
            var cek = db.Permohonan.Where(x => x.CreatedBy.Id == currentuser.Id && x.IsnotValid==false).Count();
            if (cek > 0)
            {
                return Json(notfound=true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(notfound = false, JsonRequestBehavior.AllowGet);
            }
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
