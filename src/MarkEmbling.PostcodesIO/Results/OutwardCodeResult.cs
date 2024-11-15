using System;
using System.Collections.Generic;

namespace MarkEmbling.PostcodesIO.Results
{
    [Serializable]
    public class OutwardCodeResult {
        public string Outcode { get; set; }
        public int Eastings { get; set; }
        public int Northings { get; set; }
        public List<string> AdminCounty { get; set; }
        public List<string> AdminDistrict { get; set; }
        public List<string> AdminWard { get; set; }
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }
        public List<string> Country { get; set; }
        public List<string> Parish { get; set; }
    }
}