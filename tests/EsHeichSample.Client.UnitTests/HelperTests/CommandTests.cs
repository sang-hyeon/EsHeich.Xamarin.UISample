
namespace EsHeichSample.Client.UnitTests.HelperTests
{
    using System;

    using Xunit;
    using Moq;
    using FluentAssertions;
    using AutoFixture;
    using AutoFixture.Xunit2;

    using EsHeichSample.Client;

    public class CommandTests
    {

        [Theory]
        [InlineAutoData(false, typeof(object))]
        [InlineAutoData(true, typeof(MockData))]
        public void Execute_does_invoke_action_correctly(bool executed, Type paramType)
        {
            //Arrange
            var observer = false;
            var param = Activator.CreateInstance(paramType);
            var action = new Action<MockData>((x) => observer = true);
            var sut = new Command<MockData>(action);

            //Action
            sut.Execute(param);

            //Assert
            observer.Should().Be(executed);
        }

        [Theory]
        [InlineAutoData(false, typeof(object))]
        [InlineAutoData(true, typeof(MockData))]
        public void CanExecute_does_return_correctly(bool expect, Type paramType)
        {
            //Arrange
            var param = Activator.CreateInstance(paramType);
            var action = new Action<MockData>((x) => { });
            var canExcute = new Func<MockData,bool>((x) => true);
            var sut = new Command<MockData>(action, canExcute);

            //Action
            var can = sut.CanExecute(param);

            //Assert
            can.Should().Be(expect);
        }

        [Fact]
        public void raise_CanExecuteChanged_if_can_execute_changed()
        {
            //Arrange
            var action = new Action<MockData>((x) => { });
            var canExcute = new Func<MockData, bool>((x) => true);
            var sut = new Command<MockData>(action, canExcute);

            using(var mt = sut.Monitor())
            {
                //Action
                sut.RaiseCanExecuteChanged();

                //Aseert
                mt.Should().Raise(nameof(Command.CanExecuteChanged));
            }
        }

        [Theory]
        [AutoData]
        public void replace_parameters_of_Constructor(object param)
        {
            //Arrange
            var observer = false;
            var observer_canExecute = false;

            var action = new Action(() => observer = true);
            var canExcute = new Func<bool>(() => { observer_canExecute = true; return true; });
            var sut = new Command(action, canExcute);

            //Action
            sut.CanExecute(param);
            sut.Execute(param);

            //Assert
            observer.Should().BeTrue();
            observer_canExecute.Should().BeTrue();
        }

        public class MockData
        {
        }
    }
}
