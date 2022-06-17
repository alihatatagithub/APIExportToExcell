using APIExportToExcell.Data;
using APIExportToExcell.Entities;
using APIExportToExcell.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace APIExportToExcell.Repository
{
    public class EmployeeRepository : IEmployee
    {
        private AppDbContext _appDbContext;

        public EmployeeRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<Employee> AddEmployee(Employee employee)
        {
        
            var result = await _appDbContext.Employees.AddAsync(employee);
            await _appDbContext.SaveChangesAsync();
            return result.Entity;

        }

        public async Task<Employee> DeleteEmployee(int employeeid)
        {
            var result = await _appDbContext.Employees.FirstOrDefaultAsync(e => e.Id == employeeid);
            if (result != null)
            {
                _appDbContext.Employees.Remove(result);
                await _appDbContext.SaveChangesAsync();
            }
            return result;

        }

        public async Task<Employee> GetEmployeeByIdAsync(int? employeeid)
        {
            return await _appDbContext.Employees.FirstOrDefaultAsync(e => e.Id == employeeid);
        }

        public  IReadOnlyList<Employee> GetEmployeesAsync()
        {
            return  _appDbContext.Employees.ToList();
        }

        public async Task<Employee> UpdateEmployee(Employee newemployee)
        {
            var result = await _appDbContext.Employees.FirstOrDefaultAsync(e => e.Id == newemployee.Id);

            result.FirstName = newemployee.FirstName;
            result.LastName = newemployee.LastName;
            result.HiringDate = newemployee.HiringDate;
            await _appDbContext.SaveChangesAsync();
            return result;

        }


    }

}
