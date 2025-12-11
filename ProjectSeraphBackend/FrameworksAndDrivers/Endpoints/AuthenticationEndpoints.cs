using MongoDB.Driver;

namespace ProjectSeraphBackend.FrameworksAndDrivers.Endpoints
{
    public static class AuthenticationEndpoints
    {
        public static IEndpointRouteBuilder MapAuthEndpoints(this IEndpointRouteBuilder app)
        {
            var authGroup = app.MapGroup("/auth")
            .WithTags("AuthenticationEndpoints");

            //post, not get, to encrypt credentials
            authGroup.MapPost("/user", async () =>
            {
                return new
                {
                    id = "cs8743298",
                    isNurse = false,
                };
            });

            return app;
        }
    }
}
