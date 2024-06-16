using Microsoft.Maui.Controls;
using System;

namespace WillAppMobile
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnVasiyetlerimClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new WillsPage());
        }

        private async void OnMezarTasimClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TombStonePage());
        }

        private async void OnAyarlarClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SettingsPage());
        }

        private async void OnLogoutClicked(object sender, EventArgs e)
        {
            bool confirm = await DisplayAlert("Çıkış", "Çıkmak istediğinizden emin misiniz?", "Evet", "Hayır");
            if (confirm)
            {
                Application.Current.MainPage = new LoginPage();
            }
        }
    }
}
