namespace EFC_Practice;

public partial class Dependent
{
    public int DependentId { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Relationship { get; set; } = null!;
    public int EmployeeId { get; set; }

    public virtual Employee Employee { get; set; } = null!;
}
