using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using PrasTestProject.Data.Contexts;
using PrasTestProject.Interfaces.Storages;
using PrasTestProject.Models.ViewModels;

namespace PrasTestProject.Data.Storages
{
    public class NewsListStorage(
        NewsDbContext context,
        IMapper mapper) : INewsListStorage
    {
        private readonly NewsDbContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<(IReadOnlyList<NewsViewModel> items, int total)> Handle(
            int page,
            int pageSize,
            CancellationToken cancellationToken = default)
        {
            var query = _context.News
                .AsNoTracking()
                .OrderByDescending(x => x.CreatedAtUtc);

            var total = await query.CountAsync(cancellationToken);
            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ProjectTo<NewsViewModel>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return (items, total);
        }
    }
}
