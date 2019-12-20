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
    public class HistoryController : Controller
    {
        private KoraTodayEntities db = new KoraTodayEntities();

        //
        // GET: /History/

        public ActionResult Index()
        {
            var histories = db.Histories.Include(h => h.Club);
            return View(histories.ToList());
        }

        //
        // GET: /History/Create

        public ActionResult Create()
        {
            ViewBag.ClubId = new SelectList(db.Clubs, "ClubId", "ClubName");
            return View();
        }

        //
        // POST: /History/Create

        [HttpPost]
        public ActionResult Create(History history, HttpPostedFileBase HistoryPhoto)
        {
            if (ModelState.IsValid)
            {

                string path = Path.Combine(Server.MapPath("~/Uploads"), HistoryPhoto.FileName);
                HistoryPhoto.SaveAs(path);
                history.HistoryImage = HistoryPhoto.FileName;
                history.HistoryTime = DateTime.Now.ToString();
                db.Histories.Add(history);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ModelState.Clear();
            ViewBag.ClubId = new SelectList(db.Clubs, "ClubId", "ClubName", history.ClubId);
            return View(history);
        }

        //
        // GET: /History/Edit/5

        public ActionResult Edit(int id = 0)
        {
            History history = db.Histories.Find(id);
            if (history == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClubId = new SelectList(db.Clubs, "ClubId", "ClubName", history.ClubId);
            return View(history);
        }

        //
        // POST: /History/Edit/5

        [HttpPost]
        public ActionResult Edit(History history, HttpPostedFileBase HistoryPhoto)
        {
            if (ModelState.IsValid)
            {
                string path = Path.Combine(Server.MapPath("~/Uploads"), HistoryPhoto.FileName);
                HistoryPhoto.SaveAs(path);
                history.HistoryImage = HistoryPhoto.FileName;
                db.Entry(history).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClubId = new SelectList(db.Clubs, "ClubId", "ClubName", history.ClubId);
            return View(history);
        }

        //
        // GET: /History/Delete/5

        public ActionResult Delete(int id = 0)
        {
            History history = db.Histories.Find(id);
            if (history == null)
            {
                return HttpNotFound();
            }
            return View(history);
        }

        //
        // POST: /History/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            History history = db.Histories.Find(id);
            db.Histories.Remove(history);
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