using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCCFantasy.Common.Models
{
    public class ErrorMessage
    {
        public ErrorMessage(string description) : this(string.Empty, description)
        {
        }

        public ErrorMessage(string error, string description)
        {
            Error = error;
            Description = description;
        }

        public string Error { get; }
        public string Description { get; }
    }
}
