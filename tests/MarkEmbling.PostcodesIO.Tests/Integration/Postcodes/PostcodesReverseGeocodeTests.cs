using MarkEmbling.PostcodesIO.Internals;
using MarkEmbling.PostcodesIO.Resources;
using MarkEmbling.PostcodesIO.Results;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarkEmbling.PostcodesIO.Tests.Integration.Postcodes
{
    [TestFixture, Explicit("Hits live Postcodes.io API")]
    public class PostcodesReverseGeocodeTests {
        private PostcodesResource _postcodes;

        [SetUp]
        public void Setup() {
            _postcodes = new PostcodesResource(new RequestExecutor("https://api.postcodes.io"));
        }

        [Test]
        public void ReverseGeocode_simple_query_returns_populated_response()
        {
            var results = _postcodes.ReverseGeocode(new ReverseGeocodeQuery
            {
                Latitude = 51.2452924089757,
                Longitude = -0.58231794275613
            }).ToList();

            AssertPopulatedResponse(results);
        }

        [Test]
        public async Task ReverseGeocodeAsync_simple_query_returns_populated_response()
        {
            var results = (await _postcodes.ReverseGeocodeAsync(new ReverseGeocodeQuery
            {
                Latitude = 51.2452924089757,
                Longitude = -0.58231794275613
            })).ToList();

            AssertPopulatedResponse(results);
        }

        [Test]
        public void ReverseGeocode_with_limit_returns_only_that_number_of_results() {
            var results = _postcodes.ReverseGeocode(new ReverseGeocodeQuery
            {
                Latitude = 51.2452924089757,
                Longitude = -0.58231794275613,
                Limit = 2
            }).ToList();

            Assert.AreEqual(2, results.Count);
        }

        [Test]
        public async Task ReverseGeocodeAsync_with_limit_returns_only_that_number_of_results()
        {
            var results = (await _postcodes.ReverseGeocodeAsync(new ReverseGeocodeQuery
            {
                Latitude = 51.2452924089757,
                Longitude = -0.58231794275613,
                Limit = 2
            })).ToList();

            Assert.AreEqual(2, results.Count);
        }

        // TODO: tests for radius and wideSearch. Probably better as unit tests.

        private static void AssertPopulatedResponse(List<PostcodeResult> results)
        {
            Assert.True(results.Any());
            //TODO probably a better way of writing this without using Assert.IsTrue
            Assert.IsTrue(results.Any(p => p.Postcode == "GU1 1AA"));
        }
    }
}