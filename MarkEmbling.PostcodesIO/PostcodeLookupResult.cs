using System.CodeDom;
using RestSharp.Deserializers;

namespace MarkEmbling.PostcodesIO {
    public class PostcodeLookupResult {
        public string Postcode { get; set; }
        public int Quality { get; set; }
        public int Eastings { get; set; }
        public int Northings { get; set; }
        public string Country { get; set; }

        [DeserializeAs(Name = "nhs_ha")]
        public string NHSHealthAuthority { get; set; }

        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string ParliamentaryConstituency { get; set; }
        public string EuropeanElectoralRegion { get; set; }
        public string PrimaryCareTrust { get; set; }
        public string Region { get; set; }
        public string LSOA { get; set; }
        public string MSOA { get; set; }
        public string NUTS { get; set; }
        public string InCode { get; set; }
        public string OutCode { get; set; }
        public string AdminDistrict { get; set; }
        public string Parish { get; set; }
        public string AdminCounty { get; set; }
        public string AdminWard { get; set; }
        public string CCG { get; set; }
        public Codes Codes { get; set; }

        /*
{
    "status": 200,
    "result": {
        "postcode": "GU1 1AA",
        "quality": 1,
        "eastings": 499050,
        "northings": 150523,
        "country": "England",
        "nhs_ha": "South East Coast",
        "longitude": -0.58231794275613,
        "latitude": 51.2452924089757,
        "parliamentary_constituency": "Guildford",
        "european_electoral_region": "South East",
        "primary_care_trust": "Surrey",
        "region": "South East",
        "lsoa": "Guildford 015A",
        "msoa": "Guildford 015",
        "nuts": null,
        "incode": "1AA",
        "outcode": "GU1",
        "admin_district": "Guildford",
        "parish": null,
        "admin_county": "Surrey",
        "admin_ward": "Friary and St Nicolas",
        "ccg": "NHS Guildford and Waverley",
        "codes": {
            "admin_district": "E07000209",
            "admin_county": "E10000030",
            "admin_ward": "E05007293",
            "parish": "E43000138",
            "ccg": "E38000067"
        }
    }
}
         */
    }

    public class Codes {
        public string AdminDistrict { get; set; }
        public string AdminCounty { get; set; }
        public string AdminWard { get; set; }
        public string Parish { get; set; }
        public string CCG { get; set; }
    }
}