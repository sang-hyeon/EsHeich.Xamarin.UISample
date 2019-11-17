
namespace EsHeichSample.Client.UnitTests.ViewModelTests
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Xunit;
    using Moq;
    using FluentAssertions;
    using AutoFixture;
    using AutoFixture.Xunit2;

    using EsHeichSample.Client.Models;
    using EsHeichSample.Client.ViewModels;
    using EsHeichSample.Client.Services;
    using System.Linq;
    using System.ComponentModel.DataAnnotations;

    public class HomeViewModelTests
    {
        [Theory]
        [InlineAutoData(false)]
        [InlineAutoData(true, null)]
        public void Construct_correctly(bool needThrowException, Mock<IHeroService> heroService)
        {
            //Arrange
            Action creating = () => new HomeViewModel(heroService.Object);
            bool exception = false;

            //Action
            try
            {
                creating.Invoke();
            }
            catch
            {
                exception = true;
            }
            finally
            {
                //Assert
                exception.Should().Be(needThrowException);
            }
        }

        [Theory]
        [AutoData]
        public void Constructor_loads_heroes_correctly(Hero[] heroes, Mock<IHeroService> heroService)
        {
            //Arrange
            heroService.Setup(x => x.GetDcHeroesAsync(It.IsAny<CancellationToken>()))
                                .Returns(Task.FromResult(heroes));

            //Action
            var sut = new HomeViewModel(heroService.Object);

            //Action
            sut.Heroes.Select(x=> x.RealName)
                .Should().BeEquivalentTo(
                                    heroes.Select(x=> x.RealName));
        }
    }
}
