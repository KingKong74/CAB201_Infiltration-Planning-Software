using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment_2_Final
{
    public class InvalidOptionException : Exception
    {
        public InvalidOptionException() : base("Invalid option.") { }

        public InvalidOptionException(string message) : base(message) { }
    }

    public class InvalidDirectionException : Exception
    {
        public InvalidDirectionException() : base("Fences must be horizontal or vertical.") { }

        public InvalidDirectionException(string message) : base(message) { }
    }

    public class InvalidSpecificationException : Exception
    {
        public InvalidSpecificationException() : base("Invalid map specification.") { }

        public InvalidSpecificationException(string message) : base(message) { }
    }
}
