using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kora_Today.Models;
using System.IO;
using System.Dynamic;

namespace Kora_Today.Controllers
{
    public class HomeController : Controller
    {
//-------------------------------------------------------------------------------------
        private KoraTodayEntities db = new KoraTodayEntities();

        public ActionResult LeagueHome()
        {
            return View(db.Leagues.ToList());
        }

        public ActionResult LeagueDetails(int id = 0)
        {
            League league = db.Leagues.Find(id);
            if (league == null)
            {
                return HttpNotFound();
            }
            return View(league);
        }
        public PartialViewResult Rank(int id = 0)
        {
            System.Threading.Thread.Sleep(2000);
            League league = db.Leagues.Find(id);
            var clubs = (from c in db.Clubs
                         where c.LeagueId == league.LeagueId
                        orderby c.Points descending
                        select c).ToList();
            return PartialView("_Rankpartial", clubs);
        }
        public PartialViewResult TopSoccer(int id = 0)
        {
            System.Threading.Thread.Sleep(2000);
            League league = db.Leagues.Find(id);
            var players = (from p in db.Players
                         where p.LeagueId == league.LeagueId
                         orderby p.Goals descending
                         select p).ToList();
            return PartialView("_TopSoccerpartial", players);
        }
        public PartialViewResult Fixtures(int id = 0)
        {
            System.Threading.Thread.Sleep(2000);
            League league = db.Leagues.Find(id);
            var players = (from m in db.Matches
                           where m.MatchLeague == league.LeagueName
                           orderby m.MatchDate descending
                           select m).ToList();
            return PartialView("_Fixturespartial", players);
        }

        public ActionResult LeagueClubs(int id = 0)
        {
            League league = db.Leagues.Find(id);
            if (league == null)
            {
                return HttpNotFound();
            }
            return View(league);
        }

        public ActionResult ClubHistory(int id = 0)
        {
            Club club = db.Clubs.Find(id);
            if (club == null)
            {
                return HttpNotFound();
            }
            return View(club);
        }
        public ActionResult FixturesCalendar(string search)
        {
            List<Match> matches = (from m in db.Matches
                                    where m.MatchDate == search
                                    select m).ToList();
            if (Request.IsAjaxRequest())
            {
                return PartialView("_FixturesPartial", matches);
            }
            return View(matches);
        }
        public ActionResult MatchLive(int id = 0)
        {
            Match match = db.Matches.Find(id);
            if (match == null)
            {
                return HttpNotFound();
            }
            return View(match);
        }
        public ActionResult MatchEvents(int id = 0)
        {
            Match match = db.Matches.Find(id);
            if (match == null)
            {
                return HttpNotFound();
            }
            return View(match);
        }


//-------------------------------------------------------------------------------------
        public ActionResult Index()
        {
            ViewBag.NoMatches = ":: No Matches Today ::";
            string date = DateTime.Today.Date.ToString("yyyy-MM-dd");
            List<Match> matches = (from m in db.Matches
                                   where m.MatchDate == date
                                   select m).ToList();
            ViewBag.MatchesCount = matches.Count();

            List<Slider> sliders = db.Sliders.ToList();

            dynamic mymodel = new ExpandoObject();
            mymodel.Matches = matches;
            mymodel.Sliders = sliders;
            return View(mymodel);  
        }

        public ActionResult About()
        {

            return View();
        }
    }
}
