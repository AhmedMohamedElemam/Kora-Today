using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kora_Today.Models;

namespace Kora_Today.Controllers
{
    [Authorize(Roles = "Admin")]
    public class MatchTvController : Controller
    {
        private KoraTodayEntities db = new KoraTodayEntities();

        //
        // GET: /MatchTv/

        public ActionResult Index()
        {
            var matchtvs = db.MatchTvs.Include(m => m.Match);
            return View(matchtvs.ToList());
        }

      
        //
        // GET: /MatchTv/Create

        public ActionResult Create()
        {
            ViewBag.MatchId = db.Matches.OrderByDescending(m => m.MatchDate).ToList().Select(p => new SelectListItem
            {
                Value = p.MatchId.ToString(),
                Text = string.Format("{1} VS {2} = {3}", p.MatchId, p.MatchHome, p.MatchAway, p.MatchDate)
            });
                      return View();
        }

        //
        // POST: /MatchTv/Create

        [HttpPost]
        public ActionResult Create(MatchTv matchtv)
        {
            if (ModelState.IsValid)
            {
                matchtv.MatchTvId = Guid.NewGuid();
                db.MatchTvs.Add(matchtv);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MatchId = db.Matches.OrderByDescending(m => m.MatchDate).ToList().Select(p => new SelectListItem
            {
                Value = p.MatchId.ToString(),
                Text = string.Format("{1} VS {2} = {3}", p.MatchId, p.MatchHome, p.MatchAway, p.MatchDate)
            });
            return View(matchtv);
        }

        //
        // GET: /MatchTv/Edit/5
        /* Guid id = default(Guid) */
        public ActionResult Edit(Guid id)
        {
            MatchTv matchtv = db.MatchTvs.Find(id);
            if (matchtv == null)
            {
                return HttpNotFound();
            }
            ViewBag.MatchId = db.Matches.OrderByDescending(m => m.MatchDate).ToList().Select(p => new SelectListItem
            {
                Value = p.MatchId.ToString(),
                Text = string.Format("{1} VS {2} = {3}", p.MatchId, p.MatchHome, p.MatchAway, p.MatchDate)
            });
            return View(matchtv);
        }

        //
        // POST: /MatchTv/Edit/5

        [HttpPost]
        public ActionResult Edit(MatchTv matchtv)
        {
            if (ModelState.IsValid)
            {
                db.Entry(matchtv).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MatchId = db.Matches.OrderByDescending(m => m.MatchDate).ToList().Select(p => new SelectListItem
            {
                Value = p.MatchId.ToString(),
                Text = string.Format("{1} VS {2} = {3}", p.MatchId, p.MatchHome, p.MatchAway, p.MatchDate)
            });
            return View(matchtv);
        }

        //
        // GET: /MatchTv/Delete/5

        public ActionResult Delete(Guid id)
        {
            MatchTv matchtv = db.MatchTvs.Find(id);
            if (matchtv == null)
            {
                return HttpNotFound();
            }
            return View(matchtv);
        }

        //
        // POST: /MatchTv/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            MatchTv matchtv = db.MatchTvs.Find(id);
            db.MatchTvs.Remove(matchtv);
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