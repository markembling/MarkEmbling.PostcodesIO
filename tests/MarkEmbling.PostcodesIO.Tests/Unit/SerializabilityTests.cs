using MarkEmbling.PostcodesIO.Data;
using NUnit.Framework;
using System.Text.Json;

namespace MarkEmbling.PostcodesIO.Tests.Unit
{
    [TestFixture]
    public class SerializabilityTests
    {
        static byte[] Serialize(object o)
        {
            if (o == null) return null;
            return JsonSerializer.SerializeToUtf8Bytes(o);
        }

        static T Deserialize<T>(byte[] bytes)
        {
            if (bytes == null || bytes.Length == 0) return default;
            return JsonSerializer.Deserialize<T>(bytes);
        }

        [Test]
        public void PostcodeData_should_be_serializable()
        {
            var expected = new PostcodeData
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
                Codes = new PostcodeCodesData {Parish = "Parish", CCG = "CCG", AdminDistrict = "AdminDistrict", AdminWard = "AdminWard", AdminCounty = "AdminCounty"}
            };

            var expectedBytes = Serialize(expected);
            var actual = Deserialize<PostcodeData>(expectedBytes);

            Assert.That(actual.Postcode, Is.EqualTo(expected.Postcode));
            Assert.That(actual.Eastings, Is.EqualTo(expected.Eastings));
            Assert.That(actual.Latitude, Is.EqualTo(expected.Latitude));
            Assert.That(actual.Codes.AdminCounty, Is.EqualTo(expected.Codes.AdminCounty));
        }

        [Test]
        public void OutcodeData_should_be_serializable()
        {
            var expected = new OutcodeData
            {
                AdminCounty = ["AdminCounty"],
                AdminDistrict = ["AdminDistrict"],
                AdminWard = ["AdminWard"],
                Country = ["Country"],
                Eastings = 10000,
                Latitude = 53.9999,
                Longitude = -3.9999,
                Northings = 20000,
                Outcode = "OUT",
                Parish = ["Parish"]
            };
            var expectedBytes = Serialize(expected);
            var actual = Deserialize<OutcodeData>(expectedBytes);

            Assert.That(actual.AdminCounty, Is.EqualTo(expected.AdminCounty));
            Assert.That(actual.Eastings, Is.EqualTo(expected.Eastings));
            Assert.That(actual.Latitude, Is.EqualTo(expected.Latitude));
            Assert.That(actual.Outcode, Is.EqualTo(expected.Outcode));
        }

        [Test]
        public void TerminatedPostcodeData_should_be_serializable()
        {
            var expected = new TerminatedPostcodeData
            {
                Latitude = 53.9999,
                Longitude = -3.9999,
                MonthTerminated = 1,
                Postcode = "Postcode",
                YearTerminated = 2000
            };
            var expectedBytes = Serialize(expected);
            var actual = Deserialize<TerminatedPostcodeData>(expectedBytes);

            Assert.That(actual.Latitude, Is.EqualTo(expected.Latitude));
            Assert.That(actual.MonthTerminated, Is.EqualTo(expected.MonthTerminated));
            Assert.That(actual.Postcode, Is.EqualTo(expected.Postcode));
        }
    }
}