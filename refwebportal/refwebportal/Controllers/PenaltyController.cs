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
    public class PenaltyController : Controller
    {
        private FROdataEntities1 db = new FROdataEntities1();

        // GET: Penalty
        public async Task<ActionResult> Index()
        {
            var penalties = db.Penalties.Include(p => p.GamePlayer);
            return View(await penalties.ToListAsync());
        }

        // GET: Penalty/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Penalty penalty = await db.Penalties.FindAsync(id);
            if (penalty == null)
            {
                return HttpNotFound();
            }
            return View(penalty);
        }

        // GET: Penalty/Create
        public ActionResult Create()
        {
            ViewBag.GamePlayerId = new SelectList(db.GamePlayers, "Id", "Id");
            return View();
        }

        // POST: Penalty/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,GamePlayerId,PenaltyTypeId,PenaltyTime")] Penalty penalty)
        {
            if (ModelState.IsValid)
            {
                db.Penalties.Add(penalty);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.GamePlayerId = new SelectList(db.GamePlayers, "Id", "Id", penalty.GamePlayerId);
            return View(penalty);
        }

        // GET: Penalty/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Penalty penalty = await db.Penalties.FindAsync(id);
            if (penalty == null)
            {
                return HttpNotFound();
            }
            ViewBag.GamePlayerId = new SelectList(db.GamePlayers, "Id", "Id", penalty.GamePlayerId);
            return View(penalty);
        }

        // POST: Penalty/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,GamePlayerId,PenaltyTypeId,PenaltyTime")] Penalty penalty)
        {
            if (ModelState.IsValid)
            {
                db.Entry(penalty).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.GamePlayerId = new SelectList(db.GamePlayers, "Id", "Id", penalty.GamePlayerId);
            return View(penalty);
        }

        // GET: Penalty/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Penalty penalty = await db.Penalties.FindAsync(id);
            if (penalty == null)
            {
                return HttpNotFound();
            }
            return View(penalty);
        }

        // POST: Penalty/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Penalty penalty = await db.Penalties.FindAsync(id);
            db.Penalties.Remove(penalty);
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
