using Product;

namespace Order.API.GRPC;

public class GrpcClientService
{
    private readonly ProductService.ProductServiceClient _productServiceClient;

    public GrpcClientService(ProductService.ProductServiceClient productServiceClient)
    {
        _productServiceClient = productServiceClient;
    }
    
    public async Task<ProductReply> GetProductAsync(int id)
    {
        var request = new ProductRequest { ProductId = "2"};
        return await _productServiceClient.GetProductInfoAsync(request);
    }
}