using System;

namespace MarkEmbling.PostcodesIO.Results {
    [Serializable]
    public class TerminatedPostcodeResult
    {
        public string Postcode { get; set; }
        public int YearTerminated { get; set; }
        public int MonthTerminated { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
    }
}