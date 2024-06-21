using Microsoft.Maui.Controls;
using WillAppMobileData;  // Ekle
using WillAppMobileData.Models;  // Ekle
using WillAppMobileData.Repositories;  // Ekle

namespace WillAppMobile
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
        }

        private void OnLogoutClicked(object sender, EventArgs e) // 'async' ifadesini kaldırdık
        {
            Application.Current.MainPage = new LoginPage();
        }
    }
}
