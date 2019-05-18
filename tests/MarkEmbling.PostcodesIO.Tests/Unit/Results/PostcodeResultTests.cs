using MarkEmbling.PostcodesIO.Results;
using Newtonsoft.Json;
using NUnit.Framework;

namespace MarkEmbling.PostcodesIO.Tests.Unit.Results
{
    [TestFixture]
    public class PostcodeResultTests
    {
        [Test]
        public void NHSHealthAuthority_has_custom_JSON_property_name()
        {
            var attrib = TestHelpers.GetPropertyAttribute<JsonPropertyAttribute>(typeof(PostcodeResult), "NHSHealthAuthority");
            Assert.IsNotNull(attrib);
            Assert.AreEqual("nhs_ha", attrib.PropertyName);
        }

        [Test]
        public void InCode_has_custom_JSON_property_name()
        {
            var attrib = TestHelpers.GetPropertyAttribute<JsonPropertyAttribute>(typeof(PostcodeResult), "InCode");
            Assert.IsNotNull(attrib);
            Assert.AreEqual("incode", attrib.PropertyName);
        }

        [Test]
        public void OutCode_has_custom_JSON_property_name()
        {
            var attrib = TestHelpers.GetPropertyAttribute<JsonPropertyAttribute>(typeof(PostcodeResult), "OutCode");
            Assert.IsNotNull(attrib);
            Assert.AreEqual("outcode", attrib.PropertyName);
        }
    }
}
