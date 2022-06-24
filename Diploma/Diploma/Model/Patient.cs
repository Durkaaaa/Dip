using Diploma.Command;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Diploma.Model
{
    public class Patient
    {
        public int Id { get; set; } 
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public int GenderId { get; set; }
        public virtual Gender Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Policy { get; set; }
        public string Snils { get; set; }
        public string PassportSeries { get; set; }
        public string PassportNumber { get; set; }
        public string Address { get; set; }

        [NotMapped]
        public Gender PatientGender
        {
            get
            {
                return DataWorker.GetGenderById(GenderId);
            }
        }
    }
}
