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
using Newtonsoft.Json;
using System.Web.Helpers;
using refwebportal.Models;

namespace refwebportal.Controllers
{
    public class GamePlayerController : Controller
    {
        private FROdataEntities3 db = new FROdataEntities3();

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

        public ActionResult GetGames()
        {
            var games = from b in db.Games
                        select new ViewGame()
                        {
                            Id = b.Id,
                            Description = b.Description,
                            GameDateTime = b.GameDateTime
                        };
            return Json(games, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetTeams(int? intGameID)
        {
            var teams = from b in db.GameTeams.Where(p => p.GameId == intGameID )
                        select new ViewTeam()
                        {
                            TeamName = b.Team.Name,
                            Id = b.Id
                        };
            return Json(teams, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetPlayers(int? intTeamID)
        {
            var players = from b in db.Players.Where(p => p.TeamId == intTeamID)
                          select new ViewPlayers()
                          {
                              Id = b.Id,
                              FirstName = b.FirstName,
                              LastName = b.LastName,
                              TeamId = b.TeamId,                   
                          };
            //var players = db.Players.Where(p => p.TeamId == intTeamID);
            return Json(players, JsonRequestBehavior.AllowGet);
        }

        // GET: GamePlayer/Create
        public ActionResult Create()
        {
            var intTeamId = 1;
            ViewBag.GameId = new SelectList(db.Games, "Id", "Description");
            ViewBag.GameTeamId = new SelectList(db.GameTeams.Where(p => p.GameId == intTeamId), "Id", "Team.Name");
            ViewBag.PlayerId = new SelectList(db.Players.Where(p => p.TeamId == intTeamId), "Id", "FullName");
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
           // ViewBag.GameTeamId = new SelectList(db.GameTeams, "Id", "Team.Name", gamePlayer.GameTeamId);
            ViewBag.PlayerId = new SelectList(db.Players, "Id", "FullName", gamePlayer.PlayerId);
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
