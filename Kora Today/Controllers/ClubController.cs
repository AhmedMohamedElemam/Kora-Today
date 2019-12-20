using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kora_Today.Models;
using System.IO;
using PagedList;
using PagedList.Mvc;

namespace Kora_Today.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ClubController : Controller
    {
        private KoraTodayEntities db = new KoraTodayEntities();

        //
        // GET: /Club/

        public ActionResult Index(int? page, string sortby)
        {
            ViewBag.league = string.IsNullOrEmpty(sortby) ? "league desc" : "";
            var clubs = db.Clubs.AsQueryable();
            switch (sortby)
            {
                case "league desc":
                    clubs = clubs.OrderByDescending(x => x.League.LeagueName);
                    break;
                default:
                    clubs = clubs.OrderBy(x => x.League.LeagueName);
                    break;

            }
            return View(clubs.ToPagedList(page ?? 1, 6));
        }

        //
        // GET: /Club/Create

        public ActionResult Create()
        {
            ViewBag.LeagueId = new SelectList(db.Leagues, "LeagueId", "LeagueName");
            return View();
        }

        //
        // POST: /Club/Create

        [HttpPost]
        public ActionResult Create(Club club, HttpPostedFileBase ClubPhoto)
        {
            if (ModelState.IsValid)
            {
                string path = Path.Combine(Server.MapPath("~/Uploads"), ClubPhoto.FileName);
                ClubPhoto.SaveAs(path);
                club.ClubImage = ClubPhoto.FileName;
                db.Clubs.Add(club);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ModelState.Clear();
            ViewBag.LeagueId = new SelectList(db.Leagues, "LeagueId", "LeagueName", club.LeagueId);
            return View(club);
        }

        //
        // GET: /Club/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Club club = db.Clubs.Find(id);
            if (club == null)
            {
                return HttpNotFound();
            }
            ViewBag.LeagueId = new SelectList(db.Leagues, "LeagueId", "LeagueName", club.LeagueId);
            return View(club);
        }

        //
        // POST: /Club/Edit/5

        [HttpPost]
        public ActionResult Edit(Club club)
        {
            if (ModelState.IsValid)
            {
                db.Entry(club).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LeagueId = new SelectList(db.Leagues, "LeagueId", "LeagueName", club.LeagueId);
            return View(club);
        }

        //
        // GET: /Club/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Club club = db.Clubs.Find(id);
            if (club == null)
            {
                return HttpNotFound();
            }
            return View(club);
        }

        //
        // POST: /Club/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Club club = db.Clubs.Find(id);
            db.Clubs.Remove(club);
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