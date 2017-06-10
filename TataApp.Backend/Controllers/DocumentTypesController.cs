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
    public class DocumentTypesController : Controller
    {
        private DataContextLocal db = new DataContextLocal();

        // GET: DocumentTypes
        public async Task<ActionResult> Index()
        {
            return View(await db.DocumentTypes.ToListAsync());
        }

        // GET: DocumentTypes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var documentType = await db.DocumentTypes.FindAsync(id);

            if (documentType == null)
            {
                return HttpNotFound();
            }

            return View(documentType);
        }

        // GET: DocumentTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DocumentTypes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(DocumentType documentType)
        {
            if (ModelState.IsValid)
            {
                db.DocumentTypes.Add(documentType);
                var response = await DBHelper.SaveChanges(db);
                if (response.Succeeded)
                {
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError(string.Empty, response.Message);
            }

            return View(documentType);
        }

        // GET: DocumentTypes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DocumentType documentType = await db.DocumentTypes.FindAsync(id);
            if (documentType == null)
            {
                return HttpNotFound();
            }
            return View(documentType);
        }

        // POST: DocumentTypes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(DocumentType documentType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(documentType).State = EntityState.Modified;
                var response = await DBHelper.SaveChanges(db);
                if (response.Succeeded)
                {
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError(string.Empty, response.Message);
            }
            return View(documentType);
        }

        // GET: DocumentTypes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var documentType = await db.DocumentTypes.FindAsync(id);

            if (documentType == null)
            {
                return HttpNotFound();
            }

            return View(documentType);
        }

        // POST: DocumentTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var documentType = await db.DocumentTypes.FindAsync(id);
            db.DocumentTypes.Remove(documentType);
            var response = await DBHelper.SaveChanges(db);
            if (response.Succeeded)
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError(string.Empty, response.Message);
            return View(documentType);
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
