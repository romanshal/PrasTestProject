using Microsoft.AspNetCore.Identity;
using PrasTestProject.Constants;
using PrasTestProject.Data.Entities;

namespace PrasTestProject.Data.Contexts
{
    public class DataSeedMaker
    {
        public static async Task SeedAsync(
            NewsDbContext context,
            ILogger<DataSeedMaker> logger,
            RoleManager<IdentityRole<Guid>> roleManager,
            UserManager<IdentityUser<Guid>> userManager)
        {

            logger.LogInformation("Seed data associated with context {context}", nameof(NewsDbContext));

            if (!context.News.Any())
            {
                context.News.AddRange(GetPreconfiguredNews());
                await context.SaveChangesAsync();
            }

            if (!context.Roles.Any())
            {
                foreach (var role in Enum.GetNames<AppRoles>())
                {
                    if (!await roleManager.RoleExistsAsync(role))
                        await roleManager.CreateAsync(new IdentityRole<Guid>(role));
                }
            }

            if (!context.Users.Any())
            {
                var admin = new IdentityUser<Guid> { UserName = "Admin", Email = "admin@gmail.com" };
                var result = await userManager.CreateAsync(admin, "Admin1admin");
                await userManager.AddToRoleAsync(admin, AppRoles.Admin.ToString());
            }

            logger.LogInformation("Successfully seed data associated with context {context}", nameof(NewsDbContext));
        }

        private static IEnumerable<News> GetPreconfiguredNews()
        {
            return [
                new News {
                    Title = "AI startup raises $50M",
                    Subtitle = "Investors bet on generative tech",
                    Body = "A young company secured major funding to expand its AI research and global reach.",
                    ImagePath = "uploads/news/20251106_114220.png"
                },
                new News {
                    Title = "Крупный банк внедрил блокчейн",
                    Subtitle = "Технология ускоряет транзакции",
                    Body = "Финансовая организация заявила о переходе на распределённый реестр для международных переводов.",
                    ImagePath = "uploads/news/20251106_122424.png"
                },
                new News {
                    Title = "Space company tests reusable rocket",
                    Subtitle = "Successful landing achieved",
                    Body = "The firm completed a milestone by landing its booster safely for the third time.",
                    ImagePath = "uploads/news/20251106_114511.png"
                },
                new News {
                    Title = "Новый закон о кибербезопасности",
                    Subtitle = "Ужесточение требований к компаниям",
                    Body = "Парламент принял поправки, обязывающие бизнес хранить данные пользователей в стране.",
                    ImagePath = "uploads/news/20251106_114923.png"
                }
            ];
        }
    }
}
