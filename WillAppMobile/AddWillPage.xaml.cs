using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using WillAppMobileData.Models;
using WillAppMobileData;
using Microsoft.EntityFrameworkCore;

namespace WillAppMobile
{
    public partial class AddWillPage : ContentPage
    {
        private readonly AppDbContext _context;
        public ObservableCollection<string> Files { get; set; } = new ObservableCollection<string>();
        public ObservableCollection<Executor> Executors { get; set; } = new ObservableCollection<Executor>();

        public AddWillPage(AppDbContext context)
        {
            InitializeComponent();
            _context = context;
            BindingContext = this;
            LoadExecutors();
        }

        private async void LoadExecutors()
        {
            var executors = await _context.Executors.ToListAsync();
            foreach (var executor in executors)
            {
                Executors.Add(executor);
                executorPicker.Items.Add($"{executor.FirstName} {executor.LastName}");
            }
            executorPicker.Items.Add("Yeni Vasi Ekle");
        }

        private void OnExecutorSelected(object sender, EventArgs e)
        {
            if (executorPicker.SelectedIndex == executorPicker.Items.Count - 1) // "Yeni Vasi Ekle" se�ene�i
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
            if (executorPicker.SelectedIndex < executorPicker.Items.Count - 1)
            {
                selectedExecutor = Executors[executorPicker.SelectedIndex];
            }
            else
            {
                selectedExecutor = new Executor
                {
                    FirstName = executorFirstNameEntry.Text,
                    LastName = executorLastNameEntry.Text,
                    Email = executorEmailEntry.Text
                };

                _context.Executors.Add(selectedExecutor);
                await _context.SaveChangesAsync();
            }

            var newWill = new Will
            {
                Title = titleEntry.Text,
                Summary = summaryEntry.Text,
                Details = detailsEditor.Text,
                Executor = selectedExecutor,
                Files = Files.Select(f => new WillAppMobileData.Models.File { Path = f }).ToList()
            };

            _context.Wills.Add(newWill);
            await _context.SaveChangesAsync();

            await DisplayAlert("Ba�ar�l�", "Vasiyet kaydedildi", "OK");
            await Navigation.PopAsync(); // �nceki sayfaya d�n.
        }
    }
}
