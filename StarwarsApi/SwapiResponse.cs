using System.Collections.Generic;
using System.Text.Json.Serialization;
public class SwapiResponse
{
    public List<Hero> results { get; set; }
    public string next { get; set; }
}