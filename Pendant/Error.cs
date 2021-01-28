using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pendant
{
    public class Error
    {
        /// <summary>
        /// Message for the error
        /// </summary>
        public string ErrorMessage { get; set; }
        /// <summary>
        /// Type of error, for example, naming convention error
        /// </summary>
        public string TypeOfError { get; set; }

        public Error(string emessage, string type)
        {
            ErrorMessage = emessage;
            TypeOfError = type;
        }
    }
}
