using Diploma.Command;
using Diploma.Model;
using Diploma.View;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace Diploma.ViewModel
{
    public class DataMedicalCardVM : ViewModelBase
    {
        #region[Свойства для мед карты]
        public static List<Medicine> AllMedicine { get; set; }
        public static MedicalCard SelectedMedicalCard { get; set; }
        public static MedicalRecord SelectedMedicalRecord { get; set; }
        public static List<MedicalRecord> AllMedicalRecord { get; set; }

        public static string Diagnosis { get; set; }
        public static string StartOfTreatment { get; set; }
        public static string EndOfTreatment { get; set; }
        public static int IndexMedicalRecord { get; set; }
        #endregion

        #region[Свойства для пациента]
        public static Patient SelectedPatient { get; set; }
        public static string PatientSurname { get; set; }
        public static string PatientName { get; set; }
        public static string PatientLastname { get; set; }
        public static string PatientDateOfBirth { get; set; }
        public static string PatientGender { get; set; }
        #endregion

        #region[Свойства для врача]
        public static string DoctorSurname { get; set; }
        public static string DoctorName { get; set; }
        public static string DoctorLastname { get; set; }
        public static string DoctorSpeciality { get; set; }
        #endregion

        #region[Свойства для лекарств]        
        public static Medicine SelectedMedicine { get; set; }
        public static string AEMedicine { get; set; }
        #endregion

        public DataMedicalCardVM(Patient selectedPatient)
        {
            SelectedPatient = selectedPatient;
            IndexMedicalRecord = 0;
            PatientSurname = null;
            PatientName = null;
            PatientLastname = null;
            PatientDateOfBirth = null;
            PatientGender = null;
            DoctorSurname = null;
            DoctorName = null;
            DoctorLastname = null;
            DoctorSpeciality = null;
            StartOfTreatment = null;
            EndOfTreatment = null;
            Diagnosis = null;
            bool medicalCard = DataWorker.BoolGetMedicalСardByPatientId(SelectedPatient);
            if (!medicalCard)
            {
                SelectedMedicalCard = DataWorker.AddNewMedicalСard(SelectedPatient);
            }
            else
            {
                SelectedMedicalCard = DataWorker.GetMedicalСardByPatientId(SelectedPatient);

                bool medicalRecord = DataWorker.BoolGetMedicalRecordByMedicalСardId(SelectedMedicalCard);
                if (medicalRecord)
                {
                    AllMedicalRecord = DataWorker.GetMedicalRecordByMedicalСardId(SelectedMedicalCard);
                    SelectedMedicalRecord = AllMedicalRecord[IndexMedicalRecord];
                    bool boolMedicine = DataWorker.BoolGetAllMedicineByMedicanRecordId(SelectedMedicalRecord);
                    if (boolMedicine)
                    {
                        AllMedicine = DataWorker.GetAllMedicinesByMedicalRecordId(SelectedMedicalRecord);
                    }
                    MedicalRecordNumber();
                }
            }
        }

        #region[Лекарства]
        public RelayCommand AddNewMedicine
        {
            get
            {
                return null ?? new RelayCommand(obj =>
                {
                    Page page = obj as Page;
                    if (AEMedicine == null || AEMedicine.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControll(page, "AEMedicineBlock");
                    }
                    else
                    {
                        SetBlackBlockControll(page, "AEMedicineBlock");
                        DataWorker.AddNewMedicine(SelectedMedicalRecord, AEMedicine);
                        UpdateAllMedicalRecord();
                        AEMedicine = null;
                    }
                });
            }
        }

        public RelayCommand EditMedicine
        {
            get
            {
                return null ?? new RelayCommand(obj =>
                {
                    if (SelectedMedicine != null)
                    {
                        Page page = obj as Page;
                        MedicalCardPage.AEMedicine.Text = SelectedMedicine.Titl;
                        AEMedicine = SelectedMedicine.Titl;
                        DataWorker.DeleteMedicine(SelectedMedicine);
                        AllMedicine = DataWorker.GetAllMedicinesByMedicalRecordId(SelectedMedicalRecord);
                        MedicalCardPage.MedicineList.ItemsSource = AllMedicine;
                        MedicalCardPage.MedicineList.Items.Refresh();
                        SelectedMedicine = null;
                    }
                });
            }
        }

        public RelayCommand DeleteMedicine
        {
            get
            {
                return null ?? new RelayCommand(obj =>
                {
                    if (SelectedMedicine != null)
                    {
                        DataWorker.DeleteMedicine(SelectedMedicine);
                        UpdateAllMedicalRecord();
                        SelectedMedicine = null;
                    }
                });
            }
        }
        #endregion

        #region[Страницы в мед карте]
        public RelayCommand AddNewMedicalRecord
        {
            get
            {
                return null ?? new RelayCommand(obj =>
                {
                    AddNewMedicalRecordWindow addNewMedicalRecordWindow = new AddNewMedicalRecordWindow(SelectedMedicalCard);
                    SetCenterPositionAndOpen(addNewMedicalRecordWindow);
                    AllMedicalRecord = DataWorker.GetMedicalRecordByMedicalСardId(SelectedMedicalCard);
                    int count = AllMedicalRecord.Count();
                    if (AllMedicalRecord != null && count > IndexMedicalRecord + 1)
                    {
                        IndexMedicalRecord = count - 1;
                        MedicalRecordNumber();
                        UpdateAllMedicalRecord();
                    }
                    else
                    {
                        if (AllMedicalRecord != null && count > IndexMedicalRecord)
                        {
                            MedicalRecordNumber();
                            UpdateAllMedicalRecord();
                        }
                    }
                });
            }
        }

        public RelayCommand EditMedicalRecord
        {
            get
            {
                return null ?? new RelayCommand(obj =>
                {
                    if (SelectedMedicalRecord != null)
                    {
                        EditMedicalRecordWindow editMedicalRecordWindow = new EditMedicalRecordWindow(SelectedMedicalCard, SelectedMedicalRecord);
                        SetCenterPositionAndOpen(editMedicalRecordWindow);
                        MedicalRecordNumber();
                        UpdateAllMedicalRecord();
                    }
                });
            }
        }

        public RelayCommand DeleteMedicalRecord
        {
            get
            {
                return null ?? new RelayCommand(obj =>
                {
                    if (SelectedMedicalRecord != null)
                    {
                        var result = DataWorker.DeleteMedicalRecord(SelectedMedicalRecord);
                        ShowMessageToUser(result);
                        AllMedicalRecord = DataWorker.GetMedicalRecordByMedicalСardId(SelectedMedicalCard);
                        if (AllMedicalRecord != null && 0 <= IndexMedicalRecord - 1)
                        {
                            IndexMedicalRecord--;
                            MedicalRecordNumber();
                            UpdateAllMedicalRecord();
                        }
                        MedicalRecordNumber();
                    }
                });
            }
        }
        #endregion

        #region[Листание страниц]
        public RelayCommand OpenNextMedicalCard
        {
            get
            {
                return null ?? new RelayCommand(obj =>
                {
                    AllMedicalRecord = DataWorker.GetMedicalRecordByMedicalСardId(SelectedMedicalCard);
                    int count = AllMedicalRecord.Count();
                    if (AllMedicalRecord != null && count > IndexMedicalRecord + 1)
                    {
                        IndexMedicalRecord += 1;
                        MedicalRecordNumber();
                        UpdateAllMedicalRecord();
                    }
                });
            }
        }

        public RelayCommand OpenPreviousMedicalCard
        {
            get
            {
                return null ?? new RelayCommand(obj =>
                {
                    AllMedicalRecord = DataWorker.GetMedicalRecordByMedicalСardId(SelectedMedicalCard);
                    if (AllMedicalRecord != null && 0 <= IndexMedicalRecord - 1)
                    {
                        IndexMedicalRecord--;
                        MedicalRecordNumber();
                        UpdateAllMedicalRecord();
                    }
                });
            }
        }
        #endregion

        private void MedicalRecordNumber()
        {
            AllMedicalRecord = DataWorker.GetMedicalRecordByMedicalСardId(SelectedMedicalCard);
            if (AllMedicalRecord.Count() > 0)
            {
                SelectedMedicalRecord = AllMedicalRecord[IndexMedicalRecord];
            }
            else
            {
                SelectedMedicalRecord = null;
            }

            if (SelectedMedicalRecord != null)
            {
                bool boolMedicine = DataWorker.BoolGetAllMedicineByMedicanRecordId(SelectedMedicalRecord);
                if (boolMedicine)
                {
                    AllMedicine = DataWorker.GetAllMedicinesByMedicalRecordId(SelectedMedicalRecord);
                }

                var patient = DataWorker.GetPatientByMedicalRecord(SelectedMedicalRecord);
                var doctor = DataWorker.GetDoctorByMedicalRecord(SelectedMedicalRecord);
                if (patient != null && doctor != null)
                {
                    PatientSurname = patient.Surname;
                    PatientName = patient.Name;
                    PatientLastname = patient.Lastname;
                    PatientDateOfBirth = patient.DateOfBirth.ToShortDateString().ToString();
                    PatientGender = patient.PatientGender.Titl;
                    
                    DoctorSurname = doctor.Surname;
                    DoctorName = doctor.Name;
                    DoctorLastname = doctor.Lastname;
                    DoctorSpeciality = doctor.DoctorSpeciality.Titl;

                    Diagnosis = SelectedMedicalRecord.Diagnosis;
                    StartOfTreatment = SelectedMedicalRecord.StartOfTreatment.ToShortDateString().ToString();
                    Diagnosis = SelectedMedicalRecord.Diagnosis;
                    EndOfTreatment = SelectedMedicalRecord.EndOfTreatment.ToString();

                    if (EndOfTreatment != "")
                    {
                        EndOfTreatment = EndOfTreatment.Substring(0, 10);
                    }
                }                
            }
            else
            {
                PatientSurname = null;
                PatientName = null;
                PatientLastname = null;
                PatientDateOfBirth = null;
                PatientGender = null;
                DoctorSurname = null;
                DoctorName = null;
                DoctorLastname = null;
                DoctorSpeciality = null;
                StartOfTreatment = null;
                EndOfTreatment = null;
                Diagnosis = null;
                MedicalCardPage.PatientSurnameCard.Text = PatientSurname;
                MedicalCardPage.PatientNameCard.Text = PatientName;
                MedicalCardPage.PatientLastnameCard.Text = PatientLastname;
                MedicalCardPage.PatientDateOfBirthCard.Text = PatientDateOfBirth;
                MedicalCardPage.PatientGenderCard.Text = PatientGender;
                MedicalCardPage.DoctorSurnameCard.Text = DoctorSurname;
                MedicalCardPage.DoctorNameCard.Text = DoctorName;
                MedicalCardPage.DoctorLastnameCard.Text = DoctorLastname;
                MedicalCardPage.DoctorSpecialityCard.Text = DoctorSpeciality;
                MedicalCardPage.StartOfTreatmentCard.Text = StartOfTreatment;
                MedicalCardPage.EndOfTreatmentCard.Text = EndOfTreatment;
                MedicalCardPage.DiagnosisCard.Text = Diagnosis;
                MedicalCardPage.MedicineList.ItemsSource = null;
                MedicalCardPage.MedicineList.Items.Refresh();
                MedicalCardPage.AEMedicine.Text = null;
            }
        }

        private void UpdateAllMedicalRecord()
        {
            MedicalCardPage.PatientSurnameCard.Text = PatientSurname;
            MedicalCardPage.PatientNameCard.Text = PatientName;
            MedicalCardPage.PatientLastnameCard.Text = PatientLastname;
            MedicalCardPage.PatientDateOfBirthCard.Text = PatientDateOfBirth;
            MedicalCardPage.PatientGenderCard.Text = PatientGender;
            MedicalCardPage.DoctorSurnameCard.Text = DoctorSurname;
            MedicalCardPage.DoctorNameCard.Text = DoctorName;
            MedicalCardPage.DoctorLastnameCard.Text = DoctorLastname;
            MedicalCardPage.DoctorSpecialityCard.Text = DoctorSpeciality;
            MedicalCardPage.StartOfTreatmentCard.Text = StartOfTreatment;
            MedicalCardPage.EndOfTreatmentCard.Text = EndOfTreatment;
            MedicalCardPage.DiagnosisCard.Text = Diagnosis;
            AllMedicine = DataWorker.GetAllMedicinesByMedicalRecordId(SelectedMedicalRecord);
            MedicalCardPage.MedicineList.ItemsSource = AllMedicine;
            MedicalCardPage.MedicineList.Items.Refresh();
            MedicalCardPage.AEMedicine.Text = null;
        }
    }
}
