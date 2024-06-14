using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using WillAppMobile.Models;

namespace WillAppMobile
{
    public partial class AddWillPage : ContentPage
    {
        public ObservableCollection<UploadedFile> Files { get; set; }

        public AddWillPage()
        {
            InitializeComponent();
            Files = new ObservableCollection<UploadedFile>();
            BindingContext = this;
        }

        private async void OnUploadFileClicked(object sender, EventArgs e)
        {
            var result = await FilePicker.Default.PickAsync(new PickOptions
            {
                PickerTitle = "Dosya Se�",
                FileTypes = FilePickerFileType.Images // T�m dosya t�rlerini desteklemek i�in de�i�tirilebilir
            });

            if (result != null)
            {
                var file = new UploadedFile
                {
                    FileName = result.FileName,
                    FilePath = result.FullPath
                };

                Files.Add(file);
            }
        }

        private async void SaveWillClicked(object sender, EventArgs e)
        {
            var willTitle = titleEntry.Text;
            var willDetails = detailsEditor.Text;

            // Yeni vasiyet ve dosyalar eklenir
            var newWill = new Will
            {
                Title = willTitle,
                Details = willDetails,
                Files = new List<UploadedFile>(Files)
            };

            var previousPage = (WillsPage)Navigation.NavigationStack[Navigation.NavigationStack.Count - 2];
            previousPage.Wills.Add(newWill);

            await Navigation.PopAsync();
        }
    }
}
