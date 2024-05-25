using EFTableToModelApp;

using (HrDbContext context = new())
{
    Company yandex = new() { Title = "Yandex" };
    Employee bob = new Employee()
    {
        Name = "Bobby",
        Age = 28,
        Company = yandex
    };
    context.Employees.Add(bob);

    Project devMobileApp = new Project()
    {
        Title = "Develop of mobile application"
    };
    devMobileApp.Employees.Add(bob);
    bob.Projects.Add(devMobileApp);

    context.SaveChanges();
}