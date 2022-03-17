using Newtonsoft.Json;

namespace CosmosDbTest.Model
{
    public class HighestScores
    {
        public Player Player { get; set; }

        public string Score { get; set; }
    }
}