
namespace EsHeichSample.Client.UnitTests.DataTests
{
    using Xunit;
    using Moq;
    using FluentAssertions;
    using AutoFixture;
    using AutoFixture.Xunit2;

    using Microsoft.EntityFrameworkCore;
    using EsHeichSample.Client.Datas;

    public class EsHeichContextTests
    {

        [Fact]
        public void Migrate_does_migrate_context_with_seed_data()
        {
            //Arrange
            var option = new DbContextOptionsBuilder()
                                        .UseInMemoryDatabase("InMemory")
                                        .Options;

            var sut = new EsHeichContext(option);
            sut.Database.EnsureDeleted();

            //Act
            sut.Migrate();

            //Assert
            sut.Heroes.Should().HaveCountGreaterThan(0);
        }
    }
}
