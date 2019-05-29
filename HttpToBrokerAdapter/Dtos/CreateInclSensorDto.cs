namespace HttpToBrokerAdapter.Dtos
{
    public class CreateInclSensorDto
    {
        public int UID { get; set; }
        public int ST { get; set; }
        public int PER { get; set; }
        public int VOLT { get; set; }
        public int CSQ { get; set; }
        public string[] X { get; set; }
        public string[] Y { get; set; }
        public string[] T { get; set; }
        public string[] TS { get; set; }
    }
}
