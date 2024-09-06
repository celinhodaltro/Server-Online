using System;
using System.Collections.Generic;
using System.Entities;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Entities
{
    public class User : DefaultDb
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}