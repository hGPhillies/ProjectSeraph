using ProjectSeraph_AdminClient.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Json;       

namespace ProjectSeraph_AdminClient.ViewModel
{
    public class MeasurementService
    {
        private readonly HttpClient _http;
        public MeasurementService()
        {
            _http = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:5001")
            };
        }

        // Retrieves all measurement data from the backend API. (GET /measurement/getAll)
        public async Task<IEnumerable<MeasurementData>> GetAllAsync()
        {
            var result = await _http.GetFromJsonAsync<IEnumerable<MeasurementData>>("/measurement/getAll");
            return result ?? Enumerable.Empty<MeasurementData>();
        }

        // Retrieves all measurement data for a specific citizen by their ID. (GET /measurement/getAll{citizenID})
        //Maybe for later use, to filter measurements by citizen
        public async Task<IEnumerable<MeasurementData>>GetAllByCitizenIdAsync(string citizenID)
        {
            if(string.IsNullOrWhiteSpace(citizenID))           
                return Enumerable.Empty<MeasurementData>();

                var result = await _http.GetFromJsonAsync<IEnumerable<MeasurementData>>($"/measurement/getAll{citizenID}");
                return result ?? Enumerable.Empty<MeasurementData>();
            }
            
        }
    }

