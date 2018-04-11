using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MessHishabUpdate;
using MessHishab.DAL;
using MessHishab.Models;

namespace MessHishab.Controllers
{
    public class DailyBajarListController : Controller
    {
        private MessDbContext db = new MessDbContext();

        // GET: /DailyBajarList/
        public ActionResult Index()
        {
            var dailybazarlists = db.DailyBazarLists.Include(d => d.tblItem).Include(d => d.tblMember).Include(d => d.tblUnit);
            return View(dailybazarlists.ToList());
        }

        // GET: /DailyBajarList/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DailyBazarList dailybazarlist = db.DailyBazarLists.Find(id);
            if (dailybazarlist == null)
            {
                return HttpNotFound();
            }
            return View(dailybazarlist);
        }

        // GET: /DailyBajarList/Create
        public ActionResult Create()
        {
            ViewBag.ItemId = new SelectList(db.Items, "Id", "Name");
            ViewBag.PurchaserId = new SelectList(db.Members, "Id", "Name");
            ViewBag.UnitId = new SelectList(db.Units, "Id", "Name");
            return View();
        }

        // POST: /DailyBajarList/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,Date,UnitId,Quantity,Price,PurchaserId,ItemId")] DailyBazarList dailybazarlist)
        {
            if (ModelState.IsValid)
            {
                dailybazarlist.Id = Guid.NewGuid();
                db.DailyBazarLists.Add(dailybazarlist);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ItemId = new SelectList(db.Items, "Id", "Name", dailybazarlist.ItemId);
            ViewBag.PurchaserId = new SelectList(db.Members, "Id", "Name", dailybazarlist.PurchaserId);
            ViewBag.UnitId = new SelectList(db.Units, "Id", "Name", dailybazarlist.UnitId);
            return View(dailybazarlist);
        }

        // GET: /DailyBajarList/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DailyBazarList dailybazarlist = db.DailyBazarLists.Find(id);
            if (dailybazarlist == null)
            {
                return HttpNotFound();
            }
            ViewBag.ItemId = new SelectList(db.Items, "Id", "Name", dailybazarlist.ItemId);
            ViewBag.PurchaserId = new SelectList(db.Members, "Id", "Name", dailybazarlist.PurchaserId);
            ViewBag.UnitId = new SelectList(db.Units, "Id", "Name", dailybazarlist.UnitId);
            return View(dailybazarlist);
        }

        // POST: /DailyBajarList/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Date,UnitId,Quantity,Price,PurchaserId,ItemId")] DailyBazarList dailybazarlist)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dailybazarlist).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ItemId = new SelectList(db.Items, "Id", "Name", dailybazarlist.ItemId);
            ViewBag.PurchaserId = new SelectList(db.Members, "Id", "Name", dailybazarlist.PurchaserId);
            ViewBag.UnitId = new SelectList(db.Units, "Id", "Name", dailybazarlist.UnitId);
            return View(dailybazarlist);
        }

        // GET: /DailyBajarList/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DailyBazarList dailybazarlist = db.DailyBazarLists.Find(id);
            if (dailybazarlist == null)
            {
                return HttpNotFound();
            }
            return View(dailybazarlist);
        }

        // POST: /DailyBajarList/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            DailyBazarList dailybazarlist = db.DailyBazarLists.Find(id);
            db.DailyBazarLists.Remove(dailybazarlist);
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
        public JsonResult BajarListView()
        {
            ItemListView itemlistview = new ItemListView();
            List<CategoryInfo> categoris = new List<CategoryInfo>();
            ItemGateway itemGateway = new ItemGateway();
            var categoryList = itemGateway.GetCategory();
            foreach (Category1 category in categoryList)
            {
                CategoryInfo categoryInfo = new CategoryInfo();
                categoryInfo.ItemList = itemGateway.getItemList(category.Id);
                categoryInfo.CategoryName = category.Name;
                categoris.Add(categoryInfo);
            }
            itemlistview.CategoryList = categoris;
            return Json(itemlistview, JsonRequestBehavior.AllowGet);
        }
        public JsonResult saveBajarList(List<BajarList> itemlist)
        {

            ItemGateway itemGateway = new ItemGateway();
            itemGateway.saveBajarList(itemlist);
            return Json("Succesfuly Added the List", JsonRequestBehavior.AllowGet);
        }
        public JsonResult getBajarListByMemberId(string date, Guid memberId)
        {

            ItemGateway itemGateway = new ItemGateway();
            var bajarlist = itemGateway.getBajarListBymemberId(date, memberId);
            return Json(bajarlist, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ShowBajarList()
        {
            return View();
        }
    }
}
