using System.Text.Json.Serialization;
using Microsoft.VisualBasic;

public class Hero
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }
    [JsonPropertyName("gender")]
    public string? Gender { get; set; }
    [JsonPropertyName("birth_year")]
    public string? BirthYear { get; set; }
    [JsonPropertyName("height")]
    public string? Height { get; set; }
    [JsonPropertyName("mass")]
    public string? Mass { get; set; }
    public int? Force { get; set; }
    public int Strength { get; set; }
    public int HP { get; set; }
    public override string ToString()
    {
        return $"{Name}, {Gender}, Born: {BirthYear}, Height: {Height} cm, Mass: {Mass} kg ";
    }
    public void InitStats()
    {
        HP = 100;
        Strength = int.Parse(Mass) / 10;
    }
}
