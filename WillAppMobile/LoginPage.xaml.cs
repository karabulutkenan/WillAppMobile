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

            // Basit bir do�rulama (ger�ek bir do�rulama mekanizmas� kullan�labilir)
            if (email == "user@example.com" && password == "password")
            {
                await DisplayAlert("Bilgi", "Giri� ba�ar�l�!", "Tamam");
                // Ana sayfaya y�nlendir
                Application.Current.MainPage = new AppShell();
            }
            else
            {
                await DisplayAlert("Hata", "Ge�ersiz e-posta veya �ifre.", "Tamam");
            }
        }

        private async void OnSignUpClicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new SignupPage());
        }
    }
}
