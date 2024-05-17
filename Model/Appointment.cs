using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HosManagement.Model
{
    public class Appointment
    {
        internal object PatientId;

        public int appointmentId { get; set; }
         public DateTime appointmentDate {  get; set; } 
         public string description {  get; set; }
         public int patientID { get; set; }

         public Patients Patients { get; set; }

         public int DoctorId { get; set; }

         public Doctor Doctor { get; set; }
    }
}
