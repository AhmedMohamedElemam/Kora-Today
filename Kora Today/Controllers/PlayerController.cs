using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kora_Today.Models;
using PagedList;
using PagedList.Mvc;
namespace Kora_Today.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PlayerController : Controller
    {
        private KoraTodayEntities db = new KoraTodayEntities();

        //
        // GET: /Player/

        public ActionResult Index(int? page, string sortby)
        {
            ViewBag.club = string.IsNullOrEmpty(sortby) ? "club desc" : "";
            var players = db.Players.AsQueryable();
            switch (sortby)
            {
                case "club desc":
                    players = players.OrderByDescending(x => x.PlayerClub);
                    break;
                default:
                    players = players.OrderByDescending(x => x.League.LeagueName);
                    break;

            }
            return View(players.ToPagedList(page ?? 1, 5));
        }

        //
        // GET: /Player/Create

        public ActionResult Create()
        {
            ViewBag.LeagueId = new SelectList(db.Leagues, "LeagueId", "LeagueName");
            return View();
        }

        //
        // POST: /Player/Create

        [HttpPost]
        public ActionResult Create(Player player, string PClub)
        {
            if (ModelState.IsValid)
            {
                player.PlayerClub = PClub;
                db.Players.Add(player);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LeagueId = new SelectList(db.Leagues, "LeagueId", "LeagueName", player.LeagueId);
            return View(player);
        }

        public JsonResult GetClub(int LeagueId)
        {
            db.Configuration.ProxyCreationEnabled = false;
            return Json(db.Clubs.Where(c => c.LeagueId == LeagueId), JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /Player/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Player player = db.Players.Find(id);
            if (player == null)
            {
                return HttpNotFound();
            }
            ViewBag.LeagueId = new SelectList(db.Leagues, "LeagueId", "LeagueName", player.LeagueId);
            return View(player);
        }

        //
        // POST: /Player/Edit/5

        [HttpPost]
        public ActionResult Edit(Player player, string PClub)
        {
            if (ModelState.IsValid)
            {
                player.PlayerClub = PClub;
                db.Entry(player).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LeagueId = new SelectList(db.Leagues, "LeagueId", "LeagueName", player.LeagueId);
            return View(player);
        }

        //
        // GET: /Player/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Player player = db.Players.Find(id);
            if (player == null)
            {
                return HttpNotFound();
            }
            return View(player);
        }

        //
        // POST: /Player/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Player player = db.Players.Find(id);
            db.Players.Remove(player);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}