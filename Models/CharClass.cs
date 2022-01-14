using System.Text.Json.Serialization;

namespace try5000rpg.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum CharClass 
    {
        knight,
        cleric,
        mage
    }
}