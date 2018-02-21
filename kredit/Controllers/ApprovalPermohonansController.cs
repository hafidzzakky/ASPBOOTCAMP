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
    public class ApprovalPermohonansController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ApprovalPermohonans
        public async Task<ActionResult> Index()
        {
            var approvalpermohonan = await db.Permohonan.Where(x => x.status == status.Waiting && x.IsDeleted == false).ToListAsync();
            return View(approvalpermohonan);
        }

        public async Task<ActionResult> SurveyList()
        {
            var approvalpermohonan = await db.Permohonan.Where(x => x.status == status.Accepted && x.IsDeleted == false).OrderByDescending(x=>x.statussurvey == status.Waiting).ToListAsync();
            return View(approvalpermohonan);
        }

        // GET: ApprovalPermohonans/Details/5
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

        // GET: ApprovalPermohonans/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ApprovalPermohonans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Idapprove,ktpimg,kkimg,skkerjaimg,tagihanlistrikimg,status,IsDeleted,Created,Updated,Deleted,Approved")] Permohonan permohonan)
        {
            if (ModelState.IsValid)
            {
                db.Permohonan.Add(permohonan);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(permohonan);
        }

        // GET: ApprovalPermohonans/Edit/5
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

        // POST: ApprovalPermohonans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Idapprove,ktpimg,kkimg,skkerjaimg,tagihanlistrikimg,status,IsDeleted,Created,Updated,Deleted,Approved")] Permohonan permohonan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(permohonan).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(permohonan);
        }

        // GET: ApprovalPermohonans/Delete/5
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

        // POST: ApprovalPermohonans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            Permohonan permohonan = await db.Permohonan.FindAsync(id);
            db.Permohonan.Remove(permohonan);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        //approval syarat umum
        // POST: Permohonans/Approve/5
        public async Task<ActionResult> Approve(int id)
        {
            var currentdate = DateTime.UtcNow;
            var currentuser = await db.Users.Where(x => x.UserName == User.Identity.Name).SingleOrDefaultAsync();
            Permohonan permohonan = await db.Permohonan.FindAsync(id);
            permohonan.status = status.Accepted;
            permohonan.Approved = currentdate;
            permohonan.ApprovedBy = currentuser;
            db.Entry(permohonan).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Reject(int id)
        {
            var currentdate = DateTime.UtcNow;
            var currentuser = await db.Users.Where(x => x.UserName == User.Identity.Name).SingleOrDefaultAsync();
            Permohonan permohonan = await db.Permohonan.FindAsync(id);
            permohonan.status = status.Rejected;
            db.Entry(permohonan).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        //approval survey dan kontrak 
        public async Task<ActionResult> SurveyApprove(int id)
        {
            var currentdate = DateTime.UtcNow;
            var currentuser = await db.Users.Where(x => x.UserName == User.Identity.Name).SingleOrDefaultAsync();
            Permohonan permohonan = await db.Permohonan.FindAsync(id);
            permohonan.statussurvey = status.Accepted;
            permohonan.Approved = currentdate;
            permohonan.ApprovedBy = currentuser;
            db.Entry(permohonan).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return RedirectToAction("SurveyList");
        }

        public async Task<ActionResult> SurveyReject(int id)
        {
            var currentdate = DateTime.UtcNow;
            var currentuser = await db.Users.Where(x => x.UserName == User.Identity.Name).SingleOrDefaultAsync();
            Permohonan permohonan = await db.Permohonan.FindAsync(id);
            permohonan.statussurvey = status.Rejected;
            db.Entry(permohonan).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return RedirectToAction("SurveyList");
        }

        public async Task<ActionResult> SurveyDetails(int id)
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
