using PrasTestProject.Models.ViewModels;

namespace PrasTestProject.Interfaces.Storages
{
    public interface INewsListStorage
    {
        Task<(IReadOnlyList<NewsViewModel> items, int total)> Handle(
            int page,
            int pageSize,
            CancellationToken cancellationToken = default);
    }
}
