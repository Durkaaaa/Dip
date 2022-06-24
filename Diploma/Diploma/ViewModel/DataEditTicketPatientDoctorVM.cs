using Diploma.Command;
using Diploma.Model;
using Diploma.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Diploma.ViewModel
{
    public class DataEditTicketPatientDoctorVM : ViewModelBase
    {
        public static Ticket SelectedTicket { get; set; }
        public static Patient SelectedPatient { get; set; }
        public static Doctor SelectedDoctor { get; set; }
        public static ReceptionHour SelectedReceptionHour { get; set; }
        public static DateTime Date { get; set; }
        public static int IndexPatient { get; set; }
        public static int IndexDoctor { get; set; }

        public DataEditTicketPatientDoctorVM(Ticket selectedTicket, List<Patient> patientList,
            List<Doctor> doctorList, DateTime date, ReceptionHour selectedReceptionHour)
        {
            SelectedTicket = selectedTicket;
            _allPatient = patientList;
            _allDoctor = doctorList;
            IndexDoctor = DataWorker.GetIndexDoctor(_allDoctor, SelectedTicket.DoctorId);
            IndexPatient = DataWorker.GetIndexPatient(_allPatient, SelectedTicket.PatientId);
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
                    EditTicketWindow editTicketWindow = new EditTicketWindow(SelectedTicket, window);
                    SelectedDoctor = null;
                    SelectedPatient = null;

                });
            }
        }

        public RelayCommand EditTicketPatientDoctor
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
                        var result = DataWorker.EditTicket(SelectedTicket, SelectedPatient, SelectedDoctor,
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
