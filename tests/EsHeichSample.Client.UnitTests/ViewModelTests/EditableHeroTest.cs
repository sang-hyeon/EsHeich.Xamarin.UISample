
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
    using System.Linq;
    using System.ComponentModel.DataAnnotations;

    public class EditableHeroTest
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
            Action creating = () => new EditableHeroViewModel(hero);
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
            EditableHeroViewModel sut = default;

            //Action
            sut = new EditableHeroViewModel(hero);

            //Assert
            sut.HeroName.Should().Be(hero.HeroName);
            sut.HeroName_ko.Should().Be(hero.HeroName_ko);
            sut.RealName.Should().Be(hero.RealName);
            sut.Comment.Should().Be(hero.Summary);
            sut.Role.Should().Be(hero.Role);
            sut.SignatureColor.Should().Be(hero.SignatureColor_Hex);
        }

        /// <summary>
        /// ToModel은 수정된 속성 정보로 새로운 엔티티를 생성합니다.
        /// </summary>
        [Theory]
        [InlineAutoData("Monkey")]
        public void ToModel_does_return_editied_entity(string newName, Hero hero)
        {
            //Arrange
            var sut = new EditableHeroViewModel(hero);

            //Action
            sut.HeroName = newName;
            var newHero = sut.ToModel();

            //Assert
            newHero.Should().BeEquivalentTo(hero, option => option.Excluding(x => x.HeroName));
            newHero.HeroName.Should().Be(newName);
        }

        /// <summary>
        /// ToModel은 엔티티의 속성이 유효하지 않으면 장애를 발생합니다.
        /// </summary>
        [Theory]
        [AutoData]
        public void ToModel_throw_exception_if_invalid_property(string newName, Hero hero)
        {
            //Arrange
            var newHeroName = Enumerable.Range(0, 200).Select(x => newName).Aggregate((a, b) => a + b);
            var sut = new EditableHeroViewModel(hero);

            //Action
            sut.HeroName = newHeroName;
            var toModel = new Action(()=> sut.ToModel());

            //Assert
            toModel.Should().Throw<ValidationException>();
        }
    }
}
