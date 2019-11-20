
namespace EsHeichSample.Client.UnitTests.ExtensionsTests
{
    using Xunit;
    using Moq;
    using FluentAssertions;
    using AutoFixture;
    using AutoFixture.Xunit2;

    public class ComparableExtensionTests
    {

        [Theory]
        [InlineData(5, 10, 15, true)]
        [InlineData(5, 15, 15, true)]
        [InlineData(50, 15, 15, false)]
        [InlineData(98, 90, 100, false)]
        public void IsInRange_does_return_correctly_when_value_is_integer
            (int min, int value, int max, bool expect)
        {
            //Arrange
            //Act            
            var inRange = value.IsInRange(min, max);

            //Assert
            inRange.Should().Be(expect);
        }

        [Theory]
        [InlineData(5, 10, 15, true)]
        [InlineData(5, 15, 15, true)]
        [InlineData(50, 15, 15, false)]
        [InlineData(98, 90, 100, false)]
        public void IsInRange_does_return_correctly_when_value_is_float
            (float min, float value, float max, bool expect)
        {
            //Arrange
            //Act            
            var inRange = value.IsInRange(min, max);

            //Assert
            inRange.Should().Be(expect);
        }

        [Theory]
        [InlineData(5, 10, 15, true)]
        [InlineData(5, 15, 15, true)]
        [InlineData(50, 15, 15, false)]
        [InlineData(98, 90, 100, false)]
        public void IsInRange_does_return_correctly_when_value_is_double
            (double min, double value, double max, bool expect)
        {
            //Arrange
            //Act            
            var inRange = value.IsInRange(min, max);

            //Assert
            inRange.Should().Be(expect);
        }
    }
}
