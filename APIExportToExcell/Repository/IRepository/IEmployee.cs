using APIExportToExcell.Entities;

namespace APIExportToExcell.Repository.IRepository
{
    public interface IEmployee
    {
        IReadOnlyList<Employee> GetEmployeesAsync();
        Task<Employee> GetEmployeeByIdAsync(int? employeeid);

        Task<Employee> AddEmployee(Employee employee);
        Task<Employee> UpdateEmployee(Employee employee);
        Task<Employee> DeleteEmployee(int employeeid);
    }
}
