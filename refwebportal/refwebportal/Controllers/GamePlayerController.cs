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
    public class GamePlayerController : Controller
    {
        private FROdataEntities1 db = new FROdataEntities1();

        // GET: GamePlayer
        public async Task<ActionResult> Index()
        {
            var gamePlayers = db.GamePlayers.Include(g => g.Game).Include(g => g.Player);
            return View(await gamePlayers.ToListAsync());
        }

        // GET: GamePlayer/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GamePlayer gamePlayer = await db.GamePlayers.FindAsync(id);
            if (gamePlayer == null)
            {
                return HttpNotFound();
            }
            return View(gamePlayer);
        }

        // GET: GamePlayer/Create
        public ActionResult Create()
        {
            ViewBag.GameId = new SelectList(db.Games, "Id", "Description");
            ViewBag.PlayerId = new SelectList(db.Players, "Id", "FirstName");
            return View();
        }

        // POST: GamePlayer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,PlayerId,GameId,IsCaptain,SquadNumber")] GamePlayer gamePlayer)
        {
            if (ModelState.IsValid)
            {
                db.GamePlayers.Add(gamePlayer);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.GameId = new SelectList(db.Games, "Id", "Description", gamePlayer.GameId);
            ViewBag.PlayerId = new SelectList(db.Players, "Id", "FirstName", gamePlayer.PlayerId);
            return View(gamePlayer);
        }

        // GET: GamePlayer/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GamePlayer gamePlayer = await db.GamePlayers.FindAsync(id);
            if (gamePlayer == null)
            {
                return HttpNotFound();
            }
            ViewBag.GameId = new SelectList(db.Games, "Id", "Description", gamePlayer.GameId);
            ViewBag.PlayerId = new SelectList(db.Players, "Id", "FirstName", gamePlayer.PlayerId);
            return View(gamePlayer);
        }

        // POST: GamePlayer/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,PlayerId,GameId,IsCaptain,SquadNumber")] GamePlayer gamePlayer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gamePlayer).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.GameId = new SelectList(db.Games, "Id", "Description", gamePlayer.GameId);
            ViewBag.PlayerId = new SelectList(db.Players, "Id", "FirstName", gamePlayer.PlayerId);
            return View(gamePlayer);
        }

        // GET: GamePlayer/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GamePlayer gamePlayer = await db.GamePlayers.FindAsync(id);
            if (gamePlayer == null)
            {
                return HttpNotFound();
            }
            return View(gamePlayer);
        }

        // POST: GamePlayer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            GamePlayer gamePlayer = await db.GamePlayers.FindAsync(id);
            db.GamePlayers.Remove(gamePlayer);
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
