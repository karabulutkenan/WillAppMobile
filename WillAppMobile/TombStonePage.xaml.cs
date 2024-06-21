using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WillAppMobileData;  // Ekle
using WillAppMobileData.Models;  // Ekle
using WillAppMobileData.Repositories;  // Ekle

namespace WillAppMobile
{
    public partial class TombStonePage : ContentPage
    {
        private readonly UserRepository _userRepository;
        private User _currentUser;

        public ObservableCollection<Post> Posts { get; set; } = new ObservableCollection<Post>();

        public TombStonePage()
        {
            InitializeComponent();
            BindingContext = this;
            _userRepository = new UserRepository(App.Database);
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadUserData();
            LoadPosts(); // LoadPosts metodunu async yapmaya gerek yok
        }

        private async Task LoadUserData()
        {
            _currentUser = await _userRepository.GetUserByIdAsync(App.CurrentUserId);

            if (_currentUser != null)
            {
                userNameLabel.Text = $"{_currentUser.FirstName} {_currentUser.LastName}";

                if (!string.IsNullOrEmpty(_currentUser.Photo))
                {
                    profileImage.Source = ImageSource.FromFile(_currentUser.Photo);
                }
                else
                {
                    profileImage.Source = "default_user_icon.png";
                }
            }
        }

        private void LoadPosts()
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
