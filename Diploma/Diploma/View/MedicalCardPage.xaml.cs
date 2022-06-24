using Diploma.Model;
using Diploma.ViewModel;
using System.Windows.Controls;

namespace Diploma.View
{
    /// <summary>
    /// Логика взаимодействия для MedicalCardPage.xaml
    /// </summary>
    public partial class MedicalCardPage : Page
    {
        public static TextBlock PatientSurnameCard;
        public static TextBlock PatientNameCard;
        public static TextBlock PatientLastnameCard;
        public static TextBlock PatientDateOfBirthCard;
        public static TextBlock PatientGenderCard;
        public static TextBlock DoctorSurnameCard;
        public static TextBlock DoctorNameCard;
        public static TextBlock DoctorLastnameCard;
        public static TextBlock DoctorSpecialityCard;
        public static TextBlock StartOfTreatmentCard;
        public static TextBlock EndOfTreatmentCard;
        public static TextBlock DiagnosisCard;
        public static TextBox AEMedicine;
        public static ListView MedicineList;
        public MedicalCardPage(Patient selectedPatient)
        {
            InitializeComponent();
            DataContext = new DataMedicalCardVM(selectedPatient);
            MedicineList = MedicineBlock;
            PatientSurnameCard = PatientSurnameBlock;
            PatientNameCard = PatientNameBlock;
            PatientLastnameCard = PatientLastnameBlock;
            PatientDateOfBirthCard = PatientDateOfBirthBlock;
            PatientGenderCard = PatientGenderBlock;
            DoctorSurnameCard = DoctorSurnameBlock;
            DoctorNameCard = DoctorNameBlock;
            DoctorLastnameCard = DoctorLastnameBlock;
            DoctorSpecialityCard = DoctorSpecialityBlock;
            StartOfTreatmentCard = StartOfTreatmentBlock;
            EndOfTreatmentCard = EndOfTreatmentBlock;
            DiagnosisCard = DiagnosisBlock;
            AEMedicine = AEMedicineBlock;
        }
    }
}
