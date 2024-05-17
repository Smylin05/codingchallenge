using dao;
using HosManagement.DAO;
using HosManagement.Model;
using HosManagement.Service;
using System.Data.SqlClient;
using static HosManagement.Util.DBPropertyUtil;

namespace HosManagement
{
    internal class Program
    {
        private static readonly object appointmentId;

        static void Main(string[] args)

        {
            Appointment appointment= new Appointment();
            Console.WriteLine("---------------GEt total Artwork List Start-----------------------");
            List<Appointment> AppointmentList = GetAppointmentList();

            // Get SqlConnection object
            using (SqlConnection connection = DBConnection.GetConnection())
            {
                try
                {
                    // Open the connection
                    connection.Open();

                    // Connection established, you can execute SQL queries here
                    Console.WriteLine("Connected to database.");

                    // Close the connection
                    //connection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }

            // Get the appointment by ID
            Appointment appointment = HospitalServiceImpl.GetAppointmentById(appointmentId);

            if (appointment != null)
            {
                // Print appointment details
                Console.WriteLine($"Appointment ID: {appointment.appointmentId}");
                Console.WriteLine($"Patient ID: {appointment.PatientId}");
                Console.WriteLine($"Doctor ID: {appointment.DoctorId}");
                Console.WriteLine($"Appointment Date: {appointment.appointmentDate}");
                Console.WriteLine($"Description: {appointment.description}");
            }
            else
            {
                // Print message if appointment is not found
                Console.WriteLine("Appointment not found.");
            }



        }


    }
}
