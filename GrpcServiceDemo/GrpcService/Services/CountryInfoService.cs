using Grpc.Core;
using Newtonsoft.Json;

namespace GrpcService.Services
{
    public class CountryInfoService : CountryInfo.CountryInfoBase
    {
        public override Task<CountryCapitalResponse> GetCountryCapital(CountryCapitalRequest request, ServerCallContext context)
        {
            List<CapitalCity> cities = new();
            using (StreamReader r = new StreamReader(@"Assets\\CapitalCities.json"))
            {
                string json = r.ReadToEnd();
                cities = JsonConvert.DeserializeObject<List<CapitalCity>>(json);
            }
            var response = cities?.FirstOrDefault(c => c.Country.ToLower() == request.Country.ToLower());
            if(response is null)
            {
                return Task.FromResult(new CountryCapitalResponse() { City = "Capital info not found", Country = request.Country });
            }

            return Task.FromResult(new CountryCapitalResponse() { City = response.City, Country = response.Country });

        }
    }
    public class CapitalCity
    {
        public string Country { get; set; }
        public string City { get; set; }
    }
}
