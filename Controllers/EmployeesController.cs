using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Data;
using WebApplication2.Models;
using WebApplication2.Models.Domain;
using System;
using System.Threading.Tasks;
namespace WebApplication2.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly mvcDemoDbContext MVCDemoDbContext;

        public EmployeesController(mvcDemoDbContext MVCDemoDbContext)
        {
            this.MVCDemoDbContext = MVCDemoDbContext;
        }

        [HttpGet]

        public async Task<IActionResult> Index()
        {
            var employees = await MVCDemoDbContext.Employees.ToListAsync();
            return View(employees);
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Add(AddEmployeeViewModel addEmployeeRequest)
        {
            
            var employee = new Employee()
            { Id = Guid.NewGuid(), 
              Name = addEmployeeRequest.Name,
              Email = addEmployeeRequest.Email, 
              Department = addEmployeeRequest.Department,
              DateOfBirth = addEmployeeRequest.DateOfBirth,
            };
            await MVCDemoDbContext.Employees.AddAsync(employee);
            await MVCDemoDbContext.SaveChangesAsync();
            return RedirectToAction("Add");


        }
        public async Task<IActionResult> Delete(Guid id)
        {
            var employee = await MVCDemoDbContext.Employees.FindAsync(id);
            if (employee != null)
            {
                MVCDemoDbContext.Employees.Remove(employee);
                await MVCDemoDbContext.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Edit(Guid id)
        {
            var employee = await MVCDemoDbContext.Employees.FindAsync(id);
            if(employee!= null)
            {
                var viewModel = new UpdateEmployeeViewModel()
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Email = employee.Email,
                    Department = employee.Department,
                    DateOfBirth = employee.DateOfBirth,
                };
                return View(viewModel);
               
            }
            return RedirectToAction("Index");
        }
    }
}
