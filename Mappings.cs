using AutoMapper;
using FeladatAPI.Models;

namespace FeladatAPI
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Book, GetBookResponse>();
            CreateMap<CreateBookRequest, Book>();
            CreateMap<ModifyBookRequest, Book>();

            CreateMap<Author, GetAuthorResponse>();
            CreateMap<CreateAuthorRequest, Author>();
            CreateMap<ModifyAuthorRequest, Author>();

            CreateMap<Book, GetBookWithAuthorResponse>()
                .ForMember(dest => dest.AuthorFirstName, opt => opt.MapFrom(src => src.Author.FirstName))
                .ForMember(dest => dest.AuthorLastName, opt => opt.MapFrom(src => src.Author.LastName))
                .ForMember(dest => dest.AuthorNationalityName, opt => opt.MapFrom(src => src.Author.Nationality.Name));
        }
    }
}