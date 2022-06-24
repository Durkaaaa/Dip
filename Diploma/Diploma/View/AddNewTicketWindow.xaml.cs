using Diploma.ViewModel;
using System.Windows;

namespace Diploma.View
{
    /// <summary>
    /// Логика взаимодействия для AddNewTicketWindow.xaml
    /// </summary>
    public partial class AddNewTicketWindow : Window
    {
        public AddNewTicketWindow()
        {
            InitializeComponent();
            DataContext = new DataAddNewTicketVM();
        }

        public AddNewTicketWindow(Window window)
        {
            InitializeComponent();
            window.Close();
            DataContext = new DataAddNewTicketVM();
        }
    }
}
