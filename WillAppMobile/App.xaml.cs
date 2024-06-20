using Microsoft.Maui.Controls;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using WillAppMobileData; // Doğru namespace'i burada belirtin

namespace WillAppMobile
{
    public partial class App : Application
    {
        private static AppDbContext _database;

        public App()
        {
            InitializeComponent();

            MainPage = new LoginPage();
        }

        public static AppDbContext Database
        {
            get
            {
                if (_database == null)
                {
                    var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "WillAppMobile.db3");
                    var options = new DbContextOptionsBuilder<AppDbContext>()
                        .UseSqlite($"Filename={dbPath}")
                        .Options;
                    _database = new AppDbContext(options);
                }
                return _database;
            }
        }
    }
}
