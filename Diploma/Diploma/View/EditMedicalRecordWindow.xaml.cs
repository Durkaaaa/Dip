using Diploma.Model;
using Diploma.ViewModel;
using System.Windows;

namespace Diploma.View
{
    /// <summary>
    /// Логика взаимодействия для EditMedicalRecordWindow.xaml
    /// </summary>
    public partial class EditMedicalRecordWindow : Window
    {
        public EditMedicalRecordWindow(MedicalCard selectedMedicalCard, MedicalRecord selectedMedicalRecord)
        {
            InitializeComponent();
            DataContext = new DataEditMedicalRecordVM(selectedMedicalCard, selectedMedicalRecord);
        }
    }
}
