namespace EFC_Practice;

public partial class Employee
{
    public Employee()
    {
        Dependents = new HashSet<Dependent>();
        InverseManager = new HashSet<Employee>();
    }

    public int EmployeeId { get; set; }
    public string? FirstName { get; set; }
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? PhoneNumber { get; set; }
    public DateTime HireDate { get; set; }
    public int JobId { get; set; }
    public decimal Salary { get; set; }
    public int? ManagerId { get; set; }
    public int? DepartmentId { get; set; }

    public virtual Department? Department { get; set; }
    public virtual Job Job { get; set; } = null!;
    public virtual Employee? Manager { get; set; }
    public virtual ICollection<Dependent> Dependents { get; set; }
    public virtual ICollection<Employee> InverseManager { get; set; }
}
