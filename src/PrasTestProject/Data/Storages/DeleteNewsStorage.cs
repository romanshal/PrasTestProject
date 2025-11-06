using Microsoft.EntityFrameworkCore;
using PrasTestProject.Data.Contexts;
using PrasTestProject.Data.Entities;
using PrasTestProject.Interfaces.Storages;

namespace PrasTestProject.Data.Storages
{
    public class DeleteNewsStorage(NewsDbContext context) : IDeleteNewsStorage
    {
        private readonly NewsDbContext _context = context;

        public async Task<News?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
            await _context.News.FirstOrDefaultAsync(n => n.Id == id, cancellationToken);

        public async Task HandleAsync(News news, CancellationToken cancellationToken = default)
        {
            _context.News.Remove(news);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
