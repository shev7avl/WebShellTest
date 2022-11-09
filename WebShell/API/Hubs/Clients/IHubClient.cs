using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShell.Domain;

namespace WebShell.API.Hubs.Clients
{
    public interface IHubClient
    {
        public Task ReceiveRequestResult(ShellRequest request);
    }
}
