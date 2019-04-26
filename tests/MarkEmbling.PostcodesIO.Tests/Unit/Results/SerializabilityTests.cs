using MarkEmbling.PostcodesIO.Results;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace MarkEmbling.PostcodesIO.Tests.Unit.Results
{
    [TestFixture]
    public class SerializabilityTests
    {
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

        [Test]
        public void TerminatedPostcodeResult_should_be_serializable()
        {
            var expected = new TerminatedPostcodeResult
            {
                Latitude = 53.9999,
                Longitude = -3.9999,
                MonthTerminated = 1,
                Postcode = "Postcode",
                YearTerminated = 2000
            };
            var expectedBytes = Serialize(expected);
            var actual = Deserialize<TerminatedPostcodeResult>(expectedBytes);

            Assert.AreEqual(expected.Latitude, actual.Latitude);
            Assert.AreEqual(expected.MonthTerminated, actual.MonthTerminated);
            Assert.AreEqual(expected.Postcode, actual.Postcode);
        }

        [Test]
        public void PlaceResult_should_be_serializable()
        {
            var expected = new PlaceResult
            {
                Code = "CODE",
                Country = "Country",
                CountyUnitary = "CountryUnitary",
                CountyUnitaryType = "CountryUnitaryType",
                DistrictBorough = "DistictBorough",
                DistrictBoroughType = "DistrictBoroughType",
                Eastings = 10000,
                Latitude = 53.9999,
                LocalType = "LocalType",
                Longitude = -3.9999,
                MaxEastings = 10000,
                MaxNorthings = 20000,
                MinEastings = 10000,
                MinNorthings = 20000,
                Name1 = "Name1",
                Name1Language = "Name1Language",
                Name2 = "Name2",
                Name2Language = "Name2Language",
                Northings = 20000,
                Region = "Region"
            };
            var expectedBytes = Serialize(expected);
            var actual = Deserialize<PlaceResult>(expectedBytes);

            Assert.AreEqual(expected.Code, actual.Code);
            Assert.AreEqual(expected.Country, actual.Country);
            Assert.AreEqual(expected.CountyUnitary, actual.CountyUnitary);
            Assert.AreEqual(expected.CountyUnitaryType, actual.CountyUnitaryType);
            Assert.AreEqual(expected.DistrictBorough, actual.DistrictBorough);
            Assert.AreEqual(expected.DistrictBoroughType, actual.DistrictBoroughType);
            Assert.AreEqual(expected.Eastings, actual.Eastings);
            Assert.AreEqual(expected.Latitude, actual.Latitude);
            Assert.AreEqual(expected.LocalType, actual.LocalType);
            Assert.AreEqual(expected.Longitude, actual.Longitude);
            Assert.AreEqual(expected.MaxEastings, actual.MaxEastings);
            Assert.AreEqual(expected.MaxNorthings, actual.MaxNorthings);
            Assert.AreEqual(expected.MinEastings, actual.MinEastings);
            Assert.AreEqual(expected.MinNorthings, actual.MinNorthings);
            Assert.AreEqual(expected.Name1, actual.Name1);
            Assert.AreEqual(expected.Name1Language, actual.Name1Language);
            Assert.AreEqual(expected.Name2, actual.Name2);
            Assert.AreEqual(expected.Name2Language, actual.Name2Language);
            Assert.AreEqual(expected.Northings, actual.Northings);
            Assert.AreEqual(expected.Region, actual.Region);
        }

        private static byte[] Serialize(object obj)
        {
            if (obj == null) return null;

            var binaryFormatter = new BinaryFormatter();
            using (var memoryStream = new MemoryStream())
            {
                binaryFormatter.Serialize(memoryStream, obj);
                byte[] objectDataAsStream = memoryStream.ToArray();
                return objectDataAsStream;
            }
        }

        private static T Deserialize<T>(byte[] stream)
        {
            if (stream == null) return default;

            var binaryFormatter = new BinaryFormatter();
            using (var memoryStream = new MemoryStream(stream))
            {
                T result = (T)binaryFormatter.Deserialize(memoryStream);
                return result;
            }
        }
    }
}