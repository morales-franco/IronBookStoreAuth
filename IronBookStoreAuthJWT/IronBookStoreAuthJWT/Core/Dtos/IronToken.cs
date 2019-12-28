using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IronBookStoreAuthJWT.Core.Dtos
{
    public class IronToken
    {
        public string Token { get; private set; }

        public IronToken(string token)
        {
            Token = token;
        }
    }
}
