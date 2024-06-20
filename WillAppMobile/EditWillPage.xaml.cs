using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using WillAppMobileData.Models;
using WillAppMobileData.Repositories;

namespace WillAppMobile
{
    public partial class EditWillPage : ContentPage
    {
        public ObservableCollection<string> Files { get; set; } = new ObservableCollection<string>();
        public ObservableCollection<Executor> Executors { get; set; } = new ObservableCollection<Executor>();
        private Will _will;

        public EditWillPage(Will will)
        {
            InitializeComponent();
            _will = will;
            BindingContext = this;

            titleEntry.Text = _will.Title;
            summaryEntry.Text = _will.Summary;
            detailsEditor.Text = _will.Details;
            LoadExecutors();
            LoadFiles();
        }

        private async void LoadExecutors()
        {
            // Vasi listesini yükleme simülasyonu, burada veritabanýndan yüklenecek
            Executors.Add(new Executor { FirstName = "Ahmet", LastName = "Yýlmaz", Email = "ahmet@example.com" });
            foreach (var executor in Executors)
            {
                executorPicker.Items.Add($"{executor.FirstName} {executor.LastName}");
            }

            if (_will.Executor != null)
            {
                executorPicker.SelectedItem = $"{_will.Executor.FirstName} {_will.Executor.LastName}";
            }
        }

        private void LoadFiles()
        {
            foreach (var file in _will.Files)
            {
                Files.Add(file.FilePath);
            }
        }

        private void OnExecutorSelected(object sender, EventArgs e)
        {
            if (executorPicker.SelectedIndex == 0) // "Yeni Vasi Ekle" seçeneði
            {
                newExecutorDetails.IsVisible = true;
            }
            else
            {
                newExecutorDetails.IsVisible = false;
            }
        }

        private async void OnUploadFileClicked(object sender, EventArgs e)
        {
            var result = await FilePicker.Default.PickMultipleAsync();
            if (result != null)
            {
                foreach (var file in result)
                {
                    Files.Add(file.FullPath);
                }
            }
        }

        private async void SaveWillClicked(object sender, EventArgs e)
        {
            Executor selectedExecutor = null;
            if (executorPicker.SelectedIndex > 0)
            {
                selectedExecutor = Executors[executorPicker.SelectedIndex - 1]; // "Yeni Vasi Ekle" düzeltmesi
            }
            else
            {
                selectedExecutor = new Executor
                {
                    FirstName = executorFirstNameEntry.Text,
                    LastName = executorLastNameEntry.Text,
                    Email = executorEmailEntry.Text
                };
            }

            _will.Title = titleEntry.Text;
            _will.Summary = summaryEntry.Text;
            _will.Details = detailsEditor.Text;
            _will.Executor = selectedExecutor;
            _will.Files = Files.Select(f => new WillAppMobileData.Models.File { FilePath = f }).ToList();

            // Veritabanýna kaydetme iþlemi burada gerçekleþtirilir
            var willRepository = new WillRepository(App.Database);
            await willRepository.UpdateWillAsync(_will);

            await DisplayAlert("Baþarýlý", "Vasiyet güncellendi", "OK");
            await Navigation.PopAsync(); // Önceki sayfaya dön.
        }
    }
}
