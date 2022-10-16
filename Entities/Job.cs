namespace EFC_Practice;

public partial class Job
{
    public Job()
    {
        Employees = new HashSet<Employee>();
    }

    public int JobId { get; set; }
    public string JobTitle { get; set; } = null!;
    public decimal? MinSalary { get; set; }
    public decimal? MaxSalary { get; set; }

    public virtual ICollection<Employee> Employees { get; set; }
}
