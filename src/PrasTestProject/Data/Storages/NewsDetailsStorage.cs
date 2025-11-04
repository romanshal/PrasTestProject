using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using PrasTestProject.Data.Contexts;
using PrasTestProject.Interfaces.Storages;
using PrasTestProject.Models.ViewModels;

namespace PrasTestProject.Data.Storages
{
    public class NewsDetailsStorage(
        NewsDbContext context,
        IMapper mapper) : INewsDetailsStorage
    {
        private readonly NewsDbContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<NewsDetailsViewModel?> Handle(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.News
                .AsNoTracking()
                .ProjectTo<NewsDetailsViewModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(news => news.Id == id, cancellationToken);
        }
    }
}
