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

        private bool VerifyPassword(string password, string storedHash)
        {
            // Þifre doðrulama mantýðý burada olacak
            // Örneðin, BCrypt kullanarak:
            // return BCrypt.Net.BCrypt.Verify(password, storedHash);
            return password == storedHash; // Basit bir doðrulama örneði
        }
    }
}
