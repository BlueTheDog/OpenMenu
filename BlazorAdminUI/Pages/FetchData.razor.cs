using Microsoft.AspNetCore.Components;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BlazorAdminUI.Pages
{
    public partial class FetchData
    {
        [Inject]
        public HttpClient Http { get; set; }
        private LocationDto[] _locations;

        protected override async Task OnInitializedAsync()
        {
            _locations = await Http.GetFromJsonAsync<LocationDto[]>("locations");
        }
    }

    public class LocationDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
