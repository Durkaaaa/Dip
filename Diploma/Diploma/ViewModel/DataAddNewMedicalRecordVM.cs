using System;
using System.Collections.Generic;
using Diploma.Model;
using Diploma.Command;
using System.Windows;

namespace Diploma.ViewModel
{
    public class DataAddNewMedicalRecordVM : ViewModelBase
    {
        public static MedicalCard SelectedMedicalCard { get; set; }
        public static Doctor SelectedDoctor { get; set; }
        public static string Diagnosis { get; set; }
        public static DateTime StartOfTreatment { get; set; }
        public static DateTime? EndOfTreatment { get; set; }

        public DataAddNewMedicalRecordVM(MedicalCard selectedMedicalCard)
        {
            SelectedMedicalCard = selectedMedicalCard;
            StartOfTreatment = DateTime.Now;
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

        public RelayCommand AddNewMedicalRecord
        {
            get
            {
                return null ?? new RelayCommand(obj =>
                {
                    Window window = obj as Window;
                    if (SelectedMedicalCard != null)
                    {
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
                            DataWorker.AddNewMedicalRecord(SelectedMedicalCard, SelectedDoctor,
                                Diagnosis, StartOfTreatment, EndOfTreatment);
                            DataMedicalCardVM.AllMedicalRecord = DataWorker.GetMedicalRecordByMedicalСardId(SelectedMedicalCard);
                            SelectedMedicalCard = null;
                            SelectedDoctor = null;
                            Diagnosis = null;
                            StartOfTreatment = DateTime.Now;
                            EndOfTreatment = null;
                            window.Close();
                        }
                    }
                });
            }
        }
    }
}