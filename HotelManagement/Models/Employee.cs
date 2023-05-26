using System.ComponentModel.DataAnnotations;

namespace AssessmentAPI.Model
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        public string? EmployeeName { get; set; }
        public string? EmployeeCreatedAt { get; set; }
        public Hotel? Hotel { get; set; }
    }
}
