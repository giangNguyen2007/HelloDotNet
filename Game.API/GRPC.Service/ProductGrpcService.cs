using Grpc.Core;
using YourApp.Grpc;

namespace Game.API.GRPC.Service;

public class ProductGrpcService : ProductService.ProductServiceBase
{
    
    public override Task<ProductReply> GetProductInfo(ProductRequest request, ServerCallContext context)
    {
        
        
        
        return Task.FromResult(new ProductReply
        {
            Name = "Sample Product",
            Stock = 10
        });
    }
}