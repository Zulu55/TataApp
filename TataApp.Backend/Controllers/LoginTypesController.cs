using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TataApp.Backend.Models;
using TataApp.Domain;

namespace TataApp.Backend.Controllers
{
    [Authorize]
    public class LoginTypesController : Controller
    {
        private DataContextLocal db = new DataContextLocal();

        // GET: LoginTypes
        public async Task<ActionResult> Index()
        {
            return View(await db.LoginTypes.ToListAsync());
        }

        // GET: LoginTypes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoginType loginType = await db.LoginTypes.FindAsync(id);
            if (loginType == null)
            {
                return HttpNotFound();
            }
            return View(loginType);
        }

        // GET: LoginTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LoginTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "LoginTypeId,Description")] LoginType loginType)
        {
            if (ModelState.IsValid)
            {
                db.LoginTypes.Add(loginType);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(loginType);
        }

        // GET: LoginTypes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoginType loginType = await db.LoginTypes.FindAsync(id);
            if (loginType == null)
            {
                return HttpNotFound();
            }
            return View(loginType);
        }

        // POST: LoginTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "LoginTypeId,Description")] LoginType loginType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(loginType).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(loginType);
        }

        // GET: LoginTypes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoginType loginType = await db.LoginTypes.FindAsync(id);
            if (loginType == null)
            {
                return HttpNotFound();
            }
            return View(loginType);
        }

        // POST: LoginTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            LoginType loginType = await db.LoginTypes.FindAsync(id);
            db.LoginTypes.Remove(loginType);
            await db.SaveChangesAsync();
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
