using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Middleware.Expections
{
    public class NotFountExpection : Exception
    {
        public NotFountExpection(string message):base(message) { }
    }
}
