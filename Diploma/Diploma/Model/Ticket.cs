using Diploma.Command;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Diploma.Model
{
    public class Ticket
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public virtual Patient Patient { get; set; }
        public int DoctorId { get; set; }
        public virtual Doctor Doctor { get; set; }
        public DateTime Date { get; set; }
        public int ReceptionHourId { get; set; }
        public virtual ReceptionHour ReceptionHour { get; set; }

        [NotMapped]
        public Patient TicketPatient
        {
            get
            {
                return DataWorker.GetPatientById(PatientId);
            }
        }

        [NotMapped]
        public Doctor TicketDoctor
        {
            get
            {
                return DataWorker.GetDoctorById(DoctorId);
            }
        }

        [NotMapped]
        public ReceptionHour TicketReceptionHour
        {
            get
            {
                return DataWorker.GetReceptionHourById(ReceptionHourId);
            }
        }
    }
}
