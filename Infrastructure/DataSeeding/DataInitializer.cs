using Application.Interfaces.DataSeeding;
using Application.Interfaces.UnitOfWork;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.DataSeeding
{
    public class DataInitializer : IDataInitializer
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public DataInitializer(IUnitOfWork unitOfWork, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task InitializeIdentityDataAsync()
        {
            if(!await _unitOfWork.Categories.AnyAsync(c => c.Name.ToLower() == "backend".ToLower()))
            {
                await _unitOfWork.Categories.AddEntityAsync(new Category { Name = "Backend" });
            }
            if(!await _unitOfWork.Categories.AnyAsync(c => c.Name.ToLower() == "frontend".ToLower()))
            {
                await _unitOfWork.Categories.AddEntityAsync(new Category { Name = "Frontend" });
            }
            if(!await _unitOfWork.Categories.AnyAsync(c => c.Name.ToLower() == "ai".ToLower()))
            {
                await _unitOfWork.Categories.AddEntityAsync(new Category { Name = "AI" });
            }
            if(!await _unitOfWork.Categories.AnyAsync(c => c.Name.ToLower() == "web development".ToLower()))
            {
                await _unitOfWork.Categories.AddEntityAsync(new Category { Name = "Web Development" });
            }
            if(!await _unitOfWork.Categories.AnyAsync(c => c.Name.ToLower() == "programming".ToLower()))
            {
                await _unitOfWork.Categories.AddEntityAsync(new Category { Name = "Programming" });
            }
            if(!await _unitOfWork.Categories.AnyAsync(c => c.Name.ToLower() == "mobile development".ToLower()))
            {
                await _unitOfWork.Categories.AddEntityAsync(new Category { Name = "Mobile Development" });
            }
            await _unitOfWork.SaveChangesAsync();


            if (!await _roleManager.RoleExistsAsync("User"))
            {
                var userRole = new IdentityRole("User");
                await _roleManager.CreateAsync(userRole);
            }


            if (!await _roleManager.RoleExistsAsync("Admin"))
            {
                var adminRole = new IdentityRole("Admin");
                await _roleManager.CreateAsync(adminRole);
            }

            var adminEmail = "esraataha@gmail.com";

            var admin = await _userManager.FindByEmailAsync(adminEmail);
            if (admin is null)
            {
                admin = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    FirstName = "Esraa",
                    LastName = "Taha"
                };

                var res = await _userManager.CreateAsync(admin, "MeawMeaw309");

                if (!res.Succeeded)
                {
                    throw new Exception($"Failed to create admin user: {string.Join(", ", res.Errors.Select(e => e.Description))}");
                }
            }

            if (!await _userManager.IsInRoleAsync(admin, "Admin"))
            {
                await _userManager.AddToRoleAsync(admin, "Admin");
            }
        }
    }
}