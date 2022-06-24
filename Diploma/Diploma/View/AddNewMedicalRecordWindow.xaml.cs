using Diploma.Model;
using Diploma.ViewModel;
using System.Windows;

namespace Diploma.View
{
    /// <summary>
    /// Логика взаимодействия для AddNewMedicalRecordWindow.xaml
    /// </summary>
    public partial class AddNewMedicalRecordWindow : Window
    {
        public AddNewMedicalRecordWindow(MedicalCard selectedMedicalCard)
        {
            InitializeComponent();
            DataContext = new DataAddNewMedicalRecordVM(selectedMedicalCard);
        }
    }
}
