using System.Net;
using FluentAssertions;
using Xunit.Abstractions;

namespace HelloTest.Test.Auth.API;

[CollectionDefinition("DB container")]
public class PostgresCollection : ICollectionFixture<PostgresFixture>;

[Collection("DB container")]
public class AuthApiTest : IClassFixture<AuthWebAppFactory>
{
    private readonly AuthWebAppFactory _factory;
    private readonly HttpClient _client;
    private readonly ITestOutputHelper _testOutputHelper;

    public AuthApiTest(AuthWebAppFactory factory, ITestOutputHelper testOutputHelper)
    {
        _factory = factory;
        _testOutputHelper = testOutputHelper;
        _client = factory.CreateClient();
    }
    
    [Fact(DisplayName = "get All Users")]
    public async Task GetAllUsersTest() 
    {
        
        var response = await _client.GetAsync("/auth");
        _testOutputHelper.WriteLine(response.Content.ReadAsStringAsync().Result);
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
    
}