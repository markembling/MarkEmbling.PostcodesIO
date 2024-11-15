using System;

namespace MarkEmbling.PostcodesIO.Data
{
    [Serializable]
    public class NearestResult : PostcodeData
    {
        public double Distance { get; set; }
    }
}
