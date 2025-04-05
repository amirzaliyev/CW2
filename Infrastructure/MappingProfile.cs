using CW2.Models;
using AutoMapper;
using CW2.DAL.Entities;

namespace CW2.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //CreateMap<Author, AuthorViewModel>().ReverseMap();
            //CreateMap<Publisher, PublisherViewModel>().ReverseMap();
            //CreateMap<Shipper, ShipperViewModel>().ReverseMap();
            //CreateMap<Genre, GenreViewModel>().ReverseMap();
            //CreateMap<Book, BookViewModel>().ReverseMap();
            //CreateMap<BookGenre, BookGenreViewModel>().ReverseMap();
            //CreateMap<Discount, DiscountViewModel>().ReverseMap();
            //CreateMap<BookDiscount, BookDiscountViewModel>().ReverseMap();
            CreateMap<Customer, CustomerViewModel>().ReverseMap();
            //CreateMap<Order, OrderViewModel>().ReverseMap();
            //CreateMap<OrderDetails, OrderDetailsViewModel>().ReverseMap();
            //CreateMap<Shipper, ShipperViewModel>().ReverseMap();
        }
    }
}
