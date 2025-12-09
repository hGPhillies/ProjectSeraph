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
    internal class CitizenService
    {
        private readonly HttpClient _http;

        public CitizenService()
        {
            _http = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:5001")
            };
        }


        public async Task<IEnumerable<Citizen>> GetAllAsync()
        {
            var result = await _http.GetFromJsonAsync<IEnumerable<Citizen>>("/citizen/getAll");
            return result ?? Enumerable.Empty<Citizen>();
        }
    }
}
