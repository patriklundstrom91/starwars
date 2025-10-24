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
    public int Force { get; set; }
    public int Strength { get; set; }
    public int HP { get; set; }
    public override string ToString()
    {
        return $"{Name}, {Gender}, Born: {BirthYear}, Height: {Height} cm, Mass: {Mass} kg ";
    }
    public void InitStats()
    {
        if (Mass.ToLower() == "unknown")
        {
            Mass = "60";
        }
        if (Height.ToLower() == "unknkown")
        {
            Height = "170";
        }
        if (int.Parse(Height) < 70)
        {
            Force = 20;
        }
        else if (int.Parse(Height) < 100)
        {
            Force = 15;
        }
        HP = 100;
        Strength = int.Parse(Mass) / 10;
    }
}
