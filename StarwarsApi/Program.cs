using System;
using System.Net.Http;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

public class Program
{
    private static readonly HttpClient client = new HttpClient();
    static async Task Main(string[] args)
    {
        var heroes = await GetHerosFromSwapi();
        Console.WriteLine("Welcome to StarWars, Pick your Heroes and let's see who survives to conquer the universe!");
        for (int i = 0; i < heroes.Count; i++)
        {
            Console.WriteLine($"{i + 1}: {heroes[i].Name}");
        }
        var selectedHeroes = new List<Hero>();
        for (int pick = 1; pick <= 2; pick++)
        {
            Console.WriteLine("--------------------------------");
            Console.Write($"Player {pick} pick hero by number: ");
            if (int.TryParse(Console.ReadLine(), out int index) && index >= 1 && index <= heroes.Count)
            {
                selectedHeroes.Add(heroes[index - 1]);
            }
            else
            {
                Console.WriteLine("Not a valid number, try again...");
                pick--;
            }
        }
        Console.WriteLine("--------------------------------");
        Console.WriteLine("You have chosen the following heroes: ");
        foreach (var hero in selectedHeroes)
        {
            Console.WriteLine(hero);
            if (int.Parse(hero.Height) < 70)
            {
                hero.Force = 200;
            }
            else if (int.Parse(hero.Height) < 100)
            {
                hero.Force = 150;
            }

        }
        Console.WriteLine("--------------------------------");
        Console.WriteLine("press any key to start the battle!");
        Console.ReadLine();
        var winner = Battle(selectedHeroes[0], selectedHeroes[1]);
        
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
    public Hero Battle(Hero hero1, Hero hero2)
    {
        int[] points = { 0, 0 };
        if (int.Parse(heroes[0].Height) > int.Parse(heroes[1].Height))
        {
            
        }
    }
}