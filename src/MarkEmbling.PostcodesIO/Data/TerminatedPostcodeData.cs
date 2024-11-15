using System;

namespace MarkEmbling.PostcodesIO.Data
{
    [Serializable]
    public class TerminatedPostcodeData
    {
        public string Postcode { get; set; }
        public int YearTerminated { get; set; }
        public int MonthTerminated { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
    }
}