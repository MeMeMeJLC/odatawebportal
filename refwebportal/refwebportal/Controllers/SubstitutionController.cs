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
    public class SubstitutionController : Controller
    {
        private FROdataEntities1 db = new FROdataEntities1();

        // GET: Substitution
        public async Task<ActionResult> Index()
        {
            var substitutions = db.Substitutions.Include(s => s.GamePlayer).Include(s => s.GamePlayer1).Include(s => s.GamePlayer2);
            return View(await substitutions.ToListAsync());
        }

        // GET: Substitution/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Substitution substitution = await db.Substitutions.FindAsync(id);
            if (substitution == null)
            {
                return HttpNotFound();
            }
            return View(substitution);
        }

        // GET: Substitution/Create
        public ActionResult Create()
        {
            ViewBag.GamePlayer_Id = new SelectList(db.GamePlayers, "Id", "Id");
            ViewBag.GamePlayerGoingOffTheFieldId = new SelectList(db.GamePlayers, "Id", "Id");
            ViewBag.GamePlayerGoingOnTheFieldId = new SelectList(db.GamePlayers, "Id", "Id");
            return View();
        }

        // POST: Substitution/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,GamePlayerGoingOffTheFieldId,GamePlayerGoingOnTheFieldId,SubstitutionTime,GamePlayer_Id")] Substitution substitution)
        {
            if (ModelState.IsValid)
            {
                db.Substitutions.Add(substitution);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.GamePlayer_Id = new SelectList(db.GamePlayers, "Id", "Id", substitution.GamePlayer_Id);
            ViewBag.GamePlayerGoingOffTheFieldId = new SelectList(db.GamePlayers, "Id", "Id", substitution.GamePlayerGoingOffTheFieldId);
            ViewBag.GamePlayerGoingOnTheFieldId = new SelectList(db.GamePlayers, "Id", "Id", substitution.GamePlayerGoingOnTheFieldId);
            return View(substitution);
        }

        // GET: Substitution/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Substitution substitution = await db.Substitutions.FindAsync(id);
            if (substitution == null)
            {
                return HttpNotFound();
            }
            ViewBag.GamePlayer_Id = new SelectList(db.GamePlayers, "Id", "Id", substitution.GamePlayer_Id);
            ViewBag.GamePlayerGoingOffTheFieldId = new SelectList(db.GamePlayers, "Id", "Id", substitution.GamePlayerGoingOffTheFieldId);
            ViewBag.GamePlayerGoingOnTheFieldId = new SelectList(db.GamePlayers, "Id", "Id", substitution.GamePlayerGoingOnTheFieldId);
            return View(substitution);
        }

        // POST: Substitution/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,GamePlayerGoingOffTheFieldId,GamePlayerGoingOnTheFieldId,SubstitutionTime,GamePlayer_Id")] Substitution substitution)
        {
            if (ModelState.IsValid)
            {
                db.Entry(substitution).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.GamePlayer_Id = new SelectList(db.GamePlayers, "Id", "Id", substitution.GamePlayer_Id);
            ViewBag.GamePlayerGoingOffTheFieldId = new SelectList(db.GamePlayers, "Id", "Id", substitution.GamePlayerGoingOffTheFieldId);
            ViewBag.GamePlayerGoingOnTheFieldId = new SelectList(db.GamePlayers, "Id", "Id", substitution.GamePlayerGoingOnTheFieldId);
            return View(substitution);
        }

        // GET: Substitution/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Substitution substitution = await db.Substitutions.FindAsync(id);
            if (substitution == null)
            {
                return HttpNotFound();
            }
            return View(substitution);
        }

        // POST: Substitution/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Substitution substitution = await db.Substitutions.FindAsync(id);
            db.Substitutions.Remove(substitution);
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
