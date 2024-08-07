using PatientService.Domain.Models;

namespace ConsultEase.PatientService.Core.Domain.Repository
{
    public class Appointment
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Reason { get; set; }

        // Navigation property to link with the Patient
        public Patient Patient { get; set; }

        // Business logic related to appointments can be added here.

        public void Reschedule(DateTime newDate)
        {
            // Ensure newDate is in the future
            AppointmentDate = newDate;
        }
    }
}