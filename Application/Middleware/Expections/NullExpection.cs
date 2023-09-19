using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Middleware.Expections
{
    public class NullExpection : Exception
    {
        public NullExpection(string message):base(message) { }
    }
}
