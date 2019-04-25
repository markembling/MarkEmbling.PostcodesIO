using MarkEmbling.PostcodesIO.Internals;
using MarkEmbling.PostcodesIO.Resources;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MarkEmbling.PostcodesIO.Tests.Integration.OutwardCodes
{
    [TestFixture, Explicit("Hits live Postcodes.io API")]
    public class OutwardCodesReverseGeocodeTests
    {
        private OutwardCodesResource _outcodes;

        [SetUp]
        public void Setup()
        {
            _outcodes = new OutwardCodesResource(new RequestExecutor("https://api.postcodes.io"));
        }

        [Test]
        public void ReverseGeocode_returns_populated_response()
        {
            var results = _outcodes.ReverseGeocode(new ReverseGeocodeQuery
            {
                Latitude = 51.2430302947367,
                Longitude = -0.564888615311003
            });
            Assert.True(results.Any());
        }

        [Test]
        public async Task ReverseGeocodeAsync_returns_populated_response()
        {
            var results = await _outcodes.ReverseGeocodeAsync(new ReverseGeocodeQuery
            {
                Latitude = 51.2430302947367,
                Longitude = -0.564888615311003
            });
            Assert.True(results.Any());
        }

        [Test]
        public void ReverseGeocode_with_limit_returns_limited_results()
        {
            var results = _outcodes.ReverseGeocode(new ReverseGeocodeQuery
            {
                Latitude = 51.2430302947367,
                Longitude = -0.564888615311003,
                Limit = 2
            });
            Assert.AreEqual(2, results.Count());
        }

        [Test]
        public async Task ReverseGeocodeAsync_with_limit_returns_limited_results()
        {
            var results = await _outcodes.ReverseGeocodeAsync(new ReverseGeocodeQuery
            {
                Latitude = 51.2430302947367,
                Longitude = -0.564888615311003,
                Limit = 2
            });
            Assert.AreEqual(2, results.Count());
        }

        [Test]
        public void ReverseGeocode_with_wide_radius_gives_more_results()
        {
            var results = _outcodes.ReverseGeocode(new ReverseGeocodeQuery
            {
                Latitude = 51.2430302947367,
                Longitude = -0.564888615311003,
                Radius = 10000
            });
            Assert.AreEqual(10, results.Count());
        }

        [Test]
        public async Task ReverseGeocodeAsync_with_wide_radius_gives_more_results()
        {
            var results = await _outcodes.ReverseGeocodeAsync(new ReverseGeocodeQuery
            {
                Latitude = 51.2430302947367,
                Longitude = -0.564888615311003,
                Radius = 10000
            });
            Assert.AreEqual(10, results.Count());
        }

        [Test]
        public void ReverseGeocode_throws_if_WideSearch_has_value()
        {
            Assert.Throws<InvalidOperationException>(
                () => _outcodes.ReverseGeocode(new ReverseGeocodeQuery
                {
                    Latitude = 51.2430302947367,
                    Longitude = -0.564888615311003,
                    WideSearch = false
                }),
                "WideSearch is not supported for outward code reverse geocoding");

            Assert.Throws<InvalidOperationException>(
                () => _outcodes.ReverseGeocode(new ReverseGeocodeQuery
                {
                    Latitude = 51.2430302947367,
                    Longitude = -0.564888615311003,
                    WideSearch = true
                }),
                "WideSearch is not supported for outward code reverse geocoding");
        }
    }
}