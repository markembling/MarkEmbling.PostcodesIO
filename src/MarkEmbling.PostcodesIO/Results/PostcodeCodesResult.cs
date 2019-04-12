using System;

namespace MarkEmbling.PostcodesIO.Results
{
    [Serializable]
    public class PostcodeCodesResult {
        public string AdminDistrict { get; set; }
        public string AdminCounty { get; set; }
        public string AdminWard { get; set; }
        public string Parish { get; set; }
        public string CCG { get; set; }
        public string NUTS { get; set; }
    }
}