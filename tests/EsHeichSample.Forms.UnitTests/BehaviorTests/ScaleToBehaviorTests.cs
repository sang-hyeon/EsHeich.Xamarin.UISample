

namespace EsHeichSample.Forms.UnitTests.BehaviorTests
{
    using System;
    using Xamarin.Forms;

    using Xunit;
    using FluentAssertions;
    using AutoFixture.Xunit2;

    public class ScaleToBehaviorTests
    {
        class TestScaleToBehavior : ScaleToBehavior<View, double>
        {
            protected override double ConvertCurrentValue()
            {
                return this.Percentage;
            }
        }


        /// <summary>
        /// Percentage는 Setter를 통한 값 대입에 올바르게 작동합니다.
        /// </summary>
        [Theory]
        [InlineData(0.5, 0.5, false)]
        [InlineData(1, 1, false)]
        [InlineData(1.5, 0, true)]
        [InlineData(500, 0, true)]
        [InlineData(-100, 0, true)]
        public void Percentage_should_replace_by_set_correctly(
            double value, double expectedValue, bool throwException)
        {
            //Arrange
            var sut = new TestScaleToBehavior();
            var throwed = false;

            //Act
            try
            {
                sut.Percentage = value;
            }catch(ArgumentException e)
            {
                throwed = true;
            }

            //Assert
            throwed.Should().Be(throwException);
            sut.Percentage.Should().Be(expectedValue);
        }

        /// <summary>
        /// Percentage는 Setter를 통한 값 대입에 지정된 BindableProperty의 값을 올바르게 변경합니다.
        /// </summary>
        [Theory]
        [InlineAutoData(typeof(Button), "Button.Opacity", 0.5d, 0.5d, 0, 1, 0, 1)]
        [InlineAutoData(typeof(Label), "Label.Opacity", 0.5d, 0.5d, 0, 1, 0, 1)]
        public void Percentage_changes_property_of_parent_correctly(
            Type ViewType, string targetProperty, double percentage, double expectedValue,
            double startAt, double endAt, double from, double to, Easing easing)
        {
            //Arrange
            var propertyType = new BindablePropertyConverter()
                                                .ConvertFromInvariantString(targetProperty) as BindableProperty;

            var control = Activator.CreateInstance(ViewType) as View;
            var sut = new TestScaleToBehavior
            {
                TargetProperty = propertyType,
                Percentage = 0,
                StartAt = startAt,
                EndAt = endAt,
                From = from,
                To = to,
                Easing = easing
            };

            control.Behaviors.Add(sut);

            //Act
            sut.Percentage = percentage;

            //Assert
            var propertyValue = (double)control.GetValue(propertyType);
            propertyValue.Should().Be(expectedValue);
        }
    }
}
