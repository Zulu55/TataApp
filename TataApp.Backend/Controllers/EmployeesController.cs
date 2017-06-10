using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using TataApp.Backend.Helpers;
using TataApp.Backend.Models;
using TataApp.Domain;

namespace TataApp.Backend.Controllers
{
    [Authorize(Roles = "Admin")]
    public class EmployeesController : Controller
    {
        private DataContextLocal db = new DataContextLocal();

        // GET: Employees
        public async Task<ActionResult> Index()
        {
            var employees = db.Employees.Include(e => e.DocumentType).Include(e => e.LoginType);
            return View(await employees.ToListAsync());
        }

        // GET: Employees/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var employee = await db.Employees.FindAsync(id);

            if (employee == null)
            {
                return HttpNotFound();
            }

            return View(employee);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            ViewBag.DocumentTypeId = new SelectList(db.DocumentTypes, "DocumentTypeId", "Description");
            ViewBag.LoginTypeId = new SelectList(db.LoginTypes, "LoginTypeId", "Description");
            return View();
        }

        // POST: Employees/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(EmployeeView view)
        {
            if (ModelState.IsValid)
            {
                var pic = string.Empty;
                var folder = "~/Content/Employees";

                if (view.PictureFile != null)
                {
                    pic = FilesHelper.UploadPhoto(view.PictureFile, folder);
                    pic = string.Format("{0}/{1}", folder, pic);
                }

                var employee = ToEmployee(view);
                employee.Picture = pic;
                db.Employees.Add(employee);
                var response = await DBHelper.SaveChanges(db);
                if (response.Succeeded)
                {
                    UsersHelper.CreateUserASP(view.Email, "Employee", view.Password);
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError(string.Empty, response.Message);
            }

            ViewBag.DocumentTypeId = new SelectList(db.DocumentTypes, "DocumentTypeId", "Description", view.DocumentTypeId);
            ViewBag.LoginTypeId = new SelectList(db.LoginTypes, "LoginTypeId", "Description", view.LoginTypeId);
            return View(view);
        }

        private Employee ToEmployee(EmployeeView view)
        {
            return new Employee
            {
                Address = view.Address,
                Document = view.Document,
                DocumentType = view.DocumentType,
                DocumentTypeId = view.DocumentTypeId,
                Email = view.Email,
                EmployeeCode = view.EmployeeCode,
                EmployeeId = view.EmployeeId,
                FirstName = view.FirstName,
                LastName = view.LastName,
                LoginType = view.LoginType,
                LoginTypeId = view.LoginTypeId,
                Phone = view.Phone,
                Picture = view.Picture,
            };
        }

        // GET: Employees/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var employee = await db.Employees.FindAsync(id);

            if (employee == null)
            {
                return HttpNotFound();
            }

            ViewBag.DocumentTypeId = new SelectList(db.DocumentTypes, "DocumentTypeId", "Description", employee.DocumentTypeId);
            ViewBag.LoginTypeId = new SelectList(db.LoginTypes, "LoginTypeId", "Description", employee.LoginTypeId);
            var view = ToView(employee);
            return View(view);
        }

        private EmployeeView ToView(Employee employee)
        {
            return new EmployeeView
            {
                Address = employee.Address,
                Document = employee.Document,
                DocumentType = employee.DocumentType,
                DocumentTypeId = employee.DocumentTypeId,
                Email = employee.Email,
                EmployeeCode = employee.EmployeeCode,
                EmployeeId = employee.EmployeeId,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                LoginType = employee.LoginType,
                LoginTypeId = employee.LoginTypeId,
                Phone = employee.Phone,
                Picture = employee.Picture,
            };
        }

        // POST: Employees/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EmployeeView view)
        {
            if (ModelState.IsValid)
            {
                var pic = view.Picture;
                var folder = "~/Content/Employees";

                if (view.PictureFile != null)
                {
                    pic = FilesHelper.UploadPhoto(view.PictureFile, folder);
                    pic = string.Format("{0}/{1}", folder, pic);
                }

                var employee = ToEmployee(view);
                employee.Picture = pic;
                db.Entry(employee).State = EntityState.Modified;
                var response = await DBHelper.SaveChanges(db);
                if (response.Succeeded)
                {
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError(string.Empty, response.Message);
            }
            ViewBag.DocumentTypeId = new SelectList(db.DocumentTypes, "DocumentTypeId", "Description", view.DocumentTypeId);
            ViewBag.LoginTypeId = new SelectList(db.LoginTypes, "LoginTypeId", "Description", view.LoginTypeId);
            return View(view);
        }

        // GET: Employees/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var employee = await db.Employees.FindAsync(id);

            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var employee = await db.Employees.FindAsync(id);
            db.Employees.Remove(employee);
            var response = await DBHelper.SaveChanges(db);
            if (response.Succeeded)
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError(string.Empty, response.Message);
            return View(employee);
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
