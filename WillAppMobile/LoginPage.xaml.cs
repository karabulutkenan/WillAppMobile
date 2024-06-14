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
            // Giri� i�lemleri burada yap�lacak
            await DisplayAlert("Giri�", "Giri� yap�ld�.", "OK");
            Application.Current.MainPage = new AppShell();
        }

        private async void OnSignUpClicked(object sender, EventArgs e)
        {
            // Kay�t olma i�lemleri burada yap�lacak
            await DisplayAlert("Kay�t Ol", "Kay�t olma sayfas�na y�nlendiriliyor.", "OK");
        }
    }
}
