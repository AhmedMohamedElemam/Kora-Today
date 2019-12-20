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
    public class SliderController : Controller
    {
        private KoraTodayEntities db = new KoraTodayEntities();

        //
        // GET: /Slider/

        public ActionResult Index()
        {
            return View(db.Sliders.ToList());
        }

        //
        // GET: /Slider/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Slider/Create

        [HttpPost]
        public ActionResult Create(Slider slider, HttpPostedFileBase SliderPhoto)
        {
            if (ModelState.IsValid)
            {
                string path = Path.Combine(Server.MapPath("~/Uploads"), SliderPhoto.FileName);
                SliderPhoto.SaveAs(path);
                slider.SliderImage = SliderPhoto.FileName;
                db.Sliders.Add(slider);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ModelState.Clear();
            return View(slider);
        }

        //
        // GET: /Slider/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Slider slider = db.Sliders.Find(id);
            if (slider == null)
            {
                return HttpNotFound();
            }
            return View(slider);
        }

        //
        // POST: /Slider/Edit/5

        [HttpPost]
        public ActionResult Edit(Slider slider, HttpPostedFileBase SliderPhoto)
        {
            if (ModelState.IsValid)
            {
                string path = Path.Combine(Server.MapPath("~/Uploads"), SliderPhoto.FileName);
                SliderPhoto.SaveAs(path);
                slider.SliderImage = SliderPhoto.FileName;
                db.Entry(slider).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(slider);
        }

        //
        // GET: /Slider/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Slider slider = db.Sliders.Find(id);
            if (slider == null)
            {
                return HttpNotFound();
            }
            return View(slider);
        }

        //
        // POST: /Slider/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Slider slider = db.Sliders.Find(id);
            db.Sliders.Remove(slider);
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