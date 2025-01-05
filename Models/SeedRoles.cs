﻿using Microsoft.AspNetCore.Identity;

namespace BibliotecaUtad.Models
{
    public static class SeedRoles
    {
        public static void Seed(RoleManager<IdentityRole> roleManager)
        {
            if(roleManager.Roles.Any()==false) 
            {
                roleManager.CreateAsync(new IdentityRole("Administrador")).Wait();
                roleManager.CreateAsync(new IdentityRole("Bibliotecário")).Wait();
                roleManager.CreateAsync(new IdentityRole("Leitor")).Wait();
            }
        }
    }
}
