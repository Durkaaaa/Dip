using Diploma.Model;
using Diploma.ViewModel;
using System;
using System.Collections.Generic;
using System.Windows;

namespace Diploma.View
{
    /// <summary>
    /// Логика взаимодействия для EditTicketPatientDoctorWindow.xaml
    /// </summary>
    public partial class EditTicketPatientDoctorWindow : Window
    {
        public EditTicketPatientDoctorWindow(Ticket selectedTicket, List<Patient> patientList,
            List<Doctor> doctorList, DateTime date, ReceptionHour selectedReceptionHour, Window window)
        {
            InitializeComponent();
            window.Close();
            DataContext = new DataEditTicketPatientDoctorVM(selectedTicket, patientList, doctorList, date, selectedReceptionHour);
        }
    }
}
