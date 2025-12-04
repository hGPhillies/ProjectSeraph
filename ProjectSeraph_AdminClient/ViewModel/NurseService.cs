using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Json;
using ProjectSeraph_AdminClient.Model;

namespace ProjectSeraph_AdminClient.ViewModel
{
    public class NurseService
    {
        private readonly HttpClient _http;

        public NurseService()
        {
            _http = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:5001")
            };
        }

        public async Task<IEnumerable<Nurse>> GetAllAsync()
        {
            var result = await _http.GetFromJsonAsync<IEnumerable<Nurse>>("/nurse/getAll");
                return result ?? Enumerable.Empty<Nurse>();
        }

        public async Task<Nurse?> CreateAsync(Nurse nurse)
        {
            var response = await _http.PostAsJsonAsync("/nurse/", nurse);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Nurse>();
            }
            return null;
        }
    }  
}
