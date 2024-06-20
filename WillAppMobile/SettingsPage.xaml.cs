using Microsoft.Maui.Controls;
using System;

namespace WillAppMobile
{
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();
        }

        private async void OnProfileSettingsClicked(object sender, EventArgs e)
        {
            // Profil ayarlarý sayfasýna yönlendirme
            await Navigation.PushAsync(new ProfileSettingsPage());
        }

        private async void OnNotificationSettingsClicked(object sender, EventArgs e)
        {
            // Bildirim ayarlarý sayfasýna yönlendirme
            await Navigation.PushAsync(new NotificationSettingsPage());
        }

        private async void OnDeleteAccountClicked(object sender, EventArgs e)
        {
            var answer = await DisplayAlert("Hesabý Sil", "Hesabýnýzý silmek istediðinizden emin misiniz?", "Evet", "Hayýr");
            if (answer)
            {
                // Hesap silme iþlemi burada gerçekleþtirilir
                await DisplayAlert("Baþarýlý", "Hesabýnýz silindi.", "Tamam");
                // Giriþ sayfasýna yönlendirme
                Application.Current.MainPage = new LoginPage();
            }
        }
    }
}
