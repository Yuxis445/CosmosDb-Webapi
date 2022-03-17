using Newtonsoft.Json;

namespace CosmosDbTest.Model
{
    public class VideoGame
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "lastCompetitionDate")]
        public DateTime LastCompetitionDate { get; set; }

        [JsonProperty(PropertyName = "tags")]
        public string[] Tags { get; set; }

        [JsonProperty(PropertyName = "platforms")]
        public string[]? Plataforms { get; set; }

        [JsonProperty(PropertyName = "levels")]
        public Level[] Levels { get; set; }

        [JsonProperty(PropertyName = "highestScores")]
        public HighestScores[] HighestScores { get; set; }

    }
}