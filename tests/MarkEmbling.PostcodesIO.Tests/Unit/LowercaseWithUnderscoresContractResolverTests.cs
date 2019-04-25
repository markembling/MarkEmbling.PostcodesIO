using MarkEmbling.PostcodesIO.Internals;
using NUnit.Framework;

namespace MarkEmbling.PostcodesIO.Tests.Unit
{
    [TestFixture]
    public class LowercaseWithUnderscoresContractResolverTests
    {
        LowercaseWithUnderscoresContractResolver _resolver;

        [SetUp]
        public void Setup()
        {
            _resolver = new LowercaseWithUnderscoresContractResolver();
        }

        [Test]
        public void Resolves_simple_property_name()
        {
            var result = _resolver.GetResolvedPropertyName("Foo");
            Assert.AreEqual("foo", result);
        }

        [Test]
        public void Resolves_multi_word_property_name()
        {
            var result = _resolver.GetResolvedPropertyName("FooBar");
            Assert.AreEqual("foo_bar", result);
        }

        [Test]
        public void Numbers_count_as_words()
        {
            var result = _resolver.GetResolvedPropertyName("Number1");
            Assert.AreEqual("number_1", result);
        }

        [Test]
        public void Numbers_in_between_words_get_underscores_either_side()
        {
            var result = _resolver.GetResolvedPropertyName("Number1Thing");
            Assert.AreEqual("number_1_thing", result);
        }

        [Test]
        public void Multiple_digit_numbers_dont_get_split()
        {
            var result = _resolver.GetResolvedPropertyName("Number123Thing");
            Assert.AreEqual("number_123_thing", result);
        }

        [Test]
        public void Multiple_sequential_uppercase_characters_should_not_be_treated_as_separate_words()
        {
            var result = _resolver.GetResolvedPropertyName("HTML");
            Assert.AreEqual("html", result);
        }
    }
}
