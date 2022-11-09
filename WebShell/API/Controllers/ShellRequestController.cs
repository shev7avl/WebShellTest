using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using WebShell.API.Hubs;
using WebShell.API.Hubs.Clients;
using WebShell.Data.Repository;
using WebShell.Domain;
using System.Web.Http.Cors;
using System.Text;

namespace WebShell.API.Controllers
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ShellRequestController : ControllerBase
    {
        private readonly IRepository<ShellRequest> _requests;
        private readonly IHubContext<ShellHub, IHubClient> _context;

        public ShellRequestController(IRepository<ShellRequest> requests, IHubContext<ShellHub, IHubClient> context)
        {
            _requests = requests;
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<ShellRequest> GetShellRequestAsync(int id)
        {
            return await _requests.GetById(id);
        }

        [HttpPost("rq:{input}")]
        public async Task<ShellRequest> CreateShellRequest(string input)
        {

            Console.WriteLine($">> Controller got input: {input}");

            ShellRequest shellRequest = new ShellRequest();
            Console.WriteLine($">> Created request: {shellRequest}");
            shellRequest.Input = input;

            using (Process cmd = new Process())
            {
                cmd.StartInfo.FileName = "cmd.exe";
                cmd.StartInfo.RedirectStandardInput = true;
                cmd.StartInfo.RedirectStandardOutput = true;
                cmd.StartInfo.RedirectStandardError = true;
                cmd.StartInfo.CreateNoWindow = true;
                cmd.StartInfo.UseShellExecute = false;
                cmd.StartInfo.Arguments = $"/C {input}";

                cmd.Start();
                cmd.BeginOutputReadLine();
                cmd.BeginErrorReadLine();

                var output = new StringBuilder();
                var error = new StringBuilder();

                cmd.OutputDataReceived += (sender, args) =>
                {
                    Console.WriteLine($">>Received output data^ {args.Data}");
                    output.AppendLine(args.Data);
                };
                cmd.ErrorDataReceived += (sender, args) =>
                {
                    Console.WriteLine($">>Received error data^ {args.Data}");
                    error.AppendLine(args.Data);
                };

                if (!cmd.WaitForExit(5000))
                {
                    Console.WriteLine(">> The process execution is too long, closing the process");
                    cmd.Close();

                    Console.WriteLine("The process has been closed");
                }

                shellRequest.Output = output.ToString();
                shellRequest.Error = error.ToString();

            }
            Console.WriteLine($">> Request id: {shellRequest.Id}");
            Console.WriteLine($">> Request input: {shellRequest.Input}");
            Console.WriteLine($">> Request output: {shellRequest.Output}");
            Console.WriteLine($">> Request error: {shellRequest.Error}");


            await _context.Clients.All.ReceiveRequestResult(shellRequest);
            await _requests.Create(shellRequest);

            return shellRequest;
        }
    }
}
