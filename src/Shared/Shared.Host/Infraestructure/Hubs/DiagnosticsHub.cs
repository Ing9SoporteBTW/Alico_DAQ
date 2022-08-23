namespace Shared.Host.Infraestructure.Hubs
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.SignalR;

    public class DiagnosticsHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            await Clients.All.SendAsync("Send", $"{Context.ConnectionId} joined");
        }

        public override async Task OnDisconnectedAsync(Exception ex)
        {
            await Clients.Others.SendAsync("Send", $"{Context.ConnectionId} left");
        }

        public Task SendRequestTrace(string message)
        {
            return Clients.All.SendAsync("SendRequestTrace", message);
        }

    }
}
