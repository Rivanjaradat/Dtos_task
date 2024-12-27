using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using session3.Data;
using session3.Data.Models;
using session3.DTOs.Employees;

namespace session3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public EmployeesController(ApplicationDbContext context)
        {
          
            this.context = context;
        }
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
           var employees= context.Employees.ToList();
            var response= employees.Adapt<IEnumerable<GetEmployeesDto>>();
            return Ok(response);
        }

        [HttpGet("Details")]
        public IActionResult GetById(int id)
        {

           var employee = context.Employees.Find(id);
           
            if (employee == null)
            {
                return NotFound();
            }
            var response = employee.Adapt<GetEmployeesDto>();
            return Ok(response);
        }

        [HttpPost("Create")]
        public IActionResult Create(CreateEmployeeDto empDto)
        {
            var employee=empDto.Adapt<Employee>();
            context.Employees.Add(employee);
            context.SaveChanges();
            return Ok("Employee added successfully");
        }

        [HttpPut("Update")]
        public IActionResult Update(int id,UpdateEmployeeDto emp)
        {
            var employee = context.Employees.Find(id);
            if (employee == null)
            {
                return NotFound();
            }
            employee.Name = emp.Name;
            employee.Description = emp.Description;
             var response = employee.Adapt<Employee>();

            context.Employees.Update(employee);
            context.SaveChanges();
            return Ok("Employee updated successfully"); 

        }

        [HttpDelete("Delete")]
        public IActionResult Delete(int id)
        {
            var employee = context.Employees.Find(id);
            if (employee == null)
            {
                return NotFound();
            }
            context.Employees.Remove(employee);
            context.SaveChanges();

            return Ok($"Delete employee with id {id}");
        }           
    }
}
