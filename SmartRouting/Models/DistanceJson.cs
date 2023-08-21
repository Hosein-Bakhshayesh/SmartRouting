using System.Xml.Linq;

namespace SmartRouting.Models
{
    public class DistanceJson
    {
        public string status { get; set; }
        public row[] rows { get; set; }

    }

    public class row
    {
        public element[] elements { get; set; }
    }

    public class element
    {
        public string status { get; set; }
        public DistanceJs duration { get; set; }
        public DistanceJs distance { get; set; }
    }

    public class DistanceJs
    {
        public int value { get; set; }
        public string text { get; set; }
    }
}
