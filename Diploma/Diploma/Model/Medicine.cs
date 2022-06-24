namespace Diploma.Model
{
    public class Medicine
    {
        public int Id { get; set; }
        public int MedicalRecordId { get; set; }
        public virtual MedicalRecord MedicalRecord { get; set; }
        public string Titl { get; set; }
    }
}
