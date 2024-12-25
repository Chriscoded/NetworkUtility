using FluentAssertions;
using NetworkUtility.Ping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkUtility.Test.PingTests
{
    public class NetworkServiceTests
    {
        [Fact]//So that the test runner will identify this test
        public void Networkservice_SendPing_ReturnString()
        {
            //Arrange - Classes, Variables, Mocks

            var pingService = new NetworkService();
            //Act
            var result = pingService.SendPing();

            //Assert
            result.Should().NotBeNullOrWhiteSpace();
            result.Should().Be("Success: Ping sent!");
            result.Should().Contain("Success", Exactly.Once());
        }

        [Theory] //helps you pass variables, an inline data as they are sometimes called
        [InlineData(2, 5, 7)]
        [InlineData(3, 1, 4)]

        public void NetworkService_PingTimeout_ReturnInt(int a, int b, int expected)
        {
            //Arrange - Classes, Variables, Mocks
            var pingService = new NetworkService();

            //Act
            var result = pingService.PingTimeout(a,b);

            //Assert
            result.Should().Be(expected);
            result.Should().NotBeInRange(-10000, 0);
            result.Should().BeGreaterThanOrEqualTo(4);

        }

    }
}
