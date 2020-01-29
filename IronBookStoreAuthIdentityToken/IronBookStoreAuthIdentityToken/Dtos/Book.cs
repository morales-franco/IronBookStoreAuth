using System;

namespace IronBookStoreAuthIdentityToken.Dtos
{
    public class Book : BookAbstractBase
    {
        public Guid BookId { get; set; }
    }
}
