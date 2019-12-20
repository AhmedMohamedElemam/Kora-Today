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
    public class MatchDetailController : Controller
    {
        private KoraTodayEntities db = new KoraTodayEntities();

        //
        // GET: /MatchDetail/

        public ActionResult Index()
        {
            var matchdetails = db.MatchDetails.Include(m => m.Match);
            return View(matchdetails.ToList());
        }

        //
        // GET: /MatchDetail/Create

        public ActionResult Create()
        {
            ViewBag.MatchId = db.Matches.OrderByDescending(m => m.MatchDate).ToList().Select(p => new SelectListItem
            {
                Value = p.MatchId.ToString(),
                Text = string.Format("{1} VS {2} = {3}", p.MatchId, p.MatchHome, p.MatchAway, p.MatchDate)
            });
            ViewBag.MatchDetailsIcon = new SelectList(new[] { "1st Half.png", "2nd Half.png", "Yellow Card.png", "Red Card.png", "2nd Yellow Card.png", "Substitution.png", "End 1st Half.png", "End 2nd Half.png", "Goal.png", "Fault.png", "Injury.png", "Missed penalty.png", "Offside.png", "Extra Time.png" });
            return View();
        }

        //
        // POST: /MatchDetail/Create

        [HttpPost]
        public ActionResult Create(MatchDetail matchdetail)
        {
            if (ModelState.IsValid)
            {
                matchdetail.MatchDetailsId = Guid.NewGuid();
                db.MatchDetails.Add(matchdetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MatchId = db.Matches.OrderByDescending(m => m.MatchDate).ToList().Select(p => new SelectListItem
            {
                Value = p.MatchId.ToString(),
                Text = string.Format("{1} VS {2} = {3}", p.MatchId, p.MatchHome, p.MatchAway, p.MatchDate)
            });
            ViewBag.MatchDetailsIcon = new SelectList(new[] { "1st Half.png", "2nd Half.png", "Yellow Card.png", "Red Card.png", "2nd Yellow Card.png", "Substitution.png", "End 1st Half.png", "End 2nd Half.png", "Goal.png", "Fault.png", "Injury.png", "Missed penalty.png", "Offside.png", "Extra Time.png" });
            return View(matchdetail);
        }

        //
        // GET: /MatchDetail/Edit/5

        public ActionResult Edit(Guid id)
        {
            MatchDetail matchdetail = db.MatchDetails.Find(id);
            if (matchdetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.MatchId = db.Matches.OrderByDescending(m => m.MatchDate).ToList().Select(p => new SelectListItem
            {
                Value = p.MatchId.ToString(),
                Text = string.Format("{1} VS {2} = {3}", p.MatchId, p.MatchHome, p.MatchAway, p.MatchDate)
            });
            ViewBag.MatchDetailsIcon = new SelectList(new[] { "1st Half.png", "2nd Half.png", "Yellow Card.png", "Red Card.png", "2nd Yellow Card.png", "Substitution.png", "End 1st Half.png", "End 2nd Half.png", "Goal.png", "Fault.png", "Injury.png", "Missed penalty.png", "Offside.png", "Extra Time.png" });
            return View(matchdetail);
        }

        //
        // POST: /MatchDetail/Edit/5

        [HttpPost]
        public ActionResult Edit(MatchDetail matchdetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(matchdetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MatchId = db.Matches.OrderByDescending(m => m.MatchDate).ToList().Select(p => new SelectListItem
            {
                Value = p.MatchId.ToString(),
                Text = string.Format("{1} VS {2} = {3}", p.MatchId, p.MatchHome, p.MatchAway, p.MatchDate)
            });
            ViewBag.MatchDetailsIcon = new SelectList(new[] { "1st Half.png", "2nd Half.png", "Yellow Card.png", "Red Card.png", "2nd Yellow Card.png", "Substitution.png", "End 1st Half.png", "End 2nd Half.png", "Goal.png", "Fault.png", "Injury.png", "Missed penalty.png", "Offside.png", "Extra Time.png" });
            return View(matchdetail);
        }

        //
        // GET: /MatchDetail/Delete/5

        public ActionResult Delete(Guid id)
        {
            MatchDetail matchdetail = db.MatchDetails.Find(id);
            if (matchdetail == null)
            {
                return HttpNotFound();
            }
            return View(matchdetail);
        }

        //
        // POST: /MatchDetail/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            MatchDetail matchdetail = db.MatchDetails.Find(id);
            db.MatchDetails.Remove(matchdetail);
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