using System.Text.Json.Serialization;

public class Hero
{
    [JsonPropertyName("name")]
    public string Name { get; set; }
    [JsonPropertyName("gender")]
    public string Gender { get; set; }
    [JsonPropertyName("birth_year")]
    public string BirthYear { get; set; }
    [JsonPropertyName("height")]
    public string Height { get; set; }
    [JsonPropertyName("mass")]
    public string Mass { get; set; }
    public int Force { get; set; }
    public override string ToString()
    {
        return $"{Name}, {Gender}, Born: {BirthYear}, Height: {Height} cm, Mass: {Mass} kg ";
    }
}
