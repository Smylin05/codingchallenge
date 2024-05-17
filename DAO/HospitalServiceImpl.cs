using System;
using System.Data.SqlClient;
using HosManagement.DAO;
using HosManagement.Exceptions;
using HosManagement.Model;


namespace dao
{
    public class HospitalService : IHospitalService
    {
        private readonly string connectionString;
        private object updatedAppointment;

        public HospitalService(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public Appointment GetAppointmentById(int appointmentId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Appointments WHERE AppointmentId = @AppointmentId";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@AppointmentId", appointmentId);

                        connection.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                Appointment appointment = new Appointment
                                {
                                    appointmentId = Convert.ToInt32(reader["AppointmentId"]),
                                    patientID = Convert.ToInt32(reader["PatientId"]),
                                    DoctorId = Convert.ToInt32(reader["DoctorId"]),
                                    appointmentDate = reader["AppointmentDate"] != DBNull.Value ? Convert.ToDateTime(reader["AppointmentDate"]) : DateTime.MinValue,
                                    description = reader["Description"].ToString()
                                };
                                Console.WriteLine($"AppointmentId: {appointment.appointmentId}, PatientId: {appointment.PatientId}, DoctorId: {appointment.DoctorId}, AppointmentDate: {appointment.appointmentDate}, Description: {appointment.description}");

                                return appointment;
                            }
                            else
                            {
                                return null;
                            }
                        }
                    }
                }
            }
            
            catch (Exception ex)
            {
                // Handle other exceptions
                throw new Exception("An error occurred while fetching the appointment.", ex);
            }
        }

        public bool CancelAppointment(int appointmentID)
        {
            try
            {
                if (appointmentID <= 0)
                {
                    throw new ArgumentException("Appointment ID must be greater than 0", nameof(appointmentID));
                }

                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand command = connection.CreateCommand())
                {
                    string sql = "DELETE FROM Artwork WHERE AppointmentID = @AppointmentID";

                    command.CommandText = sql;
                    command.Parameters.AddWithValue("@AppointmentID", appointmentID);

                    connection.Open();

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Appointment canceled successfully.");
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("Failed to cancel appointment. Appointment with specified ID not found.");
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new AppointmentNotFoundException(ex.Message);
            }
        }

        public bool UpdateAppointment(Appointment appointment)
        {
            try
            {
                AppointmentManagement.UpdateAppointmentDetailsFromUser(updatedAppointment);

                if (updatedAppointment == null)
                {
                    throw new ArgumentNullException(nameof(updatedAppointment));
                }

                if (updatedAppointment.AppointmentId == 0)
                {
                    throw new ArgumentException("Appointment ID cannot be 0 for updating existing appointment", nameof(updatedAppointment));
                }

                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand command = connection.CreateCommand())
                {
                    string sql = "UPDATE Appointments SET PatientId = @PatientId, DoctorId = @DoctorId, AppointmentDate = @AppointmentDate, Description = @Description WHERE AppointmentId = @AppointmentId;";

                    command.CommandText = sql;
                    command.Parameters.AddWithValue("@PatientId", updatedAppointment.patientId);
                    command.Parameters.AddWithValue("@DoctorId", updatedAppointment.DoctorId);
                    command.Parameters.AddWithValue("@AppointmentDate", updatedAppointment.AppointmentDate);
                    command.Parameters.AddWithValue("@Description", updatedAppointment.Description);
                    command.Parameters.AddWithValue("@AppointmentId", updatedAppointment.AppointmentId);

                    connection.Open();

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Appointment updated successfully.");
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("Failed to update appointment.");
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new AppointmentNotFoundException(ex.Message);
            }
        }

        public bool scheduleAppointment(Appointment appointment)
        {
            throw new NotImplementedException();
        }
    }
    }

    
}
