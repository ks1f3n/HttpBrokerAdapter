namespace HttpToBrokerAdapter.Dtos
{
    public class CreateGapSensorDto
    {
        public int ID { get; set; }
        public int ST { get; set; }
        public int PER { get; set; }
        public int VOLT { get; set; }
        public int CSQ { get; set; }
        public string[] v { get; set; }
        public string[] t { get; set; }
    }
}
