using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFTableToModelApp
{
    public class Examples
    {
        public static void CreateDatabase()
        {
            using (HrDbContext context = new())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                List<Company> companies = null!;
                List<Employee> employees = null!;
                List<Country> countries = null!;
                List<Position> positions = null!;

                positions = new()
                {
                    new(){ Title = "Manager" },
                    new(){ Title = "Developer" },
                    new(){ Title = "Designer" }
                };

                countries = new()
                {
                    new(){ Title = "Russia" },
                    new(){ Title = "China" },
                    new(){ Title = "Usa" },
                };

                companies = new()
                {
                    new() { Title = "Yandex", Country = countries[0] },
                    new() { Title = "Ozon", Country = countries[0] },
                    new() { Title = "Avito", Country = countries[0] },

                    new() { Title = "Huawai", Country = countries[1] },
                    new() { Title = "Xiaomi", Country = countries[1] },

                    new() { Title = "Microsoft", Country = countries[2] },
                    new() { Title = "Google", Country = countries[2] },

                };
                employees = new()
                {
                    new(){ Name = "Bobby", Age = 35, Company = companies[0], Position = positions[0] },
                    new(){ Name = "Sammy", Age = 22, Company = companies[1], Position = positions[1] },
                    new(){ Name = "Jommy", Age = 41, Company = companies[2], Position = positions[1] },
                    new(){ Name = "Tommt", Age = 26, Company = companies[0], Position = positions[0] },
                    new(){ Name = "Billy", Age = 48, Company = companies[1], Position = positions[2] },
                    new(){ Name = "Kenny", Age = 31, Company = companies[0], Position = positions[2] },

                    new(){ Name = "Chi Li", Age = 27, Company = companies[3], Position = positions[1] },
                    new(){ Name = "Dao Sin", Age = 33, Company = companies[4], Position = positions[0] },
                    new(){ Name = "Kio San", Age = 21, Company = companies[3], Position = positions[2] },

                    new(){ Name = "Mikle", Age = 38, Company = companies[5], Position = positions[2] },
                    new(){ Name = "Anna", Age = 23, Company = companies[6], Position = positions[1] },
                    new(){ Name = "Susan", Age = 20, Company = companies[6], Position = positions[0] },
                };
                context.Companies.AddRange(companies);
                context.Employees.AddRange(employees);
                context.Countries.AddRange(countries);
                context.Positions.AddRange(positions);
                context.SaveChanges();
            }
        }
    }
}
