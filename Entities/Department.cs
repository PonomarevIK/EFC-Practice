namespace EFC_Practice;

public partial class Department
{
    public Department()
    {
        Employees = new HashSet<Employee>();
    }

    public int DepartmentId { get; set; }
    public string DepartmentName { get; set; } = null!;
    public int? LocationId { get; set; }

    public virtual Location? Location { get; set; }
    public virtual ICollection<Employee> Employees { get; set; }
}
