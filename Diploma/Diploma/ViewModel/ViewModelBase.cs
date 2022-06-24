using Diploma.Command;
using Diploma.Model;
using Diploma.View;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Diploma.ViewModel
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        private List<Ticket> _listTicket;
        public List<Ticket> ListTicket
        {
            get => _listTicket;
            set
            {
                _listTicket = value;
                NotifyPropertyChanged(nameof(ListTicket));
            }
        }

        private List<Doctor> _listDoctor;
        public List<Doctor> ListDoctor
        {
            get => _listDoctor;
            set
            {
                _listDoctor = value;
                NotifyPropertyChanged(nameof(ListDoctor));
            }
        }

        private List<Patient> _listPatient;
        public List<Patient> ListPatient
        {
            get => _listPatient;
            set
            {
                _listPatient = value;
                NotifyPropertyChanged(nameof(ListPatient));
            }
        }

        public void UpdateAllTicketPage()
        {
            ListTicket = DataWorker.GetAllTicket();
            TicketPage.TicketList.ItemsSource = null;
            TicketPage.TicketList.Items.Clear();
            TicketPage.TicketList.ItemsSource = ListTicket;
            TicketPage.TicketList.Items.Refresh();
        }

        public void UpdateAllDoctorPage()
        {
            ListDoctor = DataWorker.GetAllDoctor();
            DoctorPage.DoctorList.ItemsSource = null;
            DoctorPage.DoctorList.Items.Clear();
            DoctorPage.DoctorList.ItemsSource = ListDoctor;
            DoctorPage.DoctorList.Items.Refresh();
        }

        public void UpdateAllPatientPage()
        {
            ListPatient = DataWorker.GetAllPatient();
            PatientPage.PatientList.ItemsSource = null;
            PatientPage.PatientList.Items.Clear();
            PatientPage.PatientList.ItemsSource = ListPatient;
            PatientPage.PatientList.Items.Refresh();
        }

        #region [Изменение цвета TextBox]
        //Изменение цвета TextBox на красный
        public void SetRedBlockControll(Window window, string blockName)
        {
            Control block = window.FindName(blockName) as Control;
            block.BorderBrush = Brushes.Red;
        }

        //Изменение цвета TextBox на черный
        public void SetBlackBlockControll(Page page, string blockName)
        {
            Control block = page.FindName(blockName) as Control;
            block.BorderBrush = Brushes.Black;
        }

        //Изменение цвета TextBox на красный
        public void SetRedBlockControll(Page page, string blockName)
        {
            Control block = page.FindName(blockName) as Control;
            block.BorderBrush = Brushes.Red;
        }

        //Изменение цвета TextBox на черный
        public void SetBlackBlockControll(Window window, string blockName)
        {
            Control block = window.FindName(blockName) as Control;
            block.BorderBrush = Brushes.Black;
        }
        #endregion

        //Окно с сообщением для пользователя
        public void ShowMessageToUser(string message)
        {
            MessageWindow messageWindow = new MessageWindow(message);
            SetCenterPositionAndOpen(messageWindow);
        }

        //Положение окна при открытии
        public void SetCenterPositionAndOpen(Window window)
        {
            window.Owner = Application.Current.MainWindow;
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            window.ShowDialog();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
