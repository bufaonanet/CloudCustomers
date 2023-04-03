using CloudCustomers.API.Config;
using CloudCustomers.API.Models;
using CloudCustomers.API.Services;
using CloudCustomers.Tests.Fixtures;
using CloudCustomers.Tests.Helpers;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;

namespace CloudCustomers.Tests.Systems.Services;

public class TestUserService
{
    [Fact]
    public async Task GetAllUsers_WhenCalled_InvokesHttpGetRequest()
    {
        //Arrange
        var expectedResponse = UserFixture.GetTestUsers();
        var handlerMock = MockHttpMessageHandler<User>.SetupBasicGetResourceList(expectedResponse);
        var httpClient = new HttpClient(handlerMock.Object);
        var endpoint = "https://example.com/users";
        var config = Options
           .Create(new UserApiOptions { Endpoint = endpoint });

        var sut = new UserService(httpClient, config);

        //Act
        await sut.GetAllUsers();

        //Asert
        handlerMock
            .Protected()
            .Verify
            (
                "SendAsync",
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get),
                ItExpr.IsAny<CancellationToken>()
            );
    }

    [Fact]
    public async Task GetAllUsers_WhenCalled_ReturnsListOfUsers()
    {
        //Arrange
        var expectedResponse = UserFixture.GetTestUsers();
        var handlerMock = MockHttpMessageHandler<User>.SetupBasicGetResourceList(expectedResponse);
        var httpClient = new HttpClient(handlerMock.Object);
        var endpoint = "https://example.com/users";
        var config = Options
           .Create(new UserApiOptions { Endpoint = endpoint });

        var sut = new UserService(httpClient, config);

        //Act
        var result = await sut.GetAllUsers();

        //Asert
        result.Should().BeOfType<List<User>>();

    }

    [Fact]
    public async Task GetAllUsers_WhenHits404_ReturnEmptyList()
    {
        //Arrange        
        var handlerMock = MockHttpMessageHandler<User>.SetuReturn404();
        var httpClient = new HttpClient(handlerMock.Object);
        var endpoint = "https://example.com/users";
        var config = Options
           .Create(new UserApiOptions { Endpoint = endpoint });

        var sut = new UserService(httpClient, config);

        //Act
        var result = await sut.GetAllUsers();

        //Asert
        result.Count.Should().Be(0);

    }

    [Fact]
    public async Task GetAllUsers_WhenCalled_ReturnsListOfUsersOfExpectedSize()
    {
        //Arrange
        var expectedResponse = UserFixture.GetTestUsers();
        var handlerMock = MockHttpMessageHandler<User>.SetupBasicGetResourceList(expectedResponse);
        var httpClient = new HttpClient(handlerMock.Object);
        var endpoint = "https://example.com/users";
        var config = Options
           .Create(new UserApiOptions { Endpoint = endpoint });

        var sut = new UserService(httpClient, config);

        //Act
        var result = await sut.GetAllUsers();

        //Asert
        result.Count.Should().Be(expectedResponse.Count);

    }

    [Fact]
    public async Task GetAllUsers_WhenCalled_InvokesConfiguratedExternalUrl()
    {
        //Arrange
        var expectedResponse = UserFixture.GetTestUsers();
        var endpoint = "https://example.com/users";
        var handlerMock = MockHttpMessageHandler<User>
            .SetupBasicGetResourceList(expectedResponse, endpoint);
        var httpClient = new HttpClient(handlerMock.Object);

        var config = Options
            .Create(new UserApiOptions { Endpoint = endpoint });

        var sut = new UserService(httpClient, config);

        //Act
        var result = await sut.GetAllUsers();

        var uri = new Uri(endpoint);
                
        //Asert
        handlerMock
            .Protected()
            .Verify
            (
                "SendAsync",
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(req => 
                    req.Method == HttpMethod.Get && 
                    req.RequestUri == uri),
                ItExpr.IsAny<CancellationToken>()
            );
    }
}