
using FluentAssertions;
using FutiPlay.Core.Bac;
using FutiPlay.Core.Interfaces.IRepository;
using FutiPlay.Core.Models;
using FutiPlay.Core.Response;
using Moq;
using xShared.Request;
using xShared.Responses;

namespace FutiPlay.Testing.Domain.BAC
{
    [TestFixture]
    public class TournamentBacTest
    {
        [Test]
        public async Task FetchTournamentByRequestAsync_ShouldReturnTournamentResponse()
        {
            // Arrange
            Mock<ITournamentRepository> mockTournamentRepository = new();
            TournamentBac tournamentBac = new TournamentBac(mockTournamentRepository.Object);

            TournamentResponse expectedResponse = new();

            mockTournamentRepository.Setup(r => r.FetchTournamentByRequestAsync()).ReturnsAsync(expectedResponse);

            // Act
            TournamentResponse result = await tournamentBac.FetchTournamentByRequestAsync();

            // Assert         
            mockTournamentRepository.Verify(r => r.FetchTournamentByRequestAsync(), Times.Once);

            result.Should().BeEquivalentTo(expectedResponse);
        }

        [Test]
        public void FetchTournamentByRequestAsync_ShouldHandleException()
        {
            // Arrange
            Mock<ITournamentRepository> mockTournamentRepository = new();
            TournamentBac tournamentBac = new(mockTournamentRepository.Object);
           
            mockTournamentRepository.Setup(r => r.FetchTournamentByRequestAsync())
                .ThrowsAsync(new Exception("Simulated Exception"));

            // Act and Assert
            Assert.ThrowsAsync<Exception>(async () => await tournamentBac.FetchTournamentByRequestAsync());
        }

        [Test]
        public async Task InsertTournamentByRequestAsync_ShouldReturnModelOperationResponse()
        {
            //Arrange
            Mock<ITournamentRepository> mockRepository = new();
            TournamentBac tournamentBac = new(mockRepository.Object);         

            Tournament tournament = new()
            {
                Id = 1,
                Name = "test",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.Date.AddMonths(1),
                Status = "Active"
            };

            ModelOperationResponse responseSimulated = new();
            responseSimulated.AddInfoMessage("Tournament was successfully added");

            mockRepository.Setup(r => r.InsertTournamentByRequestAsync(It.IsAny<ModelOperationRequest<Tournament>>()))
                .ReturnsAsync(responseSimulated);

            //Act
            ModelOperationResponse response = await tournamentBac.InsertTournamentByRequestAsync(tournament);

            //Asset
            mockRepository.Verify(r => r.InsertTournamentByRequestAsync(It.IsAny<ModelOperationRequest<Tournament>>()),Times.Once);

            response.Should().BeEquivalentTo(responseSimulated);
        }

        [Test]
        public void InsertTournamentByRequestAsync_ShouldHandleFailure()
        {
            // Arrange
            Mock<ITournamentRepository> mockRepository = new();
            TournamentBac tournamentBac = new(mockRepository.Object);

            Tournament tournament = new();

            mockRepository.Setup(r => r.InsertTournamentByRequestAsync(It.IsAny<ModelOperationRequest<Tournament>>()))
                .ThrowsAsync(new Exception("Simulated Exception"));

            // Act and Assert
            Exception exception = Assert.ThrowsAsync<Exception>(() => tournamentBac.InsertTournamentByRequestAsync(tournament));

            exception.Message.Should().Be("Simulated Exception");
        }
    }
}
