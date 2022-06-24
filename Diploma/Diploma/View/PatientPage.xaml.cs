using Diploma.ViewModel;
using System.Windows.Controls;

namespace Diploma.View
{
    /// <summary>
    /// Логика взаимодействия для PatientPage.xaml
    /// </summary>
    public partial class PatientPage : Page
    {
        public static ListView PatientList { get; set; }
        public PatientPage()
        {
            InitializeComponent();
            DataContext = new DataPatientVM();
            PatientList = PatientListBlock;
        }
    }
}
