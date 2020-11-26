using System;

namespace MarkEmbling.PostcodesIO.Results
{
    [Serializable]
    public class NearestResult : PostcodeResult
    {
        public double Distance { get; set; }
    }
}
