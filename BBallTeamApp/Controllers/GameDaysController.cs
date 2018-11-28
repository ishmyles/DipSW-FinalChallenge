using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BBallTeamApp.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BBallTeamApp.Controllers
{
    [Authorize]
    public class GameDaysController : Controller
    {
        private BballDBEntities db = new BballDBEntities();
        GamePlayerViewModel gamePlayer = new GamePlayerViewModel();

        // GET: GameDays
        public ActionResult Index()
        {
            //var manage = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            //var currentUser = manage.FindById(User.Identity.GetUserId());
            //ViewBag.Authorised = currentUser.Approved;
            //var gameDays = db.GameDays.Include(g => g.Player);
            gamePlayer.Players = db.Players.ToList();
            gamePlayer.GameDays = db.GameDays.Include(g => g.Player).ToList();
            return View(gamePlayer);
        }

        // Get: UpcomingGames
        public async Task<ActionResult> UpcomingGames()
        {
            var gameDates = db.GameDays.Where(g => g.PassedGame == 1);
            return View(await gameDates.ToListAsync());
        }

        // Get: UpcomingGames
        public async Task<ActionResult> GamesHistory()
        {
            var gameDates = db.GameDays.Where(g => g.PassedGame == 0);
            return View(await gameDates.ToListAsync());
        }

        //GET: TeamPayments
        public ActionResult TeamPayments()
        {
            gamePlayer.Players = db.Players.ToList();
            gamePlayer.GameDays = db.GameDays.Where(a => a.PassedGame == 0).Include(c => c.Player).ToList();
            return View(gamePlayer);
        }

        // GET: GameDays/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GameDay gameDay = await db.GameDays.FindAsync(id);
            if (gameDay == null)
            {
                return HttpNotFound();
            }
            return View(gameDay);
        }

        // GET: GameDays/Create
        public ActionResult Create()
        {
            ViewBag.PaidBy = new SelectList(db.Players, "ID", "FirstName");
            return View();
        }

        // POST: GameDays/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "GameNo,Date,Time,Venue,PaidBy,PaidAmount,PassedGame")] GameDay gameDay)
        {
            if (ModelState.IsValid)
            {
                db.GameDays.Add(gameDay);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.PaidBy = new SelectList(db.Players, "ID", "FirstName", gameDay.PaidBy);
            return View(gameDay);
        }

        // GET: GameDays/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GameDay gameDay = await db.GameDays.FindAsync(id);
            if (gameDay == null)
            {
                return HttpNotFound();
            }
            ViewBag.PaidBy = new SelectList(db.Players, "ID", "FirstName", gameDay.PaidBy);
            return View(gameDay);
        }

        // POST: GameDays/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "GameNo,Date,Time,Venue,PaidBy,PaidAmount,PassedGame")] GameDay gameDay)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gameDay).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.PaidBy = new SelectList(db.Players, "ID", "FirstName", gameDay.PaidBy);
            return View(gameDay);
        }

        // GET: GameDays/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GameDay gameDay = await db.GameDays.FindAsync(id);
            if (gameDay == null)
            {
                return HttpNotFound();
            }
            return View(gameDay);
        }

        // POST: GameDays/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            GameDay gameDay = await db.GameDays.FindAsync(id);
            db.GameDays.Remove(gameDay);
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
