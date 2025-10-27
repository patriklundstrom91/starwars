using System;
using System.Net.Http;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using PlayerModule;
using static PlayerModule.Player;

public class Program
{
    private static readonly HttpClient client = new HttpClient();
    static async Task Main(string[] args)
    {
        var heroes = await GetHerosFromSwapi();
        List<Player> players = new List<Player>();
        while (true)
        {
            List<Player> selectedPlayers = new List<Player>();
            Console.WriteLine("\n--------------------------------\n");
            Console.WriteLine("Welcome to StarWars, Pick your Heroes and let's see who survives to conquer the universe!");
            if (players.Count == 0)
            {
                for (int p = 1; p <= 2; p++)
                {
                    Console.Write($"\nPlayer {p} please enter your name (or leave blank for default name): ");
                    string name = Console.ReadLine();
                    if (name.Length > 0)
                    {
                        players.Add(PlayerFactory.CreatePlayer(name));
                        selectedPlayers.Add(players[^1]);
                    }
                    else if (name.Length == 0)
                    {
                        players.Add(PlayerFactory.CreatePlayer());
                        selectedPlayers.Add(players[^1]);
                    }
                }
            }
            else
            {
                for (int pick = 0; pick <= 1; pick++)
                {

                    Console.WriteLine($"\nPlayer {pick + 1} continue with same players or make new player? Pick option below\n");
                    int option = 0;
                    for (; option < players.Count; option++)
                    {
                        Console.WriteLine($"{option + 1}. {players[option].Name}");
                    }
                    Console.WriteLine($"{option + 1}. New Player");
                    Console.Write("Select by number: ");
                    if (int.TryParse(Console.ReadLine(), out int index) && index >= 1 && index <= players.Count)
                    {
                        selectedPlayers.Add(players[index - 1]);
                    }
                    else if (index == players.Count + 1)
                    {
                        for (int p = 1; p <= 2; p++)
                        {
                            Console.Write($"\nPlayer {p} please enter your name (or leave blank for default name): ");
                            string name = Console.ReadLine();
                            if (name.Length > 0)
                            {
                                players.Add(PlayerFactory.CreatePlayer(name));
                                selectedPlayers.Add(players.Last());
                                pick++;
                            }
                            else if (name.Length == 0)
                            {
                                players.Add(PlayerFactory.CreatePlayer());
                                selectedPlayers.Add(players.Last());
                                pick++;
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("\n Not a valid selection, try again...");
                        pick--;
                    }
                }
            }
            Console.WriteLine("\n--------------------------------\n");
            for (int i = 0; i < heroes.Count; i++)
            {
                Console.WriteLine($"{i + 1}: {heroes[i].Name}");
            }
            var selectedHeroes = new List<Hero>();
            for (int pick = 0; pick <= 1; pick++)
            {
                Console.WriteLine("--------------------------------");
                Console.Write($"{selectedPlayers[pick].Name} pick hero by number: ");
                if (int.TryParse(Console.ReadLine(), out int index) && index >= 1 && index <= heroes.Count)
                {
                    selectedHeroes.Add(heroes[index - 1]);
                }
                else
                {
                    Console.WriteLine("\n Not a valid number, try again...");
                    pick--;
                }
            }
            Console.WriteLine("--------------------------------");
            Console.WriteLine("You have chosen the following heroes: ");
            for (int i = 0; i <= 1; i++)
            {
                selectedHeroes[i].InitStats(i);
                Console.WriteLine($"{players[i].Name}: {selectedHeroes[i]}");
            }
            Console.WriteLine("--------------------------------");
            Console.WriteLine("press any key to start the battle!");
            Console.ReadLine();
            Hero winner = Battle(selectedHeroes[0], selectedHeroes[1]);
            ScoreManager.AddScore(players[winner.PlayerId], 100);
            Console.WriteLine("\n --------------------------------\n");
            Console.WriteLine($"{players[winner.PlayerId].Name} wins with {winner.Name} and will rule the galaxy!");
            Console.WriteLine("\n --------------------------------\n");
            Console.WriteLine("Do you want to play again? Press any key or to see score or exit the game type score or exit ");
            string goAgain = Console.ReadLine();
            if (goAgain == "exit")
            {
                break;
            }
            else if (goAgain == "score")
            {
                Console.WriteLine("\n--------------------------------\n");
                Console.WriteLine("Top Score:\n");
                var toplist = ScoreManager.TopScore(players);
                foreach (var player in toplist)
                {
                    Console.WriteLine($"{player.Name}, Score: {player.Score}");
                }
                Console.WriteLine("\n --------------------------------\n");
                Console.Write("Press any key to continue\n");
                Console.ReadLine();
            }
        }
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
    public static Hero Battle(Hero hero1, Hero hero2)
    {
        Console.WriteLine("\n BATTLE BEGINS \n");

        Console.WriteLine($"{hero1.Name} VS {hero2.Name}");
        Console.WriteLine("--------------------------------");
        Console.WriteLine($"{hero1.Name} HP: {hero1.HP}, Strength: {hero1.Strength}");
        Console.WriteLine($"{hero2.Name} HP: {hero2.HP}, Strength: {hero2.Strength}\n");
        var attacker = hero1;
        var defender = hero2;
        while (hero1.HP > 0 && hero2.HP > 0)
        {
            Console.WriteLine($"{attacker.Name} attacks {defender.Name} with damage of {attacker.Strength}");
            defender.HP -= attacker.Strength;
            if (attacker.Force > 0)
            {
                defender.HP -= attacker.Force;
                Console.WriteLine($"{attacker.Name} used the force and took an extra {attacker.Force} HP");
            }
            Console.WriteLine($"{defender.Name} has {Math.Max(defender.HP, 0)} HP left. \n ");
            Task.Delay(1000).Wait();

            var temp = attacker;
            attacker = defender;
            defender = temp;
        }
        var winner = hero1.HP > 0 ? hero1 : hero2;
        return winner;
    }
}