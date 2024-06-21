using Microsoft.Maui.Controls;
using System;
using System.IO;
using System.Threading.Tasks;
using WillAppMobileData;  // Ekle
using WillAppMobileData.Models;  // Ekle
using WillAppMobileData.Repositories;  // Ekle
namespace WillAppMobile
{
    public partial class ProfileSettingsPage : ContentPage
    {
        private readonly UserRepository _userRepository;
        private User _currentUser;

        public ProfileSettingsPage()
        {
            InitializeComponent();
            _userRepository = new UserRepository(App.Database);
            LoadProfile();
        }

        private async void LoadProfile()
        {
            // Kullanýcý bilgilerini yükleme
            _currentUser = await _userRepository.GetUserByEmailAsync(App.CurrentUserEmail);
            if (_currentUser != null)
            {
                firstNameEntry.Text = _currentUser.FirstName;
                lastNameEntry.Text = _currentUser.LastName;
                emailEntry.Text = _currentUser.Email;
                phoneEntry.Text = _currentUser.PhoneNumber;

                if (!string.IsNullOrEmpty(_currentUser.Photo))
                {
                    profileImage.Source = ImageSource.FromFile(_currentUser.Photo);
                }
                else
                {
                    profileImage.Source = "user_icon.png"; // Varsayýlan fotoðraf
                }
            }
        }

        private async void OnUploadPhotoClicked(object sender, EventArgs e)
        {
            var result = await FilePicker.Default.PickAsync();
            if (result != null)
            {
                _currentUser.Photo = result.FullPath;
                profileImage.Source = ImageSource.FromFile(result.FullPath);
            }
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            if (_currentUser != null)
            {
                _currentUser.FirstName = firstNameEntry.Text;
                _currentUser.LastName = lastNameEntry.Text;
                _currentUser.Email = emailEntry.Text;
                _currentUser.PhoneNumber = phoneEntry.Text;

                await _userRepository.UpdateUserAsync(_currentUser);

                await DisplayAlert("Baþarýlý", "Profil bilgileri güncellendi", "Tamam");
            }
        }
    }
}
