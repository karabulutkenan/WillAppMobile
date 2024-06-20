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
            // Profil ayarlar� sayfas�na y�nlendirme
            await Navigation.PushAsync(new ProfileSettingsPage());
        }

        private async void OnNotificationSettingsClicked(object sender, EventArgs e)
        {
            // Bildirim ayarlar� sayfas�na y�nlendirme
            await Navigation.PushAsync(new NotificationSettingsPage());
        }

        private async void OnDeleteAccountClicked(object sender, EventArgs e)
        {
            var answer = await DisplayAlert("Hesab� Sil", "Hesab�n�z� silmek istedi�inizden emin misiniz?", "Evet", "Hay�r");
            if (answer)
            {
                // Hesap silme i�lemi burada ger�ekle�tirilir
                await DisplayAlert("Ba�ar�l�", "Hesab�n�z silindi.", "Tamam");
                // Giri� sayfas�na y�nlendirme
                Application.Current.MainPage = new LoginPage();
            }
        }
    }
}
