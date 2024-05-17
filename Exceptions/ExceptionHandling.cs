using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HosManagement.Exceptions
{
    public class ExceptionHandling:Exception
    {

        public ExceptionHandling(string message)
        {
        }

       

        public class PatientNumberNotFoundException : ExceptionHandling
        {
            public PatientNumberNotFoundException(string message) : base(message) { }
        }


    }
}
