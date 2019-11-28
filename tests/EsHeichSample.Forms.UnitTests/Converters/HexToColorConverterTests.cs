
namespace EsHeichSample.Forms.UnitTests.Converters
{
    using System;
    using System.Globalization;
    using Xamarin.Forms;

    using Xunit;
    using FluentAssertions;
    using AutoFixture.Xunit2;

    public class HexToColorConverterTests
    {

        [Theory]
        [InlineAutoData("#FF000000")]
        [InlineAutoData("#FFFFFFFF")]
        [InlineAutoData("#FFFFFF00")]
        public void Convert_does_return_Color_correctly(
            string hex, Type targetType, object parameter, CultureInfo culture)
        {
            //Arrange
            var sut = new HexToColorConverter();

            //Act
            var convertedValue = (Color)sut.Convert(hex, targetType, parameter, culture);

            //Assert
            convertedValue.ToHex().Should().Be(hex);
        }
    }
}
