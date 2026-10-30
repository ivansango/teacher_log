using Microsoft.AspNetCore.Identity;
using TeacherTracker.Api.Models;

namespace TeacherTracker.Api.Services;

public class UserService
{
    private readonly UserManager<ApplicationUser> _userManager;

    public UserService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<IdentityResult> CreateUserAsync(
        string email,
        string password)
    {
        var existingUser =
            await _userManager.FindByEmailAsync(email);

        if (existingUser != null)
        {
            return IdentityResult.Failed(
                new IdentityError
                {
                    Code = "DuplicateEmail",
                    Description = "Korisnik s tom e-mail adresom već postoji."
                });
        }

        var user = new ApplicationUser
        {
            UserName = email,
            Email = email
        };

        return await _userManager.CreateAsync(user, password);
    }
}