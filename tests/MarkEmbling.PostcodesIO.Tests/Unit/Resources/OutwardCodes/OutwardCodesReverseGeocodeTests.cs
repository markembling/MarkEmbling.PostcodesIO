using MarkEmbling.PostcodesIO.Internals;
using MarkEmbling.PostcodesIO.Resources;
using Moq;
using NUnit.Framework;

namespace MarkEmbling.PostcodesIO.Tests.Unit.Resources.OutwardCodes
{
    [TestFixture]
    public class OutwardCodesReverseGeocodeTests
    {
        private Mock<IRequestExecutor> _requestExecutorMock;
        private OutwardCodesResource _outcodes;

        [SetUp]
        public void Setup()
        {
            _requestExecutorMock = new Mock<IRequestExecutor>();
            _outcodes = new OutwardCodesResource(_requestExecutorMock.Object);
        }
    }
}
