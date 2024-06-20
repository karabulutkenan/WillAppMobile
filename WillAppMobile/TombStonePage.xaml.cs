using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WillAppMobileData.Models;
using WillAppMobileData.Repositories;

namespace WillAppMobile
{
    public partial class TombStonePage : ContentPage
    {
        private readonly TombstoneRepository _tombstoneRepository;
        private User _currentUser;

        public ObservableCollection<Post> Posts { get; set; } = new ObservableCollection<Post>();

        public TombStonePage()
        {
            InitializeComponent();
            BindingContext = this;
            _tombstoneRepository = new TombstoneRepository(App.Database);
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadUserData();
            await LoadPosts();
        }

        private async Task LoadUserData()
        {
            // Kullanýcýnýn giriþ yapmýþ olduðunu varsayýyoruz
            int userId = 1; // Bu örnek için sabit deðer kullanýyoruz. Gerçek uygulamada oturum açan kullanýcý ID'si kullanýlacak.
            _currentUser = await _tombstoneRepository.GetUserWithPostsAsync(userId);

            if (_currentUser != null)
            {
                userNameLabel.Text = $"{_currentUser.FirstName} {_currentUser.LastName}";

                if (_currentUser.Photo != null && _currentUser.Photo.Length > 0)
                {
                    profileImage.Source = ImageSource.FromStream(() => new MemoryStream(_currentUser.Photo));
                }
                else
                {
                    profileImage.Source = "default_user_icon.png";
                }
            }
        }

        private async Task LoadPosts()
        {
            if (_currentUser != null && _currentUser.Posts.Any())
            {
                Posts.Clear();
                foreach (var post in _currentUser.Posts)
                {
                    Posts.Add(post);
                }
                noPostsLabel.IsVisible = false;
            }
            else
            {
                noPostsLabel.IsVisible = true;
            }

            postsCollectionView.ItemsSource = Posts;
        }
    }
}
