using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kora_Today.Models;
using System.IO;

namespace Kora_Today.Controllers
{
    [Authorize(Roles = "Admin")]
    public class LeagueController : Controller
    {
        private KoraTodayEntities db = new KoraTodayEntities();
        //
        // GET: /League/

        public ActionResult Index()
        {
            return View(db.Leagues.ToList());
        }

        //
        // GET: /League/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /League/Create

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create(League league, HttpPostedFileBase LeaguePhoto)
        {
            if (ModelState.IsValid)
            {
                string path = Path.Combine(Server.MapPath("~/Uploads"), LeaguePhoto.FileName);
                LeaguePhoto.SaveAs(path);
                league.LeagueImage = LeaguePhoto.FileName;
                db.Leagues.Add(league);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ModelState.Clear();
            return View(league);
        }

        //
        // GET: /League/Edit/5

        public ActionResult Edit(int id = 0)
        {
            League league = db.Leagues.Find(id);
            if (league == null)
            {
                return HttpNotFound();
            }
            return View(league);
        }

        //
        // POST: /League/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Edit(League league,HttpPostedFileBase LeaguePhoto)
        {
            if (ModelState.IsValid)
            {
                string path = Path.Combine(Server.MapPath("~/Uploads"), LeaguePhoto.FileName);
                LeaguePhoto.SaveAs(path);
                league.LeagueImage = LeaguePhoto.FileName;
                db.Entry(league).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(league);
        }

        //
        // GET: /League/Delete/5

        public ActionResult Delete(int id = 0)
        {
            League league = db.Leagues.Find(id);
            if (league == null)
            {
                return HttpNotFound();
            }
            return View(league);
        }

        //
        // POST: /League/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]

        public ActionResult DeleteConfirmed(int id)
        {
            League league = db.Leagues.Find(id);
            db.Leagues.Remove(league);
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