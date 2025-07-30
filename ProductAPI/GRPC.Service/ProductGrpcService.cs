using Grpc.Core;
using YourApp.Grpc;

namespace ProductAPI.GRPC.Service;

public class ProductGrpcService : ProductService.ProductServiceBase
{
    
    public override Task<ProductReply> GetProductInfo(ProductRequest request, ServerCallContext context)
    {
        Console.WriteLine($"Received Message: Product request = {request.ProductId}");

        
        return Task.FromResult(new ProductReply
        {
            Name = "Sample Product",
            Stock = 10
        });
    }
}