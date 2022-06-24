using Diploma.ViewModel;
using System.Windows.Controls;

namespace Diploma.View
{
    /// <summary>
    /// Логика взаимодействия для DoctorPage.xaml
    /// </summary>
    public partial class DoctorPage : Page
    {
        public static ListView DoctorList { get; set; }
        public DoctorPage()
        {
            InitializeComponent();
            DataContext = new DataDoctorVM();
            DoctorList = DoctorListBlock;
        }
    }
}
