namespace EFC_Practice;

public partial class Location
{
    public Location()
    {
        Departments = new HashSet<Department>();
    }

    public int LocationId { get; set; }
    public string? StreetAddress { get; set; }
    public string? PostalCode { get; set; }
    public string City { get; set; } = null!;
    public string? StateProvince { get; set; }
    public string CountryId { get; set; } = null!;

    public virtual Country Country { get; set; } = null!;
    public virtual ICollection<Department> Departments { get; set; }
}
