using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using WillAppMobileData.Models;
using WillAppMobileData.Repositories;

namespace WillAppMobile
{
    public partial class WillsPage : ContentPage
    {
        private readonly WillRepository _willRepository;

        public ObservableCollection<Will> Wills { get; set; }

        public WillsPage()
        {
            InitializeComponent();

            _willRepository = new WillRepository(App.Database);
            Wills = new ObservableCollection<Will>();
            BindingContext = this;

            LoadWills();
        }

        private async void LoadWills()
        {
            var wills = await _willRepository.GetAllWillsAsync();
            foreach (var will in wills)
            {
                Wills.Add(will);
            }
        }

        private async void OnAddNewWill(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddWillPage());
        }

        private async void OnEditWill(object sender, EventArgs e)
        {
            var menuItem = sender as MenuItem;
            var will = menuItem?.BindingContext as Will;
            if (will != null)
            {
                await Navigation.PushAsync(new EditWillPage(will));
            }
        }

        private async void OnDeleteWill(object sender, EventArgs e)
        {
            var menuItem = sender as MenuItem;
            var will = menuItem?.BindingContext as Will;
            if (will != null)
            {
                await _willRepository.DeleteWillAsync(will);
                Wills.Remove(will);
            }
        }
    }
}
