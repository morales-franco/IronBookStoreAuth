using AutoMapper;

namespace IronBookStoreAuthIdentityToken.Infraestructure.Mapper
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMap<Core.Entities.Book, Dtos.Book>();
            CreateMap<Dtos.BookForCreation, Core.Entities.Book>();
            CreateMap<Dtos.BookForUpdate, Core.Entities.Book>();

            CreateMap<Dtos.RegisterUser, Core.Entities.User>();
            CreateMap<Core.Entities.User, Dtos.User>();
        }
    }
}
