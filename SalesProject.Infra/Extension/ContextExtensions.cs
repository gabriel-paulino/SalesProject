using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using SalesProject.Domain.Entities;
using SalesProject.Domain.Enums;
using SalesProject.Infra.Context;
using System.Linq;

namespace SalesProject.Infra.Extension
{
    public static class ContextExtensions
    {
        public static bool MigrationsApplied(this DataContext context)
        {
            var applied = context.GetService<IHistoryRepository>()
                .GetAppliedMigrations()
                .Select(m => m.MigrationId);

            var total = context.GetService<IMigrationsAssembly>()
                .Migrations
                .Select(m => m.Key);

            return !total.Except(applied).Any();
        }

        public static void Seed(this DataContext context)
        {

            if (!context.Users.Any())
            {
                var admin = new User(
                    username: "admin",
                    name: "Administrator",
                    email: "admin@zpmetais.com",
                    role: RoleType.Administrator
                    );

                var hasher = new PasswordHasher<User>();

                admin.EncryptPassword(hasher.HashPassword(admin, "admin"));

                context.Users.Add(admin);
                context.SaveChanges();
            }
        }
    }
}