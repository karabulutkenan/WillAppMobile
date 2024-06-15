using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace WillAppMobile.Services
{
    public class EDevletService
    {
        private static readonly HttpClient client = new HttpClient();

        public async Task<bool> ValidateIdentity(string tc, string name, string surname)
        {
            var url = $"https://tckimlik.nvi.gov.tr/Modul/TcKimlikNoDogrula?TCKimlikNo={tc}&Ad={name}&Soyad={surname}";
            var response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<EDevletResponse>(jsonResponse);
                return result != null && result.Return;
            }
            return false;
        }
    }

    public class EDevletResponse
    {
        public bool Return { get; set; }
    }
}
