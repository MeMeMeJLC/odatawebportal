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
    public class GameTeamController : Controller
    {
        private FROdataEntities3 db = new FROdataEntities3();

        // GET: GameTeam
        public async Task<ActionResult> Index()
        {
            var gameTeams = db.GameTeams.Include(g => g.Game).Include(g => g.Team);
            return View(await gameTeams.ToListAsync());
        }

        // GET: GameTeam/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GameTeam gameTeam = await db.GameTeams.FindAsync(id);
            if (gameTeam == null)
            {
                return HttpNotFound();
            }
            return View(gameTeam);
        }

        // GET: GameTeam/Create
        public ActionResult Create()
        {
            ViewBag.GameId = new SelectList(db.Games, "Id", "Description");
            ViewBag.TeamId = new SelectList(db.Teams, "Id", "Name");
            return View();
        }

        // POST: GameTeam/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,GameId,TeamId")] GameTeam gameTeam)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.GameTeams.Add(gameTeam);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }

            ViewBag.GameId = new SelectList(db.Games, "Id", "Description", gameTeam.GameId);
            ViewBag.TeamId = new SelectList(db.Teams, "Id", "Name", gameTeam.TeamId);
            return View(gameTeam);
        }

        // GET: GameTeam/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GameTeam gameTeam = await db.GameTeams.FindAsync(id);
            if (gameTeam == null)
            {
                return HttpNotFound();
            }
            ViewBag.GameId = new SelectList(db.Games, "Id", "Description", gameTeam.GameId);
            ViewBag.TeamId = new SelectList(db.Teams, "Id", "Name", gameTeam.TeamId);
            return View(gameTeam);
        }

        // POST: GameTeam/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Game.Id,Team.Id")] GameTeam gameTeam)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gameTeam).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.GameId = new SelectList(db.Games, "Id", "Description", gameTeam.GameId);
            ViewBag.TeamId = new SelectList(db.Teams, "Id", "Name", gameTeam.TeamId);
            return View(gameTeam);
        }

        // GET: GameTeam/Delete/5
        public async Task<ActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists see your system administrator.";
            }
            GameTeam gameTeam = await db.GameTeams.FindAsync(id);
            if (gameTeam == null)
            {
                return HttpNotFound();
            }
            return View(gameTeam);
        }

        // POST: GameTeam/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                GameTeam gameTeam = await db.GameTeams.FindAsync(id);
                db.GameTeams.Remove(gameTeam);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch (DataException/* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
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
