
namespace EsHeichSample.Client.UnitTests.ViewModelTests
{
    using System;

    using Xunit;
    using Moq;
    using FluentAssertions;
    using AutoFixture.Xunit2;

    using EsHeichSample.Client.Models;
    using EsHeichSample.Client.ViewModels;

    public class HeroViewModelTest
    {

        /// <summary>
        /// 생성자는 올바르게 동작합니다.
        /// </summary>
        [Theory]
        [InlineAutoData(false)]
        [InlineAutoData(true, null)]
        public void Construct_correctly(bool throwException, Hero hero)
        {
            //Arrange
            Action creating = ()=> new HeroViewModel(hero);
            bool exception = false;

            //Act
            try {
                creating.Invoke();
            }
            catch {
                exception = true;
            }
            finally
            {
                //Assert
                exception.Should().Be(throwException);
            }
        }

        /// <summary>
        /// 생성자는 올바르게 멤버 속성을 초기화 합니다.
        /// </summary>
        [Theory]
        [AutoData]
        public void Constructor_initialize_properties_correctly(Hero hero)
        {
            //Arrange
            HeroViewModel sut = default;

            //Act
            sut = new HeroViewModel(hero);

            //Assert
            sut.HeroName.Should().Be(hero.HeroName);
            sut.HeroName_ko.Should().Be(hero.HeroName_ko);
            sut.RealName.Should().Be(hero.RealName);
            sut.Comment.Should().Be(hero.Summary);
            sut.Role.Should().Be(hero.Role);
            sut.SignatureColor.Should().Be(hero.SignatureColor_Hex);
        }
    }
}
