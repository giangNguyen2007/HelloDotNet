using System.Net;
using FluentAssertions;
using Xunit.Abstractions;

namespace HelloTest.Test.Order.API;

public class OrderAPITest : IClassFixture<CustomOrderWebAppFactory>
{
    private readonly HttpClient _client;
    private readonly ITestOutputHelper _testOutputHelper;
    
    public OrderAPITest( CustomOrderWebAppFactory factory, ITestOutputHelper testOutputHelper)
    {
        _client = factory.CreateClient();
        _testOutputHelper = testOutputHelper;
    }
    
    [Fact()]
    public async Task GetAllTest() 
    {
        
        var response = await _client.GetAsync("/weather");
        _testOutputHelper.WriteLine(response.Content.ReadAsStringAsync().Result);
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
    
    
}