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
using refwebportal.Models;

namespace refwebportal.Controllers
{
    public class ViewPlayersController : Controller
    {
        private FROdataEntities3 db = new FROdataEntities3();

        // GET: ViewPlayers
        public async Task<ActionResult> Index()
        {
            var viewPlayers = db.ViewPlayers.Include(v => v.Game).Include(v => v.Team);
            return View(await viewPlayers.ToListAsync());
        }

        // GET: ViewPlayers/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewPlayers viewPlayers = await db.ViewPlayers.FindAsync(id);
            if (viewPlayers == null)
            {
                return HttpNotFound();
            }
            return View(viewPlayers);
        }

        // GET: ViewPlayers/Create
        public ActionResult Create()
        {
            ViewBag.GameId = new SelectList(db.Games, "Id", "Description");
            ViewBag.TeamId = new SelectList(db.Teams, "Id", "Name");
            return View();
        }

        // POST: ViewPlayers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,FirstName,LastName,SquadNumber,IsCaptain,TeamId,GameId")] ViewPlayers viewPlayers)
        {
            if (ModelState.IsValid)
            {
                db.ViewPlayers.Add(viewPlayers);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.GameId = new SelectList(db.Games, "Id", "Description", viewPlayers.GameId);
            ViewBag.TeamId = new SelectList(db.Teams, "Id", "Name", viewPlayers.TeamId);
            return View(viewPlayers);
        }

        // GET: ViewPlayers/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewPlayers viewPlayers = await db.ViewPlayers.FindAsync(id);
            if (viewPlayers == null)
            {
                return HttpNotFound();
            }
            ViewBag.GameId = new SelectList(db.Games, "Id", "Description", viewPlayers.GameId);
            ViewBag.TeamId = new SelectList(db.Teams, "Id", "Name", viewPlayers.TeamId);
            return View(viewPlayers);
        }

        // POST: ViewPlayers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,FirstName,LastName,SquadNumber,IsCaptain,TeamId,GameId")] ViewPlayers viewPlayers)
        {
            if (ModelState.IsValid)
            {
                db.Entry(viewPlayers).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.GameId = new SelectList(db.Games, "Id", "Description", viewPlayers.GameId);
            ViewBag.TeamId = new SelectList(db.Teams, "Id", "Name", viewPlayers.TeamId);
            return View(viewPlayers);
        }

        // GET: ViewPlayers/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewPlayers viewPlayers = await db.ViewPlayers.FindAsync(id);
            if (viewPlayers == null)
            {
                return HttpNotFound();
            }
            return View(viewPlayers);
        }

        // POST: ViewPlayers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ViewPlayers viewPlayers = await db.ViewPlayers.FindAsync(id);
            db.ViewPlayers.Remove(viewPlayers);
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
