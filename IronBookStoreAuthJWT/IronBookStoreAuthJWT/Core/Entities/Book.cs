using System;

namespace IronBookStoreAuthJWT.Core.Entities
{
    public class Book: IAuditable
    {
        public Guid BookId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
