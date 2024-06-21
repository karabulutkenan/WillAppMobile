using Microsoft.Maui.Controls;
using System;
using WillAppMobileData;  // Ekle
using WillAppMobileData.Models;  // Ekle
using WillAppMobileData.Repositories;  // Ekle

namespace WillAppMobile
{
    public partial class LoginPage : ContentPage
    {
        private readonly UserRepository _userRepository;

        public LoginPage()
        {
            InitializeComponent();
            _userRepository = new UserRepository(App.Database);
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            string email = emailEntry.Text;
            string password = passwordEntry.Text;

            var user = await _userRepository.GetUserByEmailAsync(email);

            if (user != null && VerifyPassword(password, user.PasswordHash))
            {
                App.CurrentUserEmail = user.Email; // CurrentUserEmail'i ayarla
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

        private bool VerifyPassword(string password, string storedHash)
        {
            // �ifre do�rulama mant��� burada olacak
            // �rne�in, BCrypt kullanarak:
            // return BCrypt.Net.BCrypt.Verify(password, storedHash);
            return password == storedHash; // Basit bir do�rulama �rne�i
        }
    }
}
