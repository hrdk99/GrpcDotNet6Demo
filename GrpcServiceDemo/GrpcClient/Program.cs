using Grpc.Net.Client;
using GrpcClient;


internal class Program
{
    private static async Task Main(string[] args)
    {
        var client = new CountryInfoClient();

        Console.Write("Enter Country:");
        string inputCountry = Console.ReadLine().Trim();

        while (inputCountry.ToLower().Trim() != "exit")
        {
            await client.CallGrpcClient(inputCountry);
            Console.Write("\nEnter Exit to stop. \nEnter Country:");
            inputCountry = Console.ReadLine().Trim();
        }                
    }
}


public class CountryInfoClient
{
    public async Task CallGrpcClient(string reqCountry)
    {
        var grpcChannel = GrpcChannel.ForAddress("https://localhost:7125");

        var client = new CountryInfo.CountryInfoClient(grpcChannel);

        var request = new CountryCapitalRequest();
        request.Country = reqCountry;

        var response = await client.GetCountryCapitalAsync(request);
        Console.WriteLine($"Country: {response.Country}, Capital: {response.City}");

    }
}
