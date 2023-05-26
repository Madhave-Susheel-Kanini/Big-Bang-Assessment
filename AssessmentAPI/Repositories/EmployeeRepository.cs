using AssessmentAPI.Model;
using AssessmentAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AssessmentAPI.Repositories
{
    public class EmployeeRepository : IEmployee
    {
        private readonly HotelContext _context;

        public EmployeeRepository(HotelContext context)
        {
            _context = context;
        }

        public IEnumerable<Employee> GetEmployees()
        {
            try
            {
                return _context.Employees.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while retrieving employees.");
                return new List<Employee>();
            }
        }

        public Employee GetEmployeesById(int EmployeeId)
        {
            try
            {
                return _context.Employees.Find(EmployeeId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while retrieving employee with ID {EmployeeId}.");
                return null;
            }
        }

        public Employee PostEmployee(Employee employee)
        {
            try
            {
                var emp = _context.Hotels.Find(employee.Hotel.HotelId);
                employee.Hotel = emp;
                employee.EmployeeCreatedAt = DateTime.UtcNow.ToString();
                _context.Employees.Add(employee);
                _context.SaveChanges();
                return employee;
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while adding a new employee.");
                return null;
            }
        }

        public Employee PutEmployee(int EmployeeId, Employee employee)
        {
            try
            {
                var emp = _context.Hotels.Find(employee.Hotel.HotelId);
                employee.Hotel = emp;
                _context.Entry(employee).State = EntityState.Modified;
                _context.SaveChangesAsync();
                return employee;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while updating employee with ID {EmployeeId}.");
                return null;
            }
        }

        public Employee DeleteEmployee(int EmployeeId)
        {
            try
            {
                var employee = _context.Employees.Find(EmployeeId);
                if (employee != null)
                {
                    _context.Employees.Remove(employee);
                    _context.SaveChanges();
                }
                return employee;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while deleting employee with ID {EmployeeId}.");
                return null;
            }
        }
    }
}
