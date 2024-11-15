using System;

namespace MarkEmbling.PostcodesIO.Data
{
    [Serializable]
    public class PostcodeCodesData
    {
        public string AdminDistrict { get; set; }
        public string AdminCounty { get; set; }
        public string AdminWard { get; set; }
        public string Parish { get; set; }
        public string CCG { get; set; }
        public string CCGId { get; set; }
        public string CED { get; set; }
        public string NUTS { get; set; }
        public string LAU2 { get; set; }
        public string LSOA { get; set; }
        public string MSOA { get; set; }
        public string PFA { get; set; }
    }
}