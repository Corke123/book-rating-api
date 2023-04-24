using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using book_rating_api.Dtos;
using book_rating_api.Models;

namespace book_rating_api
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Category, CategoryDto>();
            CreateMap<AddBookRequest, Book>();
            CreateMap<string, Actor>()
                .ConstructUsing(name => new Actor { Name = name });
            CreateMap<ICollection<string>, ICollection<Actor>>()
                .ConvertUsing(src => src.Select(name => new Actor { Name = name }).ToList());
            CreateMap<Book, BookResponse>()
                .ForMember(dest => dest.MainActors, opt => opt.MapFrom(src => src.MainActors.Select(a => a.Name)))
                .ForMember(dest => dest.AverageRating, opt => opt.MapFrom(src => src.UserRatings.Count == 0 ? 0 : src.UserRatings.Average(r => r.Rating)));
        }
    }
}