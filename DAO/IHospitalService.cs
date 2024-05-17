using HosManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HosManagement.DAO
{
   public interface IHospitalService
    {
        
            //List<Appointment> GetAppoinmentList();
            Appointment GetAppointmentById(int appointmentId);

             bool UpdateAppointment(Appointment appointment);

             bool CancelAppointment(int appointmentID);
             bool scheduleAppointment(Appointment appointment);

        List<Appointment> getAppointmentsForPatient(int appointmentId);




    }
    }


