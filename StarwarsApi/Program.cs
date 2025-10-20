using System;
using System.Net.Http;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;

public class program
{
    private static readonly HttpClient client = new HttpClient();
    static async Task Main(string[] args)
    {

    }
    static async Task<List<Hero>> GetHerosFromSwapi()
    {
        var heroes = new List<Hero>();
        string url = "https://swapi.py4e.com/api/people/";
        while (!string.IsNullOrEmpty(url))
        {
            try
            {
                var response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsByteArrayAsync();
                var page = JsonSerializer.DeserializeAsync<SwapiResponse>(json);
                heroes.AddRange(page.results);
                url = page.next;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error at API call: {ex.Message}");
                break;
            }
        }
        return heroes;
    }
}