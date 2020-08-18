using System;
using System.Net.Http;

namespace CreditRatingClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var httpHandler = new HttpClientHandler();
            // Return `true` to allow certificates that are untrusted/invalid
            httpHandler.ServerCertificateCustomValidationCallback
             = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            
            var address = "https://localhost:5001";
            var grpcChannel = Grpc.Net.Client.GrpcChannel.ForAddress(address,
                new Grpc.Net.Client.GrpcChannelOptions {
                    HttpHandler = httpHandler
                });

            var creditCheck = new CreditRatingService.CreditRatingCheck.CreditRatingCheckClient(grpcChannel);
            var creditReply = creditCheck.CheckCreditRequest(new CreditRatingService.CreditRequest() {
                 CustomerId = "id0201", Credit = 7000
            });

            Console.WriteLine("Credit Response Accepted: " +  creditReply.IsAccepted);
            grpcChannel.Dispose();
            
        }
    }
}
