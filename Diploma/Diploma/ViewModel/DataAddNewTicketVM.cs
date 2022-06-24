using Diploma.Command;
using Diploma.Model;
using Diploma.View;
using System;
using System.Collections.Generic;
using System.Windows;

namespace Diploma.ViewModel
{
    public class DataAddNewTicketVM : ViewModelBase
    {
        public static DateTime Date { get; set; }
        public static ReceptionHour SelectedReceptionHour { get; set; }
        public static Speciality SelectedSpeciality { get; set; }

        public DataAddNewTicketVM()
        {
            Date = DateTime.Now;
            _allSpeciality = DataWorker.GetAllSpeciality();
        }

        private List<ReceptionHour> _allReceptionHour = DataWorker.GetAllReceptionHour();
        public List<ReceptionHour> AllReceptionHour
        {
            get { return _allReceptionHour; }
            set
            {
                _allReceptionHour = value;
                NotifyPropertyChanged("AllReceptionHour");
            }
        }

        private List<Speciality> _allSpeciality;
        public List<Speciality> AllSpeciality
        {
            get { return _allSpeciality; }
            set
            {
                _allSpeciality = value;
                NotifyPropertyChanged("AllSpeciality");
            }
        }

        public RelayCommand AddNewTicketDateTime
        {
            get
            {
                return null ?? new RelayCommand(obj =>
                {
                    Window window = obj as Window;
                    DateTime dateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0).AddDays(-1);
                    DateTime dateTime1 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0).AddDays(7);
                    if (Date <= dateTime ||
                        Date >= dateTime1 ||
                        SelectedSpeciality == null ||
                        SelectedReceptionHour == null)
                    {
                        if (Date <= dateTime || Date > dateTime1)
                            ShowMessageToUser("Не правильная дата");

                        if (SelectedReceptionHour == null)
                            ShowMessageToUser("Не выбрано время");

                        if (SelectedSpeciality == null)
                            ShowMessageToUser("Не выбрана специальность");
                    }
                    else
                    {
                        var patient = DataWorker.GetPatientForTicket(Date, SelectedReceptionHour);
                        var doctor = DataWorker.GetDoctorForTicket(Date, SelectedReceptionHour, SelectedSpeciality);
                        if (doctor.Count == 0)
                        {
                            ShowMessageToUser("На эту дату и время свободных врачей нет");
                        }
                        else
                        {
                            AddNewTicketPatientDoctorWindow addNewTicketPatientDoctorWindow = new AddNewTicketPatientDoctorWindow(patient, doctor, Date, SelectedReceptionHour, window);
                            SetCenterPositionAndOpen(addNewTicketPatientDoctorWindow);
                            SelectedSpeciality = null;
                            SelectedReceptionHour = null;
                        }
                    }
                });
            }
        }
    }
}
