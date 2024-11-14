﻿using System;
using System.Text.Json.Serialization;

namespace MarkEmbling.PostcodesIO.Results
{
    [Serializable]
    public class PostcodeResult {
        public string Postcode { get; set; }
        public int Quality { get; set; }
        public int Eastings { get; set; }
        public int Northings { get; set; }
        public string Country { get; set; }

        [JsonPropertyName("nhs_ha")]
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

        [JsonPropertyName("incode")]
        public string InCode { get; set; }

        [JsonPropertyName("outcode")]
        public string OutCode { get; set; }

        public string AdminDistrict { get; set; }
        public string Parish { get; set; }
        public string AdminCounty { get; set; }
        public string AdminWard { get; set; }
        public string CCG { get; set; }
        public Codes Codes { get; set; }
    }

    [Serializable]
    public class Codes {
        public string AdminDistrict { get; set; }
        public string AdminCounty { get; set; }
        public string AdminWard { get; set; }
        public string Parish { get; set; }
        public string CCG { get; set; }
    }
}