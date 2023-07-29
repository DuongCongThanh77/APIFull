using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppApi.Token
{
    public class TokenModel
    {
        public string accessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
