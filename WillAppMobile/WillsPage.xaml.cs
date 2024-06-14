using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using WillAppMobile.Models;

namespace WillAppMobile
{
    public partial class WillsPage : ContentPage
    {
        public ObservableCollection<Will> Wills { get; set; }
        public Command AddNewWillCommand { get; set; }
        public Command EditCommand { get; set; }
        public Command DeleteCommand { get; set; }

        public WillsPage()
        {
            InitializeComponent();

            Wills = new ObservableCollection<Will>
            {
                new Will { Title = "Vasiyet �rne�i 1" },
                new Will { Title = "Vasiyet �rne�i 2" }
            };

            AddNewWillCommand = new Command(OnAddNewWill);
            EditCommand = new Command<Will>(OnEditWill);
            DeleteCommand = new Command<Will>(OnDeleteWill);

            BindingContext = this;
        }

        private async void OnAddNewWill()
        {
            // Yeni vasiyet ekleme sayfas�na y�nlendirme
            await Navigation.PushAsync(new AddWillPage());
        }

        private void OnEditWill(Will will)
        {
            // Vasiyet d�zenleme i�lemleri
            // Burada ilgili d�zenleme mant���n� uygulay�n
        }

        private void OnDeleteWill(Will will)
        {
            // Vasiyet silme i�lemleri
            if (Wills.Contains(will))
            {
                Wills.Remove(will);
            }
        }

        private async void AddNewWillClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddWillPage());
        }
    }
}
