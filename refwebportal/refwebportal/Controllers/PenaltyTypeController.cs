using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using refwebportal;

namespace refwebportal.Controllers
{
    public class PenaltyTypeController : Controller
    {
        private FROdataEntities1 db = new FROdataEntities1();

        // GET: PenaltyType
        public async Task<ActionResult> Index()
        {
            return View(await db.PenaltyTypes.ToListAsync());
        }

        // GET: PenaltyType/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PenaltyType penaltyType = await db.PenaltyTypes.FindAsync(id);
            if (penaltyType == null)
            {
                return HttpNotFound();
            }
            return View(penaltyType);
        }

        // GET: PenaltyType/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PenaltyType/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Code,Description")] PenaltyType penaltyType)
        {
            if (ModelState.IsValid)
            {
                db.PenaltyTypes.Add(penaltyType);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(penaltyType);
        }

        // GET: PenaltyType/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PenaltyType penaltyType = await db.PenaltyTypes.FindAsync(id);
            if (penaltyType == null)
            {
                return HttpNotFound();
            }
            return View(penaltyType);
        }

        // POST: PenaltyType/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Code,Description")] PenaltyType penaltyType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(penaltyType).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(penaltyType);
        }

        // GET: PenaltyType/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PenaltyType penaltyType = await db.PenaltyTypes.FindAsync(id);
            if (penaltyType == null)
            {
                return HttpNotFound();
            }
            return View(penaltyType);
        }

        // POST: PenaltyType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            PenaltyType penaltyType = await db.PenaltyTypes.FindAsync(id);
            db.PenaltyTypes.Remove(penaltyType);
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
