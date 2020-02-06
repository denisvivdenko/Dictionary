using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Dictionary.View.LoginPages
{
    /// <summary>
    /// Interaction logic for RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : Page
    {
        LoginWindow view;
        public RegistrationPage(LoginWindow view)
        {
            InitializeComponent();
            this.view = view;
        }

        private void LogInForm(object sender, RoutedEventArgs e)
        {
            view.MainFrame.NavigationService.GoBack();
        }
    }
}
