using Diploma.Command;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Diploma.Model
{
    public class Doctor
    {
        public int Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public int SpecialityId { get; set; }
        public virtual Speciality Speciality { get; set; }
        public int CabinetId { get; set; }
        public virtual Cabinet Cabinet { get; set; }
        public DateTime DateOfEmployment { get; set; }
        public DateTime WorkWith { get; set; }
        public DateTime WorkUntil { get; set; }

        [NotMapped]
        public Speciality DoctorSpeciality
        {
            get
            {
                return DataWorker.GetSpecialityById(SpecialityId);
            }
        }

        [NotMapped]
        public Cabinet DoctorCabinet
        {
            get
            {
                return DataWorker.GetCabinetById(CabinetId);
            }
        }
    }
}
