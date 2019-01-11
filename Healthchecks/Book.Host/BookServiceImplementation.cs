using System.Threading.Tasks;
using ConsoleOut.Book.Contract;
using Grpc.Core;

namespace ConsoleOut.Book.Host
{
    internal class BookServiceImplementation : BookService.BookServiceBase
    {
        public override Task<GetBookByIdResponse> GetBookById(GetBookByIdRequest request, ServerCallContext context)
        {
            return Task.FromResult(new GetBookByIdResponse
            {
                Id = 1,
                Name = "Hitchhiker's guide to the galaxy",
                Description = "Best book ever."
            });
        }
    }
}