using HosManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HosManagement.Service
{
 public  class HospitalServiceImpl:IHospitalServiceImpl
    {
       

        public void GetAppointmentById(Appointment appointmentId)
   
            {
                Console.WriteLine("Enter the appointmentId");
        
                int appointmentId = int.Parse(Console.ReadLine());
                Appointment appointment = HospitalServiceImpl.GetAppointmentById(appointmentId);

                if (appointment != null)
                {
                    Console.WriteLine(appointment);
                }
                else
                {
                    throw new ("Appointment does not exist.");
                }
            }

        private static Appointment GetAppointmentById(int appointmentId)
        {
            throw new NotImplementedException();
        }

        public static void UpdateAppointmentDetailsFromUser(Appointment appointment)
        {
            Console.WriteLine("Enter the Appointment ID:");
            Console.Write("Appointment ID: ");
            string input = Console.ReadLine();

            // Parse the input string to an integer
            if (int.TryParse(input, out int appointmentID))
            {
                // Set the appointment ID
                appointment.appointmentId = appointmentID;
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid integer for Appointment ID.");
                // Optionally, you can handle the invalid input scenario here
            }

            Console.WriteLine("Enter updated appointment details:");

            Console.Write("Patient ID: ");
            if (!int.TryParse(Console.ReadLine(), out int patientId))
            {
                throw new ArgumentException("Invalid input. Please enter a valid integer for Patient ID.");
            }
            appointment.patientID = patientId;

            Console.Write("Doctor ID: ");
            if (!int.TryParse(Console.ReadLine(), out int doctorId))
            {
                throw new ArgumentException("Invalid input. Please enter a valid integer for Doctor ID.");
            }
            appointment.DoctorId = doctorId;

            Console.Write("Appointment Date and Time (YYYY-MM-DD HH:mm:ss): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime appointmentDate))
            {
                throw new ArgumentException("Invalid date format. Please enter date in YYYY-MM-DD HH:mm:ss format.");
            }
            appointment.appointmentDate = appointmentDate;

            Console.Write("Description: ");
            appointment.description = Console.ReadLine();
        }

        public void GetAppointmentById()
        {
            throw new NotImplementedException();
        }
    }
}

