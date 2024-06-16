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
            string email = emailEntry.Text;
            string password = passwordEntry.Text;

            // Basit bir doðrulama (gerçek bir doðrulama mekanizmasý kullanýlabilir)
            if (email == "user@example.com" && password == "password")
            {
                await DisplayAlert("Bilgi", "Giriþ baþarýlý!", "Tamam");
                // Ana sayfaya yönlendir
                Application.Current.MainPage = new AppShell();
            }
            else
            {
                await DisplayAlert("Hata", "Geçersiz e-posta veya þifre.", "Tamam");
            }
        }

        private async void OnSignUpClicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new SignupPage());
        }
    }
}
