using FluentAssertions;
using FluentAssertions.Extensions;
using NetworkUtility.Ping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;

namespace NetworkUtility.Test.PingTests
{
    public class NetworkServiceTests
    {
        private readonly NetworkService _pingService;
        public NetworkServiceTests()
        {
            _pingService = new NetworkService();
        }

        [Fact]//So that the test runner will identify this test
        public void Networkservice_SendPing_ReturnString()
        {
            //Arrange - Classes, Variables, Mocks

            //Act
            var result = _pingService.SendPing();

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

            //Act
            var result = _pingService.PingTimeout(a,b);

            //Assert
            result.Should().Be(expected);
            result.Should().NotBeInRange(-10000, 0);
            result.Should().BeGreaterThanOrEqualTo(4);

        }

        [Fact]//So that the test runner will identify this test
        public void Networkservice_LastPingDate_ReturnDate()
        {
            //Arrange - Classes, Variables, Mocks

            //Act
            var result = _pingService.LastPingDate();

            //Assert
            result.Should().BeSameDateAs(DateTime.Today);
            result.Should().BeAfter(1.January(2010));
            result.Should().BeBefore(1.January(2029));
        }

        [Fact]
        public void NetworkService_GetPingOptions_ReturnObject()
        {
            //Arrange - Classes, Variables, Mocks
            var expected = new PingOptions()
            {
                DontFragment = true,
                Ttl = 1
            };
            //Act
            var result = _pingService.GetPingOptions();

            //Assert

            //for an object use "BeEquivalentTo" instead of "Be"
            result.Should().BeOfType<PingOptions>();
            result.Should().BeEquivalentTo(expected);
            result.Ttl.Should().Be(1);

        }

        [Fact]
        public void NetworkService_MostRecentPings_ReturnIEnumerable()
        {
            //Arrange - Classes, Variables, Mocks
            var expected = new PingOptions()
            {
                DontFragment = true,
                Ttl = 1
            };
            //Act
            var result = _pingService.MostRecentPings();

            //Assert

            //for an IEnumerable use "ContainEquivalentOf" instead of "Be"
            //result.Should().BeOfType<IEnumerable<PingOptions>>();
            result.Should().ContainEquivalentOf(expected);
            result.Should().Contain(x => x.DontFragment == true);

        }
    }
}
