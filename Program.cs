using Microsoft.EntityFrameworkCore;
using System.Runtime.Intrinsics.X86;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using HrDbContext = EFC_Practice.sample_hr_databaseContext; // Short alias for a long class name


namespace EFC_Practice;

public class Program
{
    static void Main()
    {
        // Create
        using (HrDbContext context = new HrDbContext())
        {
            // Новый регион
            Region antarctica = new() { RegionName = "Antarctica" };
            context.Regions.Add(antarctica);

            // Новые страны
            Country wilkesLand = new() { Region = antarctica, CountryId = "WL", CountryName = "Wilkes Land" };
            Country enderbyLand = new() { Region = antarctica, CountryId = "EL", CountryName = "Enderby Land" };
            Country spain = new() { RegionId = 1, CountryId = "SP", CountryName = "Spain" };
            context.Countries.AddRange(wilkesLand, enderbyLand, spain);

            // Новые филиалы в Антарктике и Германии
            Location locationWL = new() { Country = wilkesLand, StreetAddress = "12 Icy Road", City = "Iceford" };
            context.Locations.Add(locationWL);

            Location locationDE = new() { CountryId = "DE", StreetAddress = "134 Schnitzel Strasse", StateProvince = "Sachsen", City = "Dresden" };
            context.Locations.Add(locationDE);

            // У работника с 112 родился новый ребёнок
            Dependent newborn = new() { FirstName = "Jonathan", LastName = "Doe", Relationship = "Child", EmployeeId = 112 };
            context.Dependents.Add(newborn);

            context.SaveChanges();
        }

        // Read
        using (HrDbContext context = new HrDbContext())
        {
            // Все департаменты в США, отсортированные по количеству сотрудников
            var usDepts = context.Departments
                .Include(d => d.Location)                  // Eager loading
                .Include(d => d.Employees)                 // 
                .Where(d => d.Location.CountryId == "US")
                .OrderBy(d => d.Employees.Count);
            Console.WriteLine("Департаменты, находящиеся в США:");
            foreach (var dept in usDepts)
            {
                Console.WriteLine($"{dept.DepartmentName}, {dept.Employees.Count} сотрудников");
            }
        }

        // Update
        using (HrDbContext context = new HrDbContext())
        {
            // Повысить зарплату всем работникам, у которых есть дети
            var parents = context.Employees.Where(e => e.Dependents.Count > 0);
            foreach (var parent in parents)
            {
                parent.Salary += 200;
            }

            // Сотрудник Neena Cochhar вышла замуж и сменила фамилию, адрес электронной почты и фамилию ребёнка
            var neena = context.Employees.FirstOrDefault(e => e.FirstName == "Neena" && e.LastName == "Kochhar");
            if (neena != null)
            {
                neena.LastName = "Smith";
                neena.Email = "neena.smith@sqltutorial.org";
                context.Entry(neena).Collection(t => t.Dependents).Load();  // Explicit loading
                foreach (var child in neena.Dependents)
                {
                    child.LastName = "Smith";
                }
            }

            // Сотрудника с id 206 перевели в отдел финансов
            var financeDept = context.Departments.FirstOrDefault(d => d.DepartmentName == "Finance");
            var emp206 = context.Employees.FirstOrDefault(e => e.EmployeeId == 206);
            if (financeDept != null && emp206 != null)
            {
                emp206.Department = financeDept;
            }

            context.SaveChanges();
        }

        // Delete
        using (HrDbContext context = new HrDbContext())
        {
            // Отдел доставок закрылся, его сотрудники каскадно удалятся
            var shippingDept = context.Departments.FirstOrDefault(d => d.DepartmentName == "Shipping");
            if (shippingDept != null)
            {
                Console.WriteLine($"\nСотрудников до закрытия отдела: {context.Employees.Count()}");
                context.Departments.Remove(shippingDept);
                context.SaveChanges();
                Console.WriteLine($"Сотрудников после закрытия отдела: {context.Employees.Count()}");
            }
            // Уволить всех сотрудников без номера телефона
            context.RemoveRange(context.Employees.Where(e => e.PhoneNumber == null));
            context.SaveChanges();
            Console.WriteLine($"Сотрудников после увольнения: {context.Employees.Count()}");

            context.SaveChanges();
        }
    }
}