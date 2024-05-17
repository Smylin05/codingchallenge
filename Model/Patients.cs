using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HosManagement.Model
{
   public class Patients


    {
        public int PatientID { get; set; }

        public string firstName { get; set;}

        public string lastName { get; set;}
        public DateTime  DOB { get; set;}

        public string gender { get; set;}

        public string contantNumber { get; set;}

        public string address { get; set;}
    }
}
