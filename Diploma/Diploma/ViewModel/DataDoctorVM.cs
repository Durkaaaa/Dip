using Diploma.Command;
using Diploma.Model;
using Diploma.View;
using System.Collections.Generic;

namespace Diploma.ViewModel
{
    public class DataDoctorVM : ViewModelBase
    {
        public static Doctor SelectedDoctor { get; set; }


        private List<Doctor> _allDoctor = DataWorker.GetAllDoctor();
        public List<Doctor> AllDoctor
        {
            get => _allDoctor;
            set
            {
                _allDoctor = value;
                NotifyPropertyChanged(nameof(AllDoctor));
            }
        }

        public RelayCommand AddNewDoctor
        {
            get
            {
                return null ?? new RelayCommand(obj =>
                {
                    AddNewDoctorWindow addNewDoctorWindow = new AddNewDoctorWindow();
                    SetCenterPositionAndOpen(addNewDoctorWindow);
                    UpdateAllDoctorPage();
                });
            }
        }

        public RelayCommand EditDoctor
        {
            get
            {
                return null ?? new RelayCommand(obj =>
                {
                    if (SelectedDoctor != null)
                    {
                        EditDoctorWindow editDoctorWindow = new EditDoctorWindow(SelectedDoctor);
                        SetCenterPositionAndOpen(editDoctorWindow);
                        SelectedDoctor = null;
                        UpdateAllDoctorPage();
                    }
                });
            }
        }

        public RelayCommand DeleteDoctor
        {
            get
            {
                return null ?? new RelayCommand(obj =>
                {
                    if (SelectedDoctor != null)
                    {
                        var result = DataWorker.DeleteDoctor(SelectedDoctor);
                        ShowMessageToUser(result);
                        SelectedDoctor = null;
                        UpdateAllDoctorPage();
                    }
                });
            }
        }
    }
}
