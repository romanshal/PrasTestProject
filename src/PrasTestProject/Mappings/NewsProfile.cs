using AutoMapper;
using PrasTestProject.Data.Entities;
using PrasTestProject.Features.News.Commands.Create;
using PrasTestProject.Models.ViewModels;

namespace PrasTestProject.Mappings
{
    public class NewsProfile : Profile
    {
        public NewsProfile()
        {
            CreateMap<News, NewsViewModel>()
                .ForMember(dest => dest.ImagePath, opt => opt.MapFrom(src => "/" + src.ImagePath));

            CreateMap<News, NewsDetailsViewModel>()
                .IncludeBase<News, NewsViewModel>()
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Body));

            CreateMap<CreateCommand, News>();

        }
    }
}
