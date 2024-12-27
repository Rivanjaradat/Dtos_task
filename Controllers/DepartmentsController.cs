using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using session3.Data.Models;
using session3.Data;
using session3.Migrations;
using session3.DTOs.Department;


namespace session3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public DepartmentsController(ApplicationDbContext context)
        {
            this.context = context;
            
        }
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var departments = context.Departments.Select(
                x=>new GetDepartmentDto()
                {
                    Id = x.Id,
                    Name = x.Name
                }
                );
            return Ok(departments);
        }

        [HttpGet("Details")]
        public IActionResult GetById(int id)
        {
            var department = context.Departments.Where(x => x.Id == id).Select(
              x => new GetDepartmentDto()
              {
                  Id = x.Id,
                  Name = x.Name
              }
              );
          
            if (department == null)
            {
                return NotFound();
            }
            return Ok(department);
        }

        [HttpPost("Create")]
        public IActionResult Create(CreateDepartmentDots dep)
        {
            Department department = new Department
            {
                Name = dep.Name
            };
             context.Departments.Add(department);
             context.SaveChanges();
            return Ok(department);
        }

        [HttpPut("Update")]
        public IActionResult Update(int id, UpdateDepartmentDto dep)
        {
            var department = context.Departments.Find(id);
            if (department == null)
            {
                return NotFound();
            }
            department.Name = dep.Name;
           
            context.Departments.Update(department);
            context.SaveChanges();
            return Ok("Employee updated successfully");

        }

        [HttpDelete("Delete")]
        public IActionResult Delete(int id)
        {
            var department = context.Departments.Find(id);
            if (department == null)
            {
                return NotFound();
            }
            context.Departments.Remove(department);
            context.SaveChanges();

            return Ok($"Delete employee with id {id}");
        }
    }
}
