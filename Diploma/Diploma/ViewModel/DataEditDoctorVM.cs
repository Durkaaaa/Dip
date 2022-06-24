using Diploma.Command;
using Diploma.Model;
using System;
using System.Collections.Generic;
using System.Windows;

namespace Diploma.ViewModel
{
    public class DataEditDoctorVM : ViewModelBase
    {
        public static string Surname { get; set; }
        public static string Name { get; set; }
        public static string Lastname { get; set; }
        public static Speciality SelectedSpeciality { get; set; }
        public static Cabinet SelectedCabinet { get; set; }
        public static DateTime DateOfEmployment { get; set; }
        public static int WorkWithHour { get; set; }
        public static int WorkWithMinute { get; set; }
        public static int WorkUntilHour { get; set; }
        public static int WorkUntilMinute { get; set; }
        public static int IndexSpeciality { get; set; }
        public static int IndexCabinet { get; set; }
        public static Doctor SelectedDoctor { get; set; }

        public DataEditDoctorVM(Doctor selectedDoctor)
        {
            SelectedDoctor = selectedDoctor;
            _allCabinet = DataWorker.GetAllCabinet(SelectedDoctor);
            Surname = SelectedDoctor.Surname;
            Name = SelectedDoctor.Name;
            Lastname = SelectedDoctor.Lastname;
            SelectedSpeciality = SelectedDoctor.Speciality;
            SelectedCabinet = SelectedDoctor.Cabinet;
            DateOfEmployment = SelectedDoctor.DateOfEmployment;
            WorkWithHour = SelectedDoctor.WorkWith.Hour;
            WorkWithMinute = SelectedDoctor.WorkWith.Minute;
            WorkUntilHour = SelectedDoctor.WorkUntil.Hour;
            WorkUntilMinute = SelectedDoctor.WorkUntil.Minute;
            IndexSpeciality = DataWorker.GetIndexSpeciality(SelectedDoctor.SpecialityId);
            IndexCabinet = DataWorker.GetIndexCabinet(SelectedDoctor.CabinetId, _allCabinet);
        }

        private List<Speciality> _allSpeciality = DataWorker.GetAllSpeciality();
        public List<Speciality> AllSpeciality
        {
            get { return _allSpeciality; }
            set
            {
                _allSpeciality = value;
                NotifyPropertyChanged("AllSpeciality");
            }
        }

        private List<Cabinet> _allCabinet;
        public List<Cabinet> AllCabinet
        {
            get { return _allCabinet; }
            set
            {
                _allCabinet = value;
                NotifyPropertyChanged("AllCabinet");
            }
        }

        public RelayCommand EditDoctor
        {
            get
            {
                return null ?? new RelayCommand(obj =>
                {
                    Window window = obj as Window;
                    if (SelectedDoctor != null)
                    {
                        if (Surname == null || Surname.Replace(" ", "").Length == 0 ||
                            Name == null || Name.Replace(" ", "").Length == 0 ||
                            Lastname == null || Lastname.Replace(" ", "").Length == 0 ||
                            SelectedSpeciality == null ||
                            SelectedCabinet == null ||
                            DateOfEmployment > DateTime.Now ||
                            WorkWithHour >= 24 ||
                            WorkWithHour < 0 ||
                            WorkWithMinute >= 60 ||
                            WorkWithMinute < 0 ||
                            WorkUntilHour >= 24 ||
                            WorkUntilHour < 0 ||
                            WorkUntilMinute >= 60 ||
                            WorkUntilMinute < 0)
                        {
                            if (Surname == null || Surname.Replace(" ", "").Length == 0)
                                SetRedBlockControll(window, "SurnameBlock");
                            else
                                SetBlackBlockControll(window, "SurnameBlock");

                            if (Name == null || Name.Replace(" ", "").Length == 0)
                                SetRedBlockControll(window, "NameBlock");
                            else
                                SetBlackBlockControll(window, "NameBlock");

                            if (Lastname == null || Lastname.Replace(" ", "").Length == 0)
                                SetRedBlockControll(window, "LastnameBlock");
                            else
                                SetBlackBlockControll(window, "LastnameBlock");

                            if (SelectedSpeciality == null)
                                ShowMessageToUser("Не выбрана специальность");

                            if (SelectedCabinet == null)
                                ShowMessageToUser("Не выбран кабинет");

                            if (DateOfEmployment > DateTime.Now)
                                ShowMessageToUser("Неправильная дата");

                            if (WorkWithHour >= 24)
                                SetRedBlockControll(window, "WorkWithHourBlock");
                            else
                                SetBlackBlockControll(window, "WorkWithHourBlock");

                            if (WorkWithMinute >= 60)
                                SetRedBlockControll(window, "WorkWithMinuteBlock");
                            else
                                SetBlackBlockControll(window, "WorkWithMinuteBlock");

                            if (WorkUntilHour >= 24)
                                SetRedBlockControll(window, "WorkUntilHourBlock");
                            else
                                SetBlackBlockControll(window, "WorkUntilHourBlock");

                            if (WorkUntilMinute >= 60)
                                SetRedBlockControll(window, "WorkUntilMinuteBlock");
                            else
                                SetBlackBlockControll(window, "WorkUntilMinuteBlock");
                        }
                        else
                        {
                            var workWith = new DateTime(1, 1, 1, WorkWithHour, WorkWithMinute, 00);
                            var workUntil = new DateTime(1, 1, 1, WorkUntilHour, WorkUntilMinute, 00);

                            if (workWith >= workUntil)
                            {
                                ShowMessageToUser("Не правильное время работы");
                                SetRedBlockControll(window, "WorkWithHourBlock");
                                SetRedBlockControll(window, "WorkWithMinuteBlock");
                                SetRedBlockControll(window, "WorkUntilHourBlock");
                                SetRedBlockControll(window, "WorkUntilMinuteBlock");
                            }
                            else
                            {
                                SetBlackBlockControll(window, "SurnameBlock");
                                SetBlackBlockControll(window, "NameBlock");
                                SetBlackBlockControll(window, "LastnameBlock");
                                SetBlackBlockControll(window, "WorkWithHourBlock");
                                SetBlackBlockControll(window, "WorkWithMinuteBlock");
                                SetBlackBlockControll(window, "WorkUntilHourBlock");
                                SetBlackBlockControll(window, "WorkUntilMinuteBlock");
                                var result = DataWorker.EditDoctor(SelectedDoctor, Surname, Name, Lastname,
                                SelectedSpeciality, SelectedCabinet, DateOfEmployment, workWith, workUntil);
                                ShowMessageToUser(result);
                                Zeroing();
                                window.Close();
                            }
                        }
                    }
                });
            }
        }

        private void Zeroing()
        {
            Surname = null;
            Name = null;
            Lastname = null;
            SelectedSpeciality = null;
            SelectedCabinet = null;
            DateOfEmployment = DateTime.Now;
            WorkWithHour = 0;
            WorkWithMinute = 0;
            WorkUntilHour = 0;
            WorkUntilMinute = 0;
        }
    }
}
