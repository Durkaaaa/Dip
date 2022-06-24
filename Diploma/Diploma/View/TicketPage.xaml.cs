using Diploma.ViewModel;
using System.Windows.Controls;

namespace Diploma.View
{
    /// <summary>
    /// Логика взаимодействия для TicketPage.xaml
    /// </summary>
    public partial class TicketPage : Page
    {
        public static ListView TicketList;
        public TicketPage()
        {
            InitializeComponent();
            DataContext = new DataTicketVM();
            TicketList = TicketListBlock;
        }
    }
}
