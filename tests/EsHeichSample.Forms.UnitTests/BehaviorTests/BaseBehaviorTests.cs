
namespace EsHeichSample.Forms.UnitTests.BehaviorTests
{
    using Xunit;
    using Xamarin.Forms;
    using AutoFixture.Xunit2;
    using FluentAssertions;

    public class BaseBehaviorTests
    {
        class TestBehavior : BehaviorBase<View>
        {

        }

        [Theory, AutoData]
        public void AssociatedObject_should_be_parent_instance(object bindingContext)
        {
            //Arrange
            var sut = new TestBehavior();
            var control = new Button { BindingContext = bindingContext };

            //Act
            control.Behaviors.Add(sut);

            //Assert
            sut.AssociatedObject.Should().Be(control);
        }
    }
}
