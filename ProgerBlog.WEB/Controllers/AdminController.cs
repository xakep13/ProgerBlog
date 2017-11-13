using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProgerBlog.WEB.Controllers
{
    public class AdminController : Controller
    {

        EventRepository repo;

        public AdminController()
        {
            repo = new EventRepository();
        }


        // GET: Admin
        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            var _events = repo.GetEventList();
            return View(_events);
        }


        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Event _event)
        {
            repo.Create(_event);
            repo.Save();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            int b = (int)id;
            Event _event = repo.GetEvent(b);
            if (_event != null)
            {
                return View(_event);
            }
            return HttpNotFound();
        }

        // POST: Admin/Edit/5
        [HttpPost]
        public ActionResult Edit(Event _event)
        {
            repo.db.Entry(_event).State = EntityState.Modified;
            repo.Save();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            Event b = repo.GetEvent(id);
            if (b != null)
            {
                repo.Delete(b.Id);
                repo.Save();
            }
            return RedirectToAction("Index");
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult Delete(int id, FormCollection collection)
        {
            Event b = repo.GetEvent(id);
            if (b == null)
            {
                return HttpNotFound();
            }
            repo.Delete(b.Id);
            repo.Save();
            return RedirectToAction("Index");
        }
    }
}