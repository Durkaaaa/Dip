using Diploma.Command;
using Diploma.Model;
using Diploma.View;
using System.Collections.Generic;

namespace Diploma.ViewModel
{
    public class DataTicketVM : ViewModelBase
    {
        public static Ticket SelectedTicket { get; set; }

        private List<Ticket> _allTicket = DataWorker.GetAllTicket();
        public List<Ticket> AllTicket
        {
            get { return _allTicket; }
            set
            {
                _allTicket = value;
                NotifyPropertyChanged("AllTicket");
            }
        }

        public RelayCommand AddNewTicket
        {
            get
            {
                return null ?? new RelayCommand(obj =>
                {
                    AddNewTicketWindow addNewTicketWindow = new AddNewTicketWindow();
                    SetCenterPositionAndOpen(addNewTicketWindow);
                    UpdateAllTicketPage();
                });
            }
        }

        public RelayCommand EditTicket
        {
            get
            {
                return null ?? new RelayCommand(obj =>
                {
                    if (SelectedTicket != null)
                    {
                        EditTicketWindow editTicketWindow = new EditTicketWindow(SelectedTicket);
                        SetCenterPositionAndOpen(editTicketWindow);
                        SelectedTicket = null;
                        UpdateAllTicketPage();
                    }
                });
            }
        }

        public RelayCommand DeleteTicket
        {
            get
            {
                return null ?? new RelayCommand(obj =>
                {
                    if (SelectedTicket != null)
                    {
                        var result = DataWorker.DeleteTicket(SelectedTicket);
                        ShowMessageToUser(result);
                        SelectedTicket = null;
                        UpdateAllTicketPage();
                    }
                });
            }
        }
    }
}
