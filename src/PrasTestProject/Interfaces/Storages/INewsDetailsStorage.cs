using PrasTestProject.Models.ViewModels;

namespace PrasTestProject.Interfaces.Storages
{
    public interface INewsDetailsStorage
    {
        Task<NewsDetailsViewModel?> Handle(Guid id, CancellationToken cancellationToken = default);
    }
}
