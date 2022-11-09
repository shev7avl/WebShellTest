using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using WebShell.API.Hubs.Clients;
using WebShell.Domain;

namespace WebShell.API.Hubs
{
    public class ShellHub: Hub<IHubClient>
    {
        public async Task SendMessage(ShellRequest request)
        {
            await Clients.All.ReceiveRequestResult(request);
        }
    }
}
