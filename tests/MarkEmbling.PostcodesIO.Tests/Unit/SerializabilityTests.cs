using MarkEmbling.PostcodesIO.Results;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace MarkEmbling.PostcodesIO.Tests.Unit
{
    [TestFixture]
    public class SerializabilityTests
    {
        static byte[] Serialize(object o)
        {
            if (o == null)
            {
                return null;
            }

            var binaryFormatter = new BinaryFormatter();
            using (var memoryStream = new MemoryStream())
            {
                binaryFormatter.Serialize(memoryStream, o);
                byte[] objectDataAsStream = memoryStream.ToArray();
                return objectDataAsStream;
            }
        }

        static T Deserialize<T>(byte[] stream)
        {
            if (stream == null)
            {
                return default(T);
            }

            var binaryFormatter = new BinaryFormatter();
            using (var memoryStream = new MemoryStream(stream))
            {
                T result = (T)binaryFormatter.Deserialize(memoryStream);
                return result;
            }
        }

        [Test]
        public void PostcodeResult_should_be_serializable()
        {
            var expected = new PostcodeResult
            {
                AdminCounty = "AdminCounty",
                AdminWard = "AdminWard",
                AdminDistrict = "AdminDistrict",
                CCG = "CCG",
                CED = "CED",
                Country = "Country",
                Eastings = 100000,
                Northings = 200000,
                EuropeanElectoralRegion = "EuropeanElectoralRegion",
                InCode = "InCode",
                LSOA = "LSOA",
                Latitude = 53.9999,
                Longitude = -3.9999,
                MSOA = "MSOA",
                NHSHealthAuthority = "NHSHealthAuthority",
                NUTS = "NUTS",
                OutCode = "OutCode",
                Parish = "Parish",
                ParliamentaryConstituency = "ParliamentaryConstituency",
                Postcode = "Postcode",
                PrimaryCareTrust = "PrimaryCareTrust",
                Quality = 1,
                Region = "Region",
                Codes = new PostcodeCodesResult {
                    Parish = "Parish",
                    CCG = "CCG",
                    AdminDistrict = "AdminDistrict",
                    AdminWard = "AdminWard",
                    AdminCounty = "AdminCounty"
                }
            };

            var expectedBytes = Serialize(expected);
            var actual = Deserialize<PostcodeResult>(expectedBytes);

            Assert.AreEqual(expected.Postcode, actual.Postcode);
            Assert.AreEqual(expected.Eastings, actual.Eastings);
            Assert.AreEqual(expected.Latitude, actual.Latitude);
            Assert.AreEqual(expected.Codes.AdminCounty, actual.Codes.AdminCounty);
        }

        [Test]
        public void OutwardCodeResult_should_be_serializable()
        {
            var expected = new OutwardCodeResult
            {
                AdminCounty = new List<string> { "AdminCounty" },
                AdminDistrict = new List<string> { "AdminDistrict" },
                AdminWard = new List<string> { "AdminWard" },
                Country = new List<string> { "Country" },
                Eastings = 10000,
                Latitude = 53.9999,
                Longitude = -3.9999,
                Northings = 20000,
                Outcode = "OUT",
                Parish = new List<string> { "Parish" }
            };
            var expectedBytes = Serialize(expected);
            var actual = Deserialize<OutwardCodeResult>(expectedBytes);

            Assert.AreEqual(expected.AdminCounty, actual.AdminCounty);
            Assert.AreEqual(expected.Eastings, actual.Eastings);
            Assert.AreEqual(expected.Latitude, actual.Latitude);
            Assert.AreEqual(expected.Outcode, actual.Outcode);
        }
    }
}