

using Game.API;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace HelloTest;

public class GateWayTest_WebAppFactory : WebApplicationFactory<GateWayProgram>
{
    private readonly string _mockUrl;

    public GateWayTest_WebAppFactory(string mockUrl)
    {
        _mockUrl = mockUrl;
    }


    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // Replace configuration or injected HttpClient base address
            // services.PostConfigure<GatewayOptions>(opts =>
            // {
            //     opts.DownstreamBaseUrl = _mockUrl;
            // });
        });
    }
}