using Microsoft.Maui.Controls;
using System;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using WillAppMobileData;  // Ekle
using WillAppMobileData.Models;  // Ekle
using WillAppMobileData.Repositories;  // Ekle

namespace WillAppMobile
{
    public partial class SignupPage : ContentPage
    {
        private readonly UserRepository _userRepository;
        private string _photoPath;

        public SignupPage()
        {
            InitializeComponent();
            _userRepository = new UserRepository(App.Database);
        }

        private async void OnSignupClicked(object sender, EventArgs e)
        {
            var tc = tcEntry.Text;
            var name = nameEntry.Text;
            var surname = surnameEntry.Text;
            var email = emailEntry.Text;
            var phone = phoneEntry.Text;
            var password = passwordEntry.Text;
            var confirmPassword = confirmPasswordEntry.Text;

            if (password != confirmPassword)
            {
                await DisplayAlert("Hata", "Şifreler uyuşmuyor.", "Tamam");
                return;
            }

            var user = new User
            {
                FirstName = name,
                LastName = surname,
                Email = email,
                PhoneNumber = phone,
                PasswordHash = CreatePasswordHash(password),
                Photo = _photoPath // Hata burada düzeltilmiştir
            };

            await _userRepository.AddUserAsync(user);

            await DisplayAlert("Başarılı", "Üyelik oluşturuldu.", "Tamam");
            await Navigation.PopModalAsync();
        }

        private string CreatePasswordHash(string password)
        {
            using (var hmac = new HMACSHA512())
            {
                var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hash);
            }
        }

        private async void OnUploadPhotoClicked(object sender, EventArgs e)
        {
            var result = await FilePicker.Default.PickAsync(new PickOptions
            {
                PickerTitle = "Fotoğraf Seç",
                FileTypes = FilePickerFileType.Images
            });

            if (result != null)
            {
                _photoPath = result.FullPath;
                // Seçilen fotoğrafın önizlemesini gösterebilirsiniz
                // profileImage.Source = ImageSource.FromFile(_photoPath);
            }
        }
    }
}
