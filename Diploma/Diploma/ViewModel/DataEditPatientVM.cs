using Diploma.Command;
using Diploma.Model;
using System;
using System.Collections.Generic;
using System.Windows;

namespace Diploma.ViewModel
{
    public class DataEditPatientVM : ViewModelBase
    {
        public static string Surname { get; set; }
        public static string Name { get; set; }
        public static string Lastname { get; set; }
        public static Gender SelectedGender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public static string Policy { get; set; }
        public static string Snils { get; set; }
        public static string PassportSeries { get; set; }
        public static string PassportNumber { get; set; }
        public static string Address { get; set; }
        public static int IndexGender { get; set; }
        public static Patient SelectedPatient { get; set; }

        public DataEditPatientVM(Patient selectedPatient)
        {
            SelectedPatient = selectedPatient;
            Surname = SelectedPatient.Surname;
            Name = SelectedPatient.Name;
            Lastname = SelectedPatient.Lastname;
            SelectedGender = SelectedPatient.Gender;
            DateOfBirth = SelectedPatient.DateOfBirth;
            Policy = SelectedPatient.Policy;
            Snils = SelectedPatient.Snils;
            PassportSeries = SelectedPatient.PassportSeries;
            PassportNumber = SelectedPatient.PassportNumber;
            Address = SelectedPatient.Address;
            IndexGender = DataWorker.GetIndexGender(SelectedPatient.GenderId);
        }

        private List<Gender> _allGender = DataWorker.GetAllGender();
        public List<Gender> AllGender
        {
            get { return _allGender; }
            set
            {
                _allGender = value;
                NotifyPropertyChanged("AllGender");
            }
        }

        public RelayCommand EditPatient
        {
            get
            {
                return null ?? new RelayCommand(obj =>
                {
                    Window window = obj as Window;
                    if (SelectedPatient != null)
                    {
                        if (Surname == null || Surname.Replace(" ", "").Length == 0 ||
                            Name == null || Name.Replace(" ", "").Length == 0 ||
                            Lastname == null || Lastname.Replace(" ", "").Length == 0 ||
                            SelectedGender == null ||
                            DateOfBirth > DateTime.Now ||
                            Policy == null || Policy.Replace(" ", "").Length == 0 ||
                            Snils == null || Snils.Replace(" ", "").Length == 0 ||
                            PassportSeries == null || PassportSeries.Replace(" ", "").Length == 0 ||
                            PassportNumber == null || PassportNumber.Replace(" ", "").Length == 0 ||
                            Address == null || Address.Replace(" ", "").Length == 0)
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

                            if (SelectedGender == null)
                                ShowMessageToUser("Не выбран пол");

                            if (DateOfBirth > DateTime.Now)
                                ShowMessageToUser("Не правильная дата");

                            if (Policy == null || Policy.Replace(" ", "").Length == 0)
                                SetRedBlockControll(window, "PolicyBlock");
                            else
                                SetBlackBlockControll(window, "PolicyBlock");

                            if (Snils == null || Snils.Replace(" ", "").Length == 0)
                                SetRedBlockControll(window, "SnilsBlock");
                            else
                                SetBlackBlockControll(window, "SnilsBlock");

                            if (PassportSeries == null || PassportSeries.Replace(" ", "").Length == 0)
                                SetRedBlockControll(window, "PassportSeriesBlock");
                            else
                                SetBlackBlockControll(window, "PassportSeriesBlock");

                            if (PassportNumber == null || PassportNumber.Replace(" ", "").Length == 0)
                                SetRedBlockControll(window, "PassportNumberBlock");
                            else
                                SetBlackBlockControll(window, "PassportNumberBlock");

                            if (Address == null || Address.Replace(" ", "").Length == 0)
                                SetRedBlockControll(window, "AddressBlock");
                            else
                                SetBlackBlockControll(window, "AddressBlock");
                        }
                        else
                        {
                            SetBlackBlockControll(window, "SurnameBlock");
                            SetBlackBlockControll(window, "NameBlock");
                            SetBlackBlockControll(window, "LastnameBlock");
                            SetBlackBlockControll(window, "PolicyBlock");
                            SetBlackBlockControll(window, "SnilsBlock");
                            SetBlackBlockControll(window, "PassportSeriesBlock");
                            SetBlackBlockControll(window, "PassportNumberBlock");
                            SetBlackBlockControll(window, "AddressBlock");
                            var result = DataWorker.EditPatient(SelectedPatient, Surname, Name,
                                Lastname, SelectedGender, DateOfBirth, Policy, Snils, PassportSeries,
                                PassportNumber, Address);
                            ShowMessageToUser(result);
                            Zeroing();
                            window.Close();
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
            SelectedGender = null;
            DateOfBirth = DateTime.Now;
            Policy = null;
            Snils = null;
            PassportSeries = null;
            PassportNumber = null;
            Address = null;
            SelectedPatient = null;
            IndexGender = 0;
        }
    }
}
