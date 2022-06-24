using Diploma.Command;
using Diploma.Model;
using Diploma.View;
using System;
using System.Collections.Generic;
using System.Windows;

namespace Diploma.ViewModel
{
    public class DataAddNewTicketPatientDoctorVM : ViewModelBase
    {
        public static Patient SelectedPatient { get; set; }
        public static Doctor SelectedDoctor { get; set; }
        public static ReceptionHour SelectedReceptionHour { get; set; }
        public static DateTime Date { get; set; }

        public DataAddNewTicketPatientDoctorVM(List<Patient> patientList, List<Doctor> doctorList,
            DateTime date, ReceptionHour selectedReceptionHour)
        {
            _allPatient = patientList;
            _allDoctor = doctorList;
            SelectedReceptionHour = selectedReceptionHour;
            Date = date;
        }

        private List<Patient> _allPatient;
        public List<Patient> AllPatient
        {
            get { return _allPatient; }
            set
            {
                _allPatient = value;
                NotifyPropertyChanged("AllPatient");
            }
        }

        private List<Doctor> _allDoctor;
        public List<Doctor> AllDoctor
        {
            get { return _allDoctor; }
            set
            {
                _allDoctor = value;
                NotifyPropertyChanged("AllDoctor");
            }
        }

        public RelayCommand BackPage
        {
            get
            {
                return null ?? new RelayCommand(obj =>
                {
                    Window window = obj as Window;
                    AddNewTicketWindow addNewTicketWindow = new AddNewTicketWindow(window);
                    SetCenterPositionAndOpen(addNewTicketWindow);
                    SelectedDoctor = null;
                    SelectedPatient = null;
                    
                });
            }
        }

        public RelayCommand AddNewTicketPatientDoctor
        {
            get
            {
                return null ?? new RelayCommand(obj =>
                {
                    Window window = obj as Window;
                    if (SelectedPatient == null ||
                        SelectedDoctor == null)
                    {
                        if (SelectedDoctor == null)
                            ShowMessageToUser("Вы не выбрали доктора");

                        if (SelectedPatient == null)
                            ShowMessageToUser("Вы не выбрали пациента");
                    }
                    else
                    {
                        var result = DataWorker.AddNewTicket(SelectedPatient, SelectedDoctor,
                            Date, SelectedReceptionHour);
                        ShowMessageToUser(result);
                        SelectedPatient = null;
                        SelectedDoctor = null;
                        window.Close();
                    }
                });
            }
        }
    }
}
