using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace WillAppMobile;

public partial class SignupPage : ContentPage
{
    public SignupPage()
    {
        InitializeComponent();
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

        bool isValid = await ValidateWithEDevlet(tc, name, surname);

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

    private async Task<bool> ValidateWithEDevlet(string tc, string name, string surname)
    {
        using (var client = new HttpClient())
        {
            var response = await client.GetAsync($"https://tckimlik.nvi.gov.tr/Modul/TcKimlikNoDogrula?TCKimlikNo={tc}&Ad={name}&Soyad={surname}");
            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<EDevletResponse>(jsonResponse);
                return result.return; // E-Devlet API response should have a proper return field for validation
            }
            return false;
        }
    }
}

public class EDevletResponse
{
    public bool return { get; set; }
}
