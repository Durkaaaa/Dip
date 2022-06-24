using Diploma.Model;
using Diploma.ViewModel;
using System.Windows;

namespace Diploma.View
{
    /// <summary>
    /// Логика взаимодействия для EditPatientWindow.xaml
    /// </summary>
    public partial class EditPatientWindow : Window
    {
        public EditPatientWindow(Patient selectedPatient) 
        {
            InitializeComponent();
            DataContext = new DataEditPatientVM(selectedPatient);
        }
    }
}
