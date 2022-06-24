using Diploma.Model;
using Diploma.ViewModel;
using System.Windows;

namespace Diploma.View
{
    /// <summary>
    /// Логика взаимодействия для EditDoctorWindow.xaml
    /// </summary>
    public partial class EditDoctorWindow : Window
    {
        public EditDoctorWindow(Doctor selectedDoctor)
        {
            InitializeComponent();
            DataContext = new DataEditDoctorVM(selectedDoctor);
        }
    }
}
