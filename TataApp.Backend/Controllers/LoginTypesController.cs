using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using TataApp.Backend.Models;
using TataApp.Domain;
using TataApp.Backend.Helpers;

namespace TataApp.Backend.Controllers
{
    [Authorize(Roles = "Admin")]
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

            var loginType = await db.LoginTypes.FindAsync(id);

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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(LoginType loginType)
        {
            if (ModelState.IsValid)
            {
                db.LoginTypes.Add(loginType);
                var response = await DBHelper.SaveChanges(db);
                if (response.Succeeded)
                {
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError(string.Empty, response.Message);
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(LoginType loginType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(loginType).State = EntityState.Modified;
                var response = await DBHelper.SaveChanges(db);
                if (response.Succeeded)
                {
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError(string.Empty, response.Message);
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

            var loginType = await db.LoginTypes.FindAsync(id);

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
            var loginType = await db.LoginTypes.FindAsync(id);
            db.LoginTypes.Remove(loginType);
            var response = await DBHelper.SaveChanges(db);
            if (response.Succeeded)
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError(string.Empty, response.Message);
            return View(loginType);
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
