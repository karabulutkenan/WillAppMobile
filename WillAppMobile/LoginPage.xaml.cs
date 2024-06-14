using Microsoft.Maui.Controls;
using System;

namespace WillAppMobile
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            // Giriþ iþlemleri burada yapýlacak
            await DisplayAlert("Giriþ", "Giriþ yapýldý.", "OK");
            Application.Current.MainPage = new AppShell();
        }

        private async void OnSignUpClicked(object sender, EventArgs e)
        {
            // Kayýt olma iþlemleri burada yapýlacak
            await DisplayAlert("Kayýt Ol", "Kayýt olma sayfasýna yönlendiriliyor.", "OK");
        }
    }
}
