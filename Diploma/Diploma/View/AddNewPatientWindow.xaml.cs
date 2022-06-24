using Diploma.ViewModel;
using System.Windows;

namespace Diploma.View
{
    /// <summary>
    /// Логика взаимодействия для AddNewPatientWindow.xaml
    /// </summary>
    public partial class AddNewPatientWindow : Window
    {
        public AddNewPatientWindow()
        {
            InitializeComponent();
            DataContext = new DataAddNewPatientVM();
        }
    }
}
