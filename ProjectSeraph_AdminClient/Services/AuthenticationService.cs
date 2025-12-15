using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSeraph_AdminClient.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        public async Task<string> LoginAsync(string username, string password)
        {
            await Task.Delay(100);

            if (username == "1111110000" && password == "admin")
            {
                return username;
            }
            else
            {
                return null;
            }
        }
    }
}
