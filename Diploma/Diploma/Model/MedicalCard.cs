using Diploma.Command;
using System.ComponentModel.DataAnnotations.Schema;

namespace Diploma.Model
{
    public class MedicalCard
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public virtual Patient Patient { get; set; }

        [NotMapped]
        public Patient MedicalСardPatient
        {
            get
            {
                return DataWorker.GetPatientById(PatientId);
            }
        }
    }
}
