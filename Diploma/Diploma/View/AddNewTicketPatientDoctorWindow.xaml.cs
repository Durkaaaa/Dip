using Diploma.Model;
using Diploma.ViewModel;
using System;
using System.Collections.Generic;
using System.Windows;

namespace Diploma.View
{
    /// <summary>
    /// Логика взаимодействия для AddNewTicketPatientDoctorWindow.xaml
    /// </summary>
    public partial class AddNewTicketPatientDoctorWindow : Window
    {
        public AddNewTicketPatientDoctorWindow(List<Patient> patientList, List<Doctor> doctorList,
            DateTime date, ReceptionHour selectedReceptionHour, Window window)
        {
            InitializeComponent();
            window.Close();
            DataContext = new DataAddNewTicketPatientDoctorVM(patientList, doctorList, date, selectedReceptionHour);
        }
    }
}
