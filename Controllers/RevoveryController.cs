using Employee_Department.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Employee_Department.Controllers
{
    public class RevoveryController : Controller
    {
        private readonly ApplicationContext _context;
        public RevoveryController(ApplicationContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var deleteRecords = _context.Employees.Where(e => e.IsDeleted == false).ToList();
            return View(deleteRecords);
        }
        [HttpPost]
        public async Task<IActionResult> Recover(int id)
        {
        var deletedRecord=_context.Employees.FirstOrDefault(e=>e.EmployeeId == id); 
            deletedRecord.IsDeleted= true;
            _context.Employees.Update(deletedRecord);   
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
