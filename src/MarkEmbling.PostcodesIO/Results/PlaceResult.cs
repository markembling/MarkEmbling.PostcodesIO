using RestSharp.Deserializers;
using System;

namespace MarkEmbling.PostcodesIO.Results
{
    [Serializable]
    public class PlaceResult
    {
        public string Code { get; set; }
        public int Eastings { get; set; }
        public int Northings { get; set; }
        public int MinNorthings { get; set; }
        public int MinEastings { get; set; }
        public int MaxNorthings { get; set; }
        public int MaxEastings { get; set; }
        public string Country { get; set; }
        public double Longitude { get; set; }
        public double Latituide { get; set; }
        public string LocalType { get; set; }
        public string Name1 { get; set; }

        [DeserializeAs(Name = "name_1_lang")]
        public string Name1Language { get; set; }

        public string Name2 { get; set; }

        [DeserializeAs(Name = "name_2_lang")]
        public string Name2Language { get; set; }

        public string CountryUnitary { get; set; }
        public string CountryUnitaryType { get; set; }
        public string DistrictBorough { get; set; }
        public string DistrictBoroughType { get; set; }
        public string Region { get; set; }
    }
}
