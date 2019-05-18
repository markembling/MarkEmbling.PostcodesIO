using MarkEmbling.PostcodesIO.Results;
using Newtonsoft.Json;
using NUnit.Framework;

namespace MarkEmbling.PostcodesIO.Tests.Unit.Results
{
    [TestFixture]
    public class PlaceResultTests
    {
        [Test]
        public void Name1Language_has_custom_JSON_property_name()
        {
            var attrib = TestHelpers.GetPropertyAttribute<JsonPropertyAttribute>(typeof(PlaceResult), "Name1Language");
            Assert.IsNotNull(attrib);
            Assert.AreEqual("name_1_lang", attrib.PropertyName);
        }

        [Test]
        public void Name2Language_has_custom_JSON_property_name()
        {
            var attrib = TestHelpers.GetPropertyAttribute<JsonPropertyAttribute>(typeof(PlaceResult), "Name2Language");
            Assert.IsNotNull(attrib);
            Assert.AreEqual("name_2_lang", attrib.PropertyName);
        }
    }
}
