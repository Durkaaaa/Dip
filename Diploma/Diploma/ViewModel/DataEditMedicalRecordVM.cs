using Diploma.Model;
using Diploma.Command;
using System;
using System.Collections.Generic;
using System.Windows;

namespace Diploma.ViewModel
{
    public class DataEditMedicalRecordVM : ViewModelBase
    {
        public static MedicalRecord SelectedMedicalRecord { get; set; }
        public static MedicalCard SelectedMedicalCard { get; set; }
        public static Doctor SelectedDoctor { get; set; }
        public static string Diagnosis { get; set; }
        public static DateTime StartOfTreatment { get; set; }
        public static DateTime? EndOfTreatment { get; set; }
        public static int IndexDoctor { get; set; }

        public DataEditMedicalRecordVM(MedicalCard selectedMedicalCard, MedicalRecord selectedMedicalRecord)
        {
            SelectedMedicalCard = selectedMedicalCard;
            SelectedMedicalRecord = selectedMedicalRecord;
            Diagnosis = SelectedMedicalRecord.Diagnosis;
            StartOfTreatment = SelectedMedicalRecord.StartOfTreatment;
            EndOfTreatment = SelectedMedicalRecord.EndOfTreatment;
            IndexDoctor = DataWorker.GetIndexDoctor(SelectedMedicalRecord.DoctorId);
        }

        private List<Doctor> _allDoctor = DataWorker.GetAllDoctor();
        public List<Doctor> AllDoctor
        {
            get { return _allDoctor; }
            set
            {
                _allDoctor = value;
                NotifyPropertyChanged("AllDoctor");
            }
        }

        public RelayCommand EditMedicalRecord
        {
            get
            {
                return null ?? new RelayCommand(obj =>
                {
                    Window window = obj as Window;
                    if (SelectedMedicalCard != null || SelectedMedicalRecord != null)
                    {
                        if (EndOfTreatment != null)
                        {
                            if (EndOfTreatment <= StartOfTreatment)
                            {
                                ShowMessageToUser("Не правильная дата");
                            }
                        }

                        if (SelectedDoctor == null ||
                            Diagnosis == null || Diagnosis.Replace(" ", "").Length == 0 ||
                            (EndOfTreatment != null && EndOfTreatment <= StartOfTreatment))
                        {
                            if (SelectedDoctor == null)
                                ShowMessageToUser("Не выбран доктор");

                            if (Diagnosis == null || Diagnosis.Replace(" ", "").Length == 0)
                                SetRedBlockControll(window, "DiagnosisBlock");
                            else
                                SetBlackBlockControll(window, "DiagnosisBlock");

                            if (EndOfTreatment != null && EndOfTreatment <= StartOfTreatment)
                                ShowMessageToUser("Не правильная дата");
                        }
                        else
                        {
                            SetBlackBlockControll(window, "DiagnosisBlock");
                            DataWorker.EditMedicalRecord(SelectedMedicalRecord, SelectedMedicalCard, SelectedDoctor,
                                Diagnosis, StartOfTreatment, EndOfTreatment);
                            DataMedicalCardVM.AllMedicalRecord = DataWorker.GetMedicalRecordByMedicalСardId(SelectedMedicalCard);
                            SelectedMedicalCard = null;
                            SelectedDoctor = null;
                            Diagnosis = null;
                            StartOfTreatment = DateTime.Now;
                            EndOfTreatment = null;
                            SelectedMedicalRecord = null;
                            IndexDoctor = 0;
                            window.Close();
                        }
                    }
                });
            }
        }
    }
}
