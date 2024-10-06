using Microsoft.Extensions.DependencyInjection;
using Stripe;

namespace NurseryApp.Application.Setting
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // Other service configurations...

            // Set the API key
            StripeConfiguration.ApiKey = "sk_test_51Q6f8OBwBCJxjGyAxbb7sHrGhMvIMVCqhhLGY7tYQuIrlypWuTKxFwL6QSwQfnRqySVexS26S80Eerx5CWb7Slxj00sF9SmcNt";
        }
    }
}
