using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Mvc;
using CarInsurance.Models;

namespace CarInsurance.Controllers
{
    public class InsureeController : Controller
    {
        //Quote calculation method
        public Insuree CalculateQuote(Insuree insuree)
        {
            //Gets the customers age by subracting today's year by their year of birth and assigns it to int "customerAge"
            DateTime dateTime = DateTime.Now;
            int customerAge = (dateTime.Year - insuree.DateOfBirth.Year);

            //50 is the base amount
            insuree.Quote = 50;

            //If the customer is under 18 add 100
            if (customerAge < 18)
            {
                insuree.Quote += 100;
            }

            //If the customer is 19 to 25 add 50
            else if (customerAge > 18 && customerAge < 26)
            {
                insuree.Quote += 50;

            }

            //If the customer is 26 or older add 25
            else if (customerAge > 25)
            {
                insuree.Quote += 25;
            }

            //If the car's year is before 2000 add 25
            if (insuree.CarYear < 2000)
            {
                insuree.Quote += 25;
            }

            //If the car's year is after 2015 add 25
            else if (insuree.CarYear > 2015)
            {
                insuree.Quote += 25;

            }

            //Reccomendation: should try to convert string data to all lower case to prevent issues
            //If the car make is a Porsche add 25
            if (insuree.CarMake == "Porsche")
            {
                insuree.Quote += 25;

            }

            //If the car make is a Porsche and its model is 911 Carrera add 50
            else if (insuree.CarMake == "Porsche" && insuree.CarModel == "911 Carrera")
            {
                insuree.Quote += 50;
            }

            //Adds 10 for every speeding ticket
            insuree.Quote += insuree.SpeedingTickets * 10;

            //If the customer has ever had a DUI, add 25% to the total
            if (insuree.DUI)
            {
                insuree.Quote += insuree.Quote * (decimal)0.25;
            }

            //If it's full coverage, add 50% to the total
            if (insuree.CoverageType)
            {
                insuree.Quote += insuree.Quote * (decimal)0.50;

            }

            return insuree;
        }

        public ActionResult Admin()
        {
            return View(db.Insurees.ToList());


        }



        private InsuranceEntities db = new InsuranceEntities();

        // GET: Insuree
        public ActionResult Index()
        {
            return View(db.Insurees.ToList());
        }

        // GET: Insuree/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Insuree insuree = db.Insurees.Find(id);
            if (insuree == null)
            {
                return HttpNotFound();
            }
            return View(insuree);
        }

        // GET: Insuree/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: Insuree/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,EmailAddress,DateOfBirth,CarYear,CarMake,CarModel,DUI,SpeedingTickets,CoverageType,Quote")] Insuree insuree)
        {

            if (ModelState.IsValid)
            {
                //Adds quote property to the insuree instance using the "CalculateQuote" method
                insuree = CalculateQuote(insuree);

                db.Insurees.Add(insuree);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(insuree);
        }
        
        // GET: Insuree/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Insuree insuree = db.Insurees.Find(id);
            if (insuree == null)
            {
                return HttpNotFound();
            }
            return View(insuree);
        }

        // POST: Insuree/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,EmailAddress,DateOfBirth,CarYear,CarMake,CarModel,DUI,SpeedingTickets,CoverageType,Quote")] Insuree insuree)
        {
            if (ModelState.IsValid)
            {
                db.Entry(insuree).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(insuree);
        }

        // GET: Insuree/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Insuree insuree = db.Insurees.Find(id);
            if (insuree == null)
            {
                return HttpNotFound();
            }
            return View(insuree);
        }

        // POST: Insuree/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Insuree insuree = db.Insurees.Find(id);
            db.Insurees.Remove(insuree);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
