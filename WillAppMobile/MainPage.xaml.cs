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

        private async void OnMenuClicked(object sender, EventArgs e)
        {
            string action = await DisplayActionSheet("Menü", "İptal", null, "Vasiyetlerim", "Mezar Taşım", "Ayarlar");
            switch (action)
            {
                case "Vasiyetlerim":
                    await Navigation.PushAsync(new WillsPage());
                    break;
                case "Mezar Taşım":
                    await Navigation.PushAsync(new TombStonePage());
                    break;
                case "Ayarlar":
                    await Navigation.PushAsync(new SettingsPage());
                    break;
            }
        }
    }
}

