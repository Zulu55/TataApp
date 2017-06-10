using Newtonsoft.Json.Linq;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using TataApp.Domain;

namespace TataApp.API.Controllers
{
    [Authorize]
    [RoutePrefix("api/Employees")]
    public class EmployeesController : ApiController
    {
        private DataContext db = new DataContext();

        [ResponseType(typeof(Employee))]
        public async Task<IHttpActionResult> GetEmployee(int id)
        {
            Employee employee = await db.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        // GET: api/Employees
        public IQueryable<Employee> GetEmployees()
        {
            return db.Employees;
        }

        // POST: api/GetGetEmployeeByEmailOrCode
        [HttpPost]
        [Route("GetGetEmployeeByEmailOrCode")]
        public async Task<IHttpActionResult> GetGetEmployeeByEmailOrCode(JObject form)
        {
            var emailOrCode = string.Empty;
            dynamic jsonObject = form;

            try
            {
                emailOrCode = jsonObject.EmailOrCode.Value;
            }
            catch
            {
                return BadRequest("Incorrect call");
            }

            Employee employee = null;
            if (emailOrCode.IndexOf('@') != -1)
            {
                employee = await db.Employees.Where(e => e.Email.ToLower() == emailOrCode.ToLower()).FirstOrDefaultAsync();
            }
            else
            {
                int code;
                int.TryParse(emailOrCode, out code);
                employee = await db.Employees.Where(e => e.EmployeeCode == code).FirstOrDefaultAsync();
            }

            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        // PUT: api/Employees/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutEmployee(int id, Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != employee.EmployeeId)
            {
                return BadRequest();
            }

            db.Entry(employee).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Employees
        [ResponseType(typeof(Employee))]
        public async Task<IHttpActionResult> PostEmployee(Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Employees.Add(employee);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = employee.EmployeeId }, employee);
        }

        // DELETE: api/Employees/5
        [ResponseType(typeof(Employee))]
        public async Task<IHttpActionResult> DeleteEmployee(int id)
        {
            Employee employee = await db.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            db.Employees.Remove(employee);
            await db.SaveChangesAsync();

            return Ok(employee);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EmployeeExists(int id)
        {
            return db.Employees.Count(e => e.EmployeeId == id) > 0;
        }
    }
}