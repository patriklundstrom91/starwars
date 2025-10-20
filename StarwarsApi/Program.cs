using System;
using System.Net.Http;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;

public class Program
{
    private static readonly HttpClient client = new HttpClient();
    static async Task Main(string[] args)
    {
        var heroes = await GetHerosFromSwapi();
        Console.WriteLine("Welcome to StarWars, Pick your Heroes and let's see who survives to conquer the universe!");
        foreach (var hero in heroes)
        {
            Console.WriteLine(hero);
        }
        Console.WriteLine("--------------------------------");
        Console.Write("Player 1 pick hero by name: ");
        var p1Pick = Console.ReadLine();
        
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

                var json = await response.Content.ReadAsStreamAsync();
                var page = await JsonSerializer.DeserializeAsync<SwapiResponse>(json);
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