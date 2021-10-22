using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilterDemo.Data
{
    public record Person(int PersonId, string FirstName, string LastName, string City, int Age);
}
