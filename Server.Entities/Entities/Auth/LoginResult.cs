using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Entities
{
    public class LoginResult
    {
        public string? Error { get; set; }
        public string? Token { get; set; }
        public string? Expiration { get; set; }
    }
}
