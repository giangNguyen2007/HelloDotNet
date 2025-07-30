using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using ProductAPI;
using ProductAPI.Dtos.Game;
using ProductAPI.Model;
using HelloTest.Test.Auth.API;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Xunit.Abstractions;

namespace HelloTest;

[CollectionDefinition("DB container")]
public class PostgresCollection : ICollectionFixture<PostgresFixture>;

[Collection("DB container")]
public class UnitTest1 : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;
    private readonly ITestOutputHelper _testOutputHelper;
 
  
    public UnitTest1( CustomWebApplicationFactory factory, ITestOutputHelper testOutputHelper)
    {
        _client = factory.CreateClient();
        _testOutputHelper = testOutputHelper;
    }

    [Fact(DisplayName = "Hello World")]
    public async Task GetAllTest() 
    {
        
        var response = await _client.GetAsync("/game");
        _testOutputHelper.WriteLine(response.Content.ReadAsStringAsync().Result);
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
    
    [Fact(DisplayName = "Hello World2")]
    public async Task GetSingleGameTest() 
    {
        
        var response = await _client.GetAsync("/game/1");
        _testOutputHelper.WriteLine(response.Content.ReadAsStringAsync().Result);
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
    
    [Fact]
    public async Task PostSingleGameTest()
    {
        var payload = new GameModel {Name = "Bluberry", Genre = "Family"};
        var postResponse = await _client.PostAsJsonAsync("/game", payload);
        postResponse.EnsureSuccessStatusCode();

        var newGame = await postResponse.Content.ReadFromJsonAsync<GameModel>();
        _testOutputHelper.WriteLine(postResponse.Content.ReadAsStringAsync().Result);
        postResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        
    }
    
}