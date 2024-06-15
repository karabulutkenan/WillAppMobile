using WillAppMobile.Services;

namespace WillAppMobile;

public partial class SignupPage : ContentPage
{
    private readonly EDevletService eDevletService;

    public SignupPage()
    {
        InitializeComponent();
        eDevletService = new EDevletService();
    }

    private async void OnSignupClicked(object sender, EventArgs e)
    {
        string tc = tcEntry.Text;
        string name = nameEntry.Text;
        string surname = surnameEntry.Text;
        string email = emailEntry.Text;
        string phone = phoneEntry.Text;
        string password = passwordEntry.Text;
        string confirmPassword = confirmPasswordEntry.Text;

        if (tc.Length != 11)
        {
            await DisplayAlert("Hata", "TC Kimlik Numarasý 11 hane olmalýdýr.", "Tamam");
            return;
        }

        if (password != confirmPassword)
        {
            await DisplayAlert("Hata", "Þifreler eþleþmiyor.", "Tamam");
            return;
        }

        bool isValid = await eDevletService.ValidateIdentity(tc, name, surname);

        if (isValid)
        {
            // Üyelik oluþturma iþlemleri
            await DisplayAlert("Baþarýlý", "Üyelik baþarýyla oluþturuldu.", "Tamam");
        }
        else
        {
            await DisplayAlert("Hata", "Geçersiz kimlik bilgileri.", "Tamam");
        }
    }
}
