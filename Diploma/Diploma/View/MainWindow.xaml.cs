using Diploma.Model.Data;
using Diploma.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace Diploma.View
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Frame FramePage; 
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new DataMainWindowVM();
            FramePage = FrameBlock;
            
        }
    }
}
