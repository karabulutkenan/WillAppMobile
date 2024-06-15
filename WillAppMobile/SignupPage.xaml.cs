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
            await DisplayAlert("Hata", "TC Kimlik Numaras� 11 hane olmal�d�r.", "Tamam");
            return;
        }

        if (password != confirmPassword)
        {
            await DisplayAlert("Hata", "�ifreler e�le�miyor.", "Tamam");
            return;
        }

        bool isValid = await eDevletService.ValidateIdentity(tc, name, surname);

        if (isValid)
        {
            // �yelik olu�turma i�lemleri
            await DisplayAlert("Ba�ar�l�", "�yelik ba�ar�yla olu�turuldu.", "Tamam");
        }
        else
        {
            await DisplayAlert("Hata", "Ge�ersiz kimlik bilgileri.", "Tamam");
        }
    }
}
