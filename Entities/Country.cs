namespace EFC_Practice;

public partial class Country
{
    public Country()
    {
        Locations = new HashSet<Location>();
    }

    public string CountryId { get; set; } = null!;
    public string? CountryName { get; set; }
    public int RegionId { get; set; }

    public virtual Region Region { get; set; } = null!;
    public virtual ICollection<Location> Locations { get; set; }
}
