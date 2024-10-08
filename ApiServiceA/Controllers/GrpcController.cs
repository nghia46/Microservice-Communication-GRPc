using Google.Protobuf.WellKnownTypes;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;
using GrpcServiceB;

namespace ApiServiceA.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GrpcController : ControllerBase
    {
        [HttpGet("SayHello/{name}")]
        public async Task<IActionResult> SayHello(string name)
        {
            using var channel = GrpcChannel.ForAddress("http://service-b-lb:5207"); // URL của Service B
            var client = new Greeter.GreeterClient(channel);

            var reply = await client.SayHelloAsync(new HelloRequest { Name = name });

            return Ok(reply.Message);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateUser(User user)
        {
            using var channel = GrpcChannel.ForAddress("http://service-b-lb:5207");
            var client = new UserService.UserServiceClient(channel);

            var response = await client.CreateUserAsync(user);

            return Ok(response);
        }

        [HttpGet("getProducts")]
        public async Task<IActionResult> GetProducts()
        {
            using var channel = GrpcChannel.ForAddress("http://service-b-lb:5207");
            var client = new ProductService.ProductServiceClient(channel);
            var response = await client.GetProductsAsync(new Empty());
            return Ok(response);
        }
    }
}