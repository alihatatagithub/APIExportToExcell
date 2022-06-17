using APIExportToExcell.Data;
using APIExportToExcell.DTOS;
using APIExportToExcell.Entities;
using APIExportToExcell.Repository.IRepository;
using AutoMapper;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.IO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APIExportToExcell.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployee _employeeRepository;
        private readonly AppDbContext _appDbContext;
        private IMapper _mapper;
        public EmployeesController(AppDbContext appDbContext
            ,IEmployee employeeRepository,IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _appDbContext = appDbContext;
            _mapper = mapper;
        }
        // GET: api/<EmployeesController>
        [HttpGet]
        public ActionResult<IReadOnlyList<Employee>> GetEmployees()
        {
            try
            {
                return Ok(_employeeRepository.GetEmployeesAsync());
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            try
            {
                var emp = await _employeeRepository.GetEmployeeByIdAsync(id);
                if (emp == null)
                {
                    return NotFound();

                }
                return emp;
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }
        [HttpPost]
        public async Task<ActionResult<Employee>> CreateEmployee(Employee emp)
        {
            try
            {
                if (emp == null )
                {
                    return BadRequest();

                }


                var Created = await _employeeRepository.AddEmployee(emp);


                return CreatedAtAction(nameof(GetEmployee), new { id = Created.Id }, Created);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error From DB");
            }
        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult<Employee>> UpdateEmployee(int id, Employee employee)
        {
            try
            {
                if (id != employee.Id)
                {
                    return BadRequest("Employee Id Mismatch");

                }
                var EmployeeToUpdate = await _employeeRepository.GetEmployeeByIdAsync(id);
                if (EmployeeToUpdate == null)
                {
                    return NotFound($"Not Found Employee with id={id}");

                }
                return await _employeeRepository.UpdateEmployee(employee);
            }
            catch (Exception)
            {

                throw;
            }


        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Employee>> DeleteEmployee(int id)
        {
            try
            {
                var EmployeeToDelete = await _employeeRepository.GetEmployeeByIdAsync(id);
                if (EmployeeToDelete == null)
                {
                    return NotFound($"Not Found Employee with id={id}");

                }
                return await _employeeRepository.DeleteEmployee(id);
            }
            catch (Exception)
            {

                throw;
            }
        }


        // PUT api/<EmployeesController>/5
        [HttpGet("Export")]
        public ActionResult GetExcell()
        {
            DataTable dt = new DataTable("Grid");
            var employees = _appDbContext.Employees.ToList();
            
                //dt.Columns.Add("id");
                dt.Columns.Add("firstName");
                dt.Columns.Add("lastName");
                dt.Columns.Add("hireDate");
                foreach (var employee in employees)
                {
                    dt.Rows.Add(employee.FirstName, employee.LastName, employee.HiringDate);
                }

            
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream s = new MemoryStream())
                {
                    wb.SaveAs(s);
                    return File(s.ToArray()
                        , "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Grid.xlsx");
                }
            };
        }

     
    }
}
