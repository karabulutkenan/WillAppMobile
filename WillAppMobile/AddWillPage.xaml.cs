using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using WillAppMobile.Models;

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
            // Vasi listesini y�kleme sim�lasyonu, burada veritaban�ndan y�klenecek
            Executors.Add(new Executor { FirstName = "Ahmet", LastName = "Y�lmaz", Email = "ahmet@example.com" });
            foreach (var executor in Executors)
            {
                executorPicker.Items.Add($"{executor.FirstName} {executor.LastName}");
            }
        }

        private void OnExecutorSelected(object sender, EventArgs e)
        {
            if (executorPicker.SelectedIndex == 0) // "Yeni Vasi Ekle" se�ene�i
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
                selectedExecutor = Executors[executorPicker.SelectedIndex - 1]; // "Yeni Vasi Ekle" d�zeltmesi
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
                Files = Files.ToList()
            };

            // Veritaban�na kaydetme i�lemi burada ger�ekle�tirilir
            await DisplayAlert("Ba�ar�l�", "Vasiyet kaydedildi", "OK");
            await Navigation.PopAsync(); // �nceki sayfaya d�n.
        }
    }
}
