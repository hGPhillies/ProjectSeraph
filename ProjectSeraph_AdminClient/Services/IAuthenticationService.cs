using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSeraph_AdminClient.Services
{
    public interface IAuthenticationService
    {
        Task<string> LoginAsync(string username, string password);
    }
}
