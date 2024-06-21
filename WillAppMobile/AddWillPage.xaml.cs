using Microsoft.EntityFrameworkCore;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using WillAppMobileData;
using WillAppMobileData.Models;
using WillAppMobileData.Repositories;

namespace WillAppMobile
{
    public partial class AddWillPage : ContentPage
    {
        public ObservableCollection<string> Files { get; set; } = new ObservableCollection<string>();
        public ObservableCollection<Executor> Executors { get; set; } = new ObservableCollection<Executor>();

        public AddWillPage()
        {
            InitializeComponent();
            BindingContext = this;
            LoadExecutors();
        }

        private async void LoadExecutors()
        {
            using (var context = new AppDbContext(DependencyService.Get<DbContextOptions<AppDbContext>>()))
            {
                var executorRepository = new ExecutorRepository(context);
                var executors = await executorRepository.GetAllExecutorsAsync();
                Executors = new ObservableCollection<Executor>(executors);
                executorPicker.Items.Add("Yeni Vasi Ekle");
                foreach (var executor in Executors)
                {
                    executorPicker.Items.Add($"{executor.FirstName} {executor.LastName}");
                }
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

            var newWill = new Will
            {
                Title = titleEntry.Text,
                Summary = summaryEntry.Text,
                Details = detailsEditor.Text,
                Executor = selectedExecutor,
                Files = Files.Select(file => new WillAppMobileData.Models.File { FilePath = file }).ToList() // Güncellenen kýsým
            };

            using (var context = new AppDbContext(DependencyService.Get<DbContextOptions<AppDbContext>>()))
            {
                var willRepository = new WillRepository(context);
                await willRepository.AddWillAsync(newWill);
            }

            await DisplayAlert("Baþarýlý", "Vasiyet kaydedildi", "OK");
            await Navigation.PopAsync(); // Önceki sayfaya dön.
        }
    }
}
