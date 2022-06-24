using Diploma.Command;
using Diploma.Model;
using Diploma.View;
using System.Collections.Generic;

namespace Diploma.ViewModel
{
    public class DataPatientVM : ViewModelBase
    {
        public static Patient SelectedPatient { get; set; }

        public DataPatientVM()
        {
            _allPatient = DataWorker.GetAllPatient();
        }

        private List<Patient> _allPatient;
        public List<Patient> AllPatient
        {
            get => _allPatient;
            set
            {
                _allPatient = value;
                NotifyPropertyChanged(nameof(AllPatient));
            }
        }

        public RelayCommand AddNewPatient
        {
            get
            {
                return null ?? new RelayCommand(obj =>
                {
                    AddNewPatientWindow addNewPatientWindow = new AddNewPatientWindow();
                    SetCenterPositionAndOpen(addNewPatientWindow);
                    UpdateAllPatientPage();
                });
            }
        }

        public RelayCommand EditPatient
        {
            get
            {
                return null ?? new RelayCommand(obj =>
                {
                    if (SelectedPatient != null)
                    {
                        EditPatientWindow editPatientWindow = new EditPatientWindow(SelectedPatient);
                        SetCenterPositionAndOpen(editPatientWindow);
                        SelectedPatient = null;
                        UpdateAllPatientPage();
                    }
                });
            }
        }

        public RelayCommand DeletePatient
        {
            get
            {
                return null ?? new RelayCommand(obj =>
                {
                    if (SelectedPatient != null)
                    {
                        var result = DataWorker.DeletePatient(SelectedPatient);
                        ShowMessageToUser(result);
                        SelectedPatient = null;
                        UpdateAllPatientPage();
                    }
                });
            }
        }

        public RelayCommand OpenMedicalCardPage
        {
            get
            {
                return null ?? new RelayCommand(obj =>
                {
                    if (SelectedPatient != null)
                    {
                        MedicalCardPage medicalCardPage = new MedicalCardPage(SelectedPatient);
                        MainWindow.FramePage.Content = medicalCardPage;
                    }
                });
            }
        }
    }
}
