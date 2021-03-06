﻿using AutoMapper;

namespace IronBookStoreAuthJWT.Core.Mapper
{
    public class DomainProfile: Profile
    {
        public DomainProfile()
        {
            CreateMap<Entities.Book, Dtos.Book>();
            CreateMap<Dtos.BookForCreation, Entities.Book>();
            CreateMap<Dtos.BookForUpdate, Entities.Book>();

            CreateMap<Dtos.RegisterUser, Entities.User>();
            CreateMap<Entities.User, Dtos.User>();
        }
    }
}
