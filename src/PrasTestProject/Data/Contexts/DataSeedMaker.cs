using PrasTestProject.Data.Entities;

namespace PrasTestProject.Data.Contexts
{
    public class DataSeedMaker
    {
        public static async Task SeedAsync(NewsDbContext context, ILogger<DataSeedMaker> logger)
        {
            if (!context.News.Any())
            {
                context.News.AddRange(GetPreconfiguredNews());
                await context.SaveChangesAsync();
                logger.LogInformation("Seed database associated with context {context}", nameof(NewsDbContext));
            }
        }

        private static IEnumerable<News> GetPreconfiguredNews()
        {
            return [
                new News {
                    Title = "AI startup raises $50M",
                    Subtitle = "Investors bet on generative tech",
                    Body = "A young company secured major funding to expand its AI research and global reach."
                },
                new News {
                    Title = "Крупный банк внедрил блокчейн",
                    Subtitle = "Технология ускоряет транзакции",
                    Body = "Финансовая организация заявила о переходе на распределённый реестр для международных переводов."
                },
                new News {
                    Title = "Space company tests reusable rocket",
                    Subtitle = "Successful landing achieved",
                    Body = "The firm completed a milestone by landing its booster safely for the third time."
                },
                new News {
                    Title = "Новый закон о кибербезопасности",
                    Subtitle = "Ужесточение требований к компаниям",
                    Body = "Парламент принял поправки, обязывающие бизнес хранить данные пользователей в стране."
                },
                new News {
                    Title = "Tech giant unveils AR glasses",
                    Subtitle = "Blending digital with reality",
                    Body = "The device promises immersive experiences for gaming, work, and education."
                },
                new News {
                    Title = "Стартап по доставке дронов",
                    Subtitle = "Первый коммерческий рейс",
                    Body = "Компания успешно доставила посылку с помощью беспилотника в городской черте."
                },
                new News {
                    Title = "Global summit on climate change",
                    Subtitle = "Nations pledge new targets",
                    Body = "Leaders agreed to accelerate renewable energy adoption worldwide."
                },
                new News {
                    Title = "Российский университет открыл ИИ-центр",
                    Subtitle = "Фокус на машинном обучении",
                    Body = "Центр будет заниматься исследованиями и подготовкой специалистов в области искусственного интеллекта."
                },
                new News {
                    Title = "Electric car sales hit record",
                    Subtitle = "Shift to sustainable transport",
                    Body = "Consumers are rapidly adopting EVs as charging infrastructure expands."
                },
                new News {
                    Title = "Нефтяная компания инвестирует в ВИЭ",
                    Subtitle = "Ставка на солнечную и ветровую энергию",
                    Body = "Крупнейший игрок рынка объявил о многомиллиардных вложениях в зелёные проекты."
                },
                new News {
                    Title = "Healthcare app gains popularity",
                    Subtitle = "Remote consultations on the rise",
                    Body = "Millions are using digital platforms to access doctors without leaving home."
                },
                new News {
                    Title = "Новый рекорд по экспорту зерна",
                    Subtitle = "Сельское хозяйство усиливает позиции",
                    Body = "Аграрный сектор сообщил о рекордных поставках продукции за рубеж."
                },
                new News {
                    Title = "Cyberattack hits major retailer",
                    Subtitle = "Customer data exposed",
                    Body = "Hackers breached systems, prompting urgent security upgrades."
                },
                new News {
                    Title = "Город запускает умные светофоры",
                    Subtitle = "Снижение пробок и аварий",
                    Body = "Система адаптивного управления движением внедрена на центральных улицах."
                },
                new News {
                    Title = "Biotech firm develops new vaccine",
                    Subtitle = "Promising trial results",
                    Body = "Early studies show strong immune response and fewer side effects."
                }
            ];
        }
    }
}
