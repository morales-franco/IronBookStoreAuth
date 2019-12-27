using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IronBookStoreAuthJWT.Core.Dtos
{
    public class Book: BookAbstractBase
    {
        public Guid BookId { get; set; }
    }
}
