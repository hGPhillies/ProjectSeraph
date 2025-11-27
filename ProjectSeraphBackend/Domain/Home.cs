namespace ProjectSeraphBackend.Domain
{
    public class Home
    {
        public string StreetName { get; set; } = string.Empty;
        public string HouseNumber { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public int? FloorNumber { get; set; }
        public string? Door { get; set; }
    }
}
