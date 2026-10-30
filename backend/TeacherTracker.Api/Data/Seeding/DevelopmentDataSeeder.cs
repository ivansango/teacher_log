
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TeacherTracker.Api.Models;

namespace TeacherTracker.Api.Data.Seeding;

public static class DevelopmentDataSeeder
{
    public static async Task SeedAsync(
        IServiceProvider services,
        IConfiguration configuration)
    {
        using var scope = services.CreateScope();

        var context = scope.ServiceProvider
            .GetRequiredService<ApplicationDbContext>();

        var userManager = scope.ServiceProvider
            .GetRequiredService<UserManager<ApplicationUser>>();

        var schoolName = configuration["DevelopmentSeed:SchoolName"];
        var schoolOib = configuration["DevelopmentSeed:SchoolOib"];
        var schoolAddress = configuration["DevelopmentSeed:SchoolAddress"];
        var schoolCity = configuration["DevelopmentSeed:SchoolCity"];
        var adminEmail = configuration["DevelopmentSeed:AdminEmail"];
        var adminPassword = configuration["DevelopmentSeed:AdminPassword"];

        if (new[]
        {
            schoolName, schoolOib, schoolAddress,
            schoolCity, adminEmail, adminPassword
        }.Any(string.IsNullOrWhiteSpace))
        {
            throw new InvalidOperationException(
                "DevelopmentSeed konfiguracija nije potpuna.");
        }

        if (schoolOib!.Length != 11)
        {
            throw new InvalidOperationException(
                "OIB testne škole mora imati 11 znakova.");
        }

        // 1. Pronađi ili kreiraj testnu školu.
        var school = await context.Schools
            .SingleOrDefaultAsync(s => s.OIB == schoolOib);

        if (school == null)
        {
            school = new School
            {
                Name = schoolName!,
                OIB = schoolOib,
                Address = schoolAddress!,
                City = schoolCity!,
                IsActive = true
            };

            context.Schools.Add(school);
            await context.SaveChangesAsync();
        }

        // 2. Pronađi ili kreiraj administratora.
        var user = await userManager.FindByEmailAsync(adminEmail!);

        if (user == null)
        {
            user = new ApplicationUser
            {
                UserName = adminEmail,
                Email = adminEmail
            };

            var result = await userManager.CreateAsync(
                user, adminPassword!);

            if (!result.Succeeded)
            {
                var errors = string.Join(
                    "; ",
                    result.Errors.Select(e => e.Description));

                throw new InvalidOperationException(
                    $"Kreiranje administratora nije uspjelo: {errors}");
            }
        }

        // 3. Provjeri postojeće članstvo.
        var membershipExists = await context.SchoolMemberships
            .AnyAsync(sm =>
                sm.UserId == user.Id &&
                sm.SchoolId == school.Id &&
                sm.Role == "SchoolAdmin");

        if (!membershipExists)
        {
            context.SchoolMemberships.Add(new SchoolMembership
            {
                UserId = user.Id,
                SchoolId = school.Id,
                Role = "SchoolAdmin",
                IsActive = true
            });

            await context.SaveChangesAsync();
        }
    }
}
