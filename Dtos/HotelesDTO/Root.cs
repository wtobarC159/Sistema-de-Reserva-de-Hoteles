namespace Sistema_de_Reserva_de_Hoteles.Dtos.HotelesDTO
{
    public class Address
    {
        public string countryCode { get; set; }
    }

    public class Datum
    {
        public string chainCode { get; set; }
        public string iataCode { get; set; }
        public int dupeId { get; set; }
        public string name { get; set; }
        public string hotelId { get; set; }
        public GeoCode geoCode { get; set; }
        public Address address { get; set; }
        public Distance distance { get; set; }
    }

    public class Distance
    {
        public double value { get; set; }
        public string unit { get; set; }
    }

    public class GeoCode
    {
        public double latitude { get; set; }
        public double longitude { get; set; }
    }

    public class Links
    {
        public string self { get; set; }
    }

    public class Meta
    {
        public int count { get; set; }
        public Links links { get; set; }
    }

    public class Root
    {
        public List<Datum> data { get; set; }
        public Meta meta { get; set; }
    }
}
