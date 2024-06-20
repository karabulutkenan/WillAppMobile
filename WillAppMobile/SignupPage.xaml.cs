using Microsoft.Maui.Controls;
using System;
using System.IO;
using WillAppMobileData;
using WillAppMobileData.Models;
using WillAppMobileData.Repositories;

using Microsoft.Maui.Controls;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WillAppMobileData.Models;
using WillAppMobileData.Repositories;

namespace WillAppMobile
{
    public partial class SignupPage : ContentPage
    {
        private readonly UserRepository _userRepository;
        private byte[] _photo;

        public SignupPage()
        {
            InitializeComponent();
            _userRepository = new UserRepository(App.Database);
        }

        private async void OnUploadPhotoClicked(object sender, EventArgs e)
        {
            var result = await FilePicker.Default.PickAsync(new PickOptions
            {
                PickerTitle = "Profil Fotoðrafý Seç",
                FileTypes = FilePickerFileType.Images
            });

            if (result != null)
            {
                using (var stream = await result.OpenReadAsync())
                using (var memoryStream = new MemoryStream())
                {
                    await stream.CopyToAsync(memoryStream);
                    _photo = memoryStream.ToArray();
                }
            }
        }

        private async void OnSignupClicked(object sender, EventArgs e)
        {
            if (passwordEntry.Text != confirmPasswordEntry.Text)
            {
                await DisplayAlert("Hata", "Þifreler uyuþmuyor.", "Tamam");
                return;
            }

            var newUser = new User
            {
                FirstName = nameEntry.Text,
                LastName = surnameEntry.Text,
                Email = emailEntry.Text,
                PhoneNumber = phoneEntry.Text,
                PasswordHash = ComputeHash(passwordEntry.Text),
                Photo = _photo
            };

            await _userRepository.AddUserAsync(newUser);
            await DisplayAlert("Baþarýlý", "Üyelik oluþturuldu.", "Tamam");
            await Navigation.PopModalAsync();
        }

        private string ComputeHash(string input)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(input);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }
    }
}
