
namespace EsHeichSample.Client.UnitTests.ServiceTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using Xunit;
    using Moq;
    using FluentAssertions;
    using AutoFixture;
    using AutoFixture.Xunit2;

    using EsHeich.RepositoryPattern;
    using EsHeichSample.Client.Models;
    using EsHeichSample.Client.Services;

    public class HeroServiceTest
    {

        [Theory]
        [InlineAutoData(false)]
        [InlineAutoData(true, null)]
        public void Construct_correctly(bool throwException, Mock<IUnitOfWorkFactory> factory)
        {
            //Arrange
            HeroService sut = default;
            bool exception = false;

            //Act
            try
            {
                sut = new HeroService(factory.Object);
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


        [Theory]
        [InlineAutoData(5)]
        [InlineAutoData(50)]
        public void GetDcHeroesAsync_does_return_correctly(
            int maxHeroLength, Mock<IUnitOfWorkFactory> factory, Mock<IUnitOfWork> unitOfWork)
        {
            //Arrange
            var heroes = new Fixture().CreateMany<Hero>(maxHeroLength);
            var sut = new HeroService(factory.Object);
            var stubRepo = new StubHeroRepo(heroes);

            unitOfWork.Setup(q => q.GetRepository<Hero, int>()).Returns(stubRepo);
            factory.Setup(q => q.CreateUnitOfWork()).Returns(unitOfWork.Object);

            //Act
            var foundHeroes = sut.GetDcHeroesAsync().Result;

            //Assert
            foundHeroes.Should().BeEquivalentTo(heroes);
        }


        public class StubHeroRepo : IRepository<Hero, int>
        {
            readonly List<Hero> _heroes;

            public StubHeroRepo(IEnumerable<Hero> heroes)
                => this._heroes = heroes.ToList();

            public Hero Add(Hero entity)
            {
                this._heroes.Add(entity);
                return entity;
            }

            public void AddRange(IEnumerable<Hero> entities)
            {
                this._heroes.AddRange(entities);
            }

            public Hero Get(int key)
                => this._heroes.FirstOrDefault(x => x.ID == key);

            public Task<Hero[]> GetAllAsync(CancellationToken token = default)
                => Task.Run(() => this._heroes.ToArray());

            public void Remove(Hero entity)
            {
                this._heroes.Remove(entity);
            }

            public void RemoveRange(IEnumerable<Hero> entities)
            {
                this._heroes.RemoveAll(x => entities.Any(q => x.ID == q.ID));
            }

            public Hero Update(Hero entity)
            {
                var item = this._heroes.FirstOrDefault(q => q.ID == entity.ID);
                var index = this._heroes.IndexOf(item);
                this._heroes[index] = entity;
                return entity;
            }

            public void UpdateRange(IEnumerable<Hero> entities)
            {
                foreach (var e in entities)
                    Update(e);
            }
        }
    }
}
