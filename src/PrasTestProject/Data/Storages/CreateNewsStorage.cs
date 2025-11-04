using PrasTestProject.Data.Contexts;
using PrasTestProject.Data.Entities;
using PrasTestProject.Interfaces.Storages;

namespace PrasTestProject.Data.Storages
{
    public class CreateNewsStorage(
        NewsDbContext context) : ICreateNewsStorage
    {
        private readonly NewsDbContext _context = context;

        public async Task<News> CreateAsync(News news, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(news, nameof(news));

            await _context.News.AddAsync(news, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return news;
        }
    }
}
