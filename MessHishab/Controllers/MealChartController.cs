using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MessHishab;
using MessHishab.DAL;
using MessHishab.Models;

namespace MessHishab.Controllers
{
    public class MealChartController : Controller
    {
        private MessDbContext db = new MessDbContext();

        // GET: /MealChart/
        public ActionResult Index()
        {
            var mealcharts = db.MealCharts.Include(m => m.tblMember);
            return View(mealcharts.ToList());
        }

        // GET: /MealChart/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MealChart mealchart = db.MealCharts.Find(id);
            if (mealchart == null)
            {
                return HttpNotFound();
            }
            return View(mealchart);
        }

        // GET: /MealChart/Create
        public ActionResult Create()
        {
            ViewBag.MemberId = new SelectList(db.Members, "Id", "Name");
            return View();
        }

        // POST: /MealChart/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,MemberId,Date,Monning,Middday,Evennig,serialNo")] MealChart mealchart)
        {
            if (ModelState.IsValid)
            {
                mealchart.Id = Guid.NewGuid();
                db.MealCharts.Add(mealchart);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MemberId = new SelectList(db.Members, "Id", "Name", mealchart.MemberId);
            return View(mealchart);
        }

        // GET: /MealChart/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MealChart mealchart = db.MealCharts.Find(id);
            if (mealchart == null)
            {
                return HttpNotFound();
            }
            ViewBag.MemberId = new SelectList(db.Members, "Id", "Name", mealchart.MemberId);
            return View(mealchart);
        }

        // POST: /MealChart/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,MemberId,Date,Monning,Middday,Evennig,serialNo")] MealChart mealchart)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mealchart).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MemberId = new SelectList(db.Members, "Id", "Name", mealchart.MemberId);
            return View(mealchart);
        }

        // GET: /MealChart/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MealChart mealchart = db.MealCharts.Find(id);
            if (mealchart == null)
            {
                return HttpNotFound();
            }
            return View(mealchart);
        }

        // POST: /MealChart/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            MealChart mealchart = db.MealCharts.Find(id);
            db.MealCharts.Remove(mealchart);
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

        public ActionResult LoadMealList()
        {
            return View();
        }
        public JsonResult SaveMealChart(List<MealList> mealList)
        {
            ItemGateway itemGateway = new ItemGateway();
            itemGateway.saveMealList(mealList);
            return Json("success", JsonRequestBehavior.AllowGet);
        }
        public JsonResult getAllMealChatList()
        {
            ItemGateway itemGateway = new ItemGateway();
            var list = itemGateway.getAllMealList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}
