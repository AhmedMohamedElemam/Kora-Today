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
    [Authorize(Roles="Admin")]
    public class MatchController : Controller
    {
        private KoraTodayEntities db = new KoraTodayEntities();

        //
        // GET: /Match/

        public ActionResult Index(int? page, string sortby)
        {
            ViewBag.league = string.IsNullOrEmpty(sortby) ? "league desc" : "";
            var matches = db.Matches.AsQueryable();
            switch(sortby)
            {
                case "league desc":
                    matches = matches.OrderByDescending(x => x.MatchLeague);
                    break;
                default:
                    matches = matches.OrderByDescending(x => x.MatchDate);
                    break;

            }
            return View(matches.ToPagedList(page ?? 1, 3));
        }

        //
        // GET: /Match/Create

        public ActionResult Create()
        {
            ViewBag.MatchLeague = new SelectList(db.Leagues, "LeagueName", "LeagueName");
            
            ViewBag.MatchHome = new SelectList(db.Clubs, "ClubName", "ClubName");
            ViewBag.MatchAway = new SelectList(db.Clubs, "ClubName", "ClubName");

            ViewBag.MatchHomeImage = new SelectList(db.Clubs, "ClubImage", "ClubImage");
            ViewBag.MatchAwayImage = new SelectList(db.Clubs, "ClubImage", "ClubImage");

            ViewBag.MatchStatus = new SelectList(new[]{"Live","Finished","Start Soon"});



            return View();
        }

        //
        // POST: /Match/Create

        [HttpPost]
        public ActionResult Create(Match match)
        {
            if (ModelState.IsValid)
            {
                db.Matches.Add(match);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MatchLeague = new SelectList(db.Leagues, "LeagueName", "LeagueName");

            ViewBag.MatchHome = new SelectList(db.Clubs, "ClubName", "ClubName");
            ViewBag.MatchAway = new SelectList(db.Clubs, "ClubName", "ClubName");

            ViewBag.MatchHomeImage = new SelectList(db.Clubs, "ClubImage", "ClubImage");
            ViewBag.MatchAwayImage = new SelectList(db.Clubs, "ClubImage", "ClubImage");

            ViewBag.MatchStatus = new SelectList(new[] { "Live", "Finished", "Start Soon" });


            return View(match);
        }

        //
        // GET: /Match/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Match match = db.Matches.Find(id);
            if (match == null)
            {
                return HttpNotFound();
            }
            return View(match);
        }

        //
        // POST: /Match/Edit/5

        [HttpPost]
        public ActionResult Edit(Match match)
        {
            if (ModelState.IsValid)
            {
                db.Entry(match).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(match);
        }

        //
        // GET: /Match/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Match match = db.Matches.Find(id);
            if (match == null)
            {
                return HttpNotFound();
            }
            return View(match);
        }

        //
        // POST: /Match/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Match match = db.Matches.Find(id);
            db.Matches.Remove(match);
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