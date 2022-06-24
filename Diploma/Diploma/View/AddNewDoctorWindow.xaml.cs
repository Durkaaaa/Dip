using Diploma.ViewModel;
using System.Windows;

namespace Diploma.View
{
    /// <summary>
    /// Логика взаимодействия для AddNewDoctorWindow.xaml
    /// </summary>
    public partial class AddNewDoctorWindow : Window
    {
        public AddNewDoctorWindow()
        {
            InitializeComponent();
            DataContext = new DataAddNewDoctorVM();
        }
    }
}
