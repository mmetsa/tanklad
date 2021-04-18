using System;
using System.Collections.Generic;
using System.Linq;
using Domain.App.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace DAL.App.EF.AppDataInit
{
    public class DataInit
    {
        public static void SeedIdentity(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, IConfiguration configuration)
        {
            var roles = configuration.GetSection("AppData:Roles").GetChildren().ToList();
            var users = configuration.GetSection("AppData:Users").GetChildren().ToList();
            roles.ForEach(r =>
            {
                var appRole = new AppRole {Name = r.Value, DisplayName = r.Value};
                var result = roleManager.CreateAsync(appRole).Result;
                if (result.Succeeded) return;
                foreach (var error in result.Errors)
                {
                    Console.WriteLine("Can't create role! Error: " + error.Description);   
                }
            });

            users.ForEach(u =>
            {
                var props = u.GetChildren().ToList();
                var values = new List<string>();
                foreach (var prop in props)
                {
                    values.Add(prop.Value);
                }

                var roles = props[4].GetChildren().ToList();

                var email = values[0];
                var firstName = values[1];
                var lastName = values[2];
                var password = values[3];

                var user = new AppUser();
                user.Email = email;
                user.Firstname = firstName;
                user.Lastname = lastName;
                user.UserName = email;
                user.EmailConfirmed = true;
                user.PhoneNumberConfirmed = true;
                user.SecurityStamp = Guid.NewGuid().ToString();
                var result = userManager.CreateAsync(user, password).Result;
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        Console.WriteLine("Can't create user! Error: " + error.Description);   
                    }
                }

                foreach (var role in roles)
                {
                    result = userManager.AddToRoleAsync(user, role.Value).Result;
            
                    if (!result.Succeeded)
                    {
                        foreach (var error in result.Errors)
                        {
                            Console.WriteLine("Can't add user to role! Error: " + error.Description);   
                        }
                    }
                }
            });
        }
    }
}