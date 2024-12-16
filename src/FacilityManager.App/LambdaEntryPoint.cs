using Amazon.Lambda.AspNetCoreServer;

namespace FacilityManager.App;

public sealed class LambdaEntryPoint : APIGatewayHttpApiV2ProxyFunction
{
    protected override void Init(IWebHostBuilder builder)
    {
        builder.UseStartup<LambdaStartup>();
    }
}