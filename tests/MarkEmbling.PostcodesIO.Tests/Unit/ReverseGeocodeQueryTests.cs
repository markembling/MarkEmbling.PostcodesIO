using NUnit.Framework;

namespace MarkEmbling.PostcodesIO.Tests.Unit
{
    [TestFixture]
    public class ReverseGeocodeQueryTests {
        [Test]
        public void Similar_instances_should_be_considered_equal() {
            var q1 = new ReverseGeocodeQuery {Latitude = 51.2452924089757, Longitude = -0.58231794275613};
            var q2 = new ReverseGeocodeQuery {Latitude = 51.2452924089757, Longitude = -0.58231794275613};
            Assert.That(q1, Is.EqualTo(q2));
        }

        [Test]
        public void Different_instances_should_not_be_considered_equal() {
            var q1 = new ReverseGeocodeQuery {Latitude = 51.2452924089757, Longitude = -0.58231794275613};
            var q2 = new ReverseGeocodeQuery {Latitude = 51.2571984465953, Longitude = -0.567549033067429};
            Assert.That(q2, Is.Not.EqualTo(q1));
        }

        [Test]
        public void Similar_queries_but_with_different_limit_should_not_be_considered_equal() {
            var q1 = new ReverseGeocodeQuery {Latitude = 51.2452924089757, Longitude = -0.58231794275613};
            var q2 = new ReverseGeocodeQuery {Latitude = 51.2452924089757, Longitude = -0.58231794275613, Limit = 1};
            Assert.That(q1, Is.Not.EqualTo(q2));
        }

        [Test]
        public void Similar_queries_but_with_different_radius_should_not_be_considered_equal() {
            var q1 = new ReverseGeocodeQuery { Latitude = 51.2452924089757, Longitude = -0.58231794275613 };
            var q2 = new ReverseGeocodeQuery {Latitude = 51.2452924089757, Longitude = -0.58231794275613, Radius = 1};
            Assert.That(q1, Is.Not.EqualTo(q2));
        }

        [Test]
        public void Similar_queries_but_with_different_widesearch_should_not_be_considered_equal() {
            var q1 = new ReverseGeocodeQuery {Latitude = 51.2452924089757, Longitude = -0.58231794275613};
            var q2 = new ReverseGeocodeQuery {
                Latitude = 51.2452924089757,
                Longitude = -0.58231794275613,
                WideSearch = true
            };
            Assert.That(q1, Is.Not.EqualTo(q2));
        }
    }
}