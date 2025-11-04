using PrasTestProject.Data.Contexts;
using PrasTestProject.Data.Entities;
using Microsoft.EntityFrameworkCore;
using PrasTestProject.Interfaces.Storages;

namespace PrasTestProject.Data.Storages
{
    public class EditNewsStorage(
        NewsDbContext context) : IEditNewsStorage
    {
        private readonly NewsDbContext _context = context;

        public async Task<News?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
            await _context.News.FirstOrDefaultAsync(n => n.Id == id, cancellationToken);
    }
}
