using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using TeacherTracker.MAUI.Models;

namespace TeacherTracker.MAUI.Services
{
    public class SchoolApiService
    {
        private readonly HttpClient _httpClient;

        public SchoolApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<SchoolDto>> GetSchoolsAsync()
        {
            var schools = await _httpClient
                .GetFromJsonAsync<List<SchoolDto>>("api/schools");

            return schools ?? new List<SchoolDto>();
        }

        public async Task<SchoolDto> CreateSchoolAsync(CreateSchoolRequest request)
        {
            using var response = await _httpClient.PostAsJsonAsync("api/schools", request);

            response.EnsureSuccessStatusCode();

            var school = await response.Content.ReadFromJsonAsync<SchoolDto>();

            return school ?? throw new InvalidOperationException("Api nije vratio podatke nove skole");

        }
    }
}
