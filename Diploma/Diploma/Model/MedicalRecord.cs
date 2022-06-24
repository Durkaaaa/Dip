using Diploma.Command;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Diploma.Model
{
    public class MedicalRecord
    {
        public int Id { get; set; }
        public int MedicalСardId { get; set; }
        public virtual MedicalCard MedicalСard { get; set; }
        public int DoctorId { get; set; }
        public virtual Doctor Doctor { get; set; }
        public string Diagnosis { get; set; }
        public DateTime StartOfTreatment { get; set; }
        public DateTime? EndOfTreatment { get; set; }

        [NotMapped]
        public MedicalCard MedicalRecordsMedicalСard
        {
            get
            {
                return DataWorker.GetMedicalСardById(MedicalСardId);
            }
        }
        
        [NotMapped]
        public Doctor MedicalRecordsDoctor
        {
            get
            {
                return DataWorker.GetDoctorById(DoctorId);
            }
        }
    }
}
