using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Data;
using WebApp.Models;

namespace WebApp.Controllers
{
    [Authorize]
    public class StudentController : Controller
    {
        private AppDb app = new AppDb();

        // GET: Student
        public ActionResult Index()
        {
            try
            {
                IEnumerable<Student> students = app.std;
                return View(students);
            }
            catch(Exception e)
            { 
                Console.WriteLine(e.Message);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Student obj)
        {
            if (obj.Name == obj.Department)
            {
                ModelState.AddModelError("Name", "Email cannot exactly match the Name.");
            }

            if (ModelState.IsValid)
            {
                app.std.Add(obj);
                app.SaveChanges();
                TempData["Add"] = "Student record added successfully!!";
                return RedirectToAction("Index");
            }
           return View(obj);   
        }

        //GET
        [HttpGet]
        public ActionResult Edit(int? rollNo)
        {
            if (rollNo == null || rollNo == 0)
            {
                return RedirectToAction("Index","Home");
            }

            var studentFromDb = app.std.Where(x => x.RollNo == rollNo).FirstOrDefault();

            if (studentFromDb == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(studentFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Student obj)
        {
            var studentFromDb = app.std.Where(x => x.RollNo == obj.RollNo).FirstOrDefault();

            if (obj.Name == obj.Department)
            {
                ModelState.AddModelError("Name", "Email cannot exactly match the Name.");
            }
            if (ModelState.IsValid)
            {
                studentFromDb.RollNo = obj.RollNo;
                studentFromDb.Name = obj.Name;
                studentFromDb.Department = obj.Department;
                app.SaveChanges();
                TempData["Edit"] = "Student record edited successfully!!";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //GET
        [HttpGet]
        public ActionResult Delete(int? rollNo)
        {
            if (rollNo == null || rollNo == 0)
            {
                return RedirectToAction("Index", "Home");
            }

            var data = app.std.Where(x => x.RollNo == rollNo).FirstOrDefault();

            if (data == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(data);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Student obj)
        {
            var stdDb = app.std.Where(x => x.RollNo == obj.RollNo).FirstOrDefault();
            app.std.Remove(stdDb);
            app.SaveChanges();
            ViewBag.Delete = "Student record deleted successfully!!";
            return RedirectToAction("Index");
        }
    }
}