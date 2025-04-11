using Microsoft.AspNetCore.SignalR;
using PruebaTecnica.Domain.Models;

namespace PruebaTecnica.Application.Hubs
{
    public class VirtualMachineHub : Hub
    {
        public async Task SendVirtualMachineUpdate(VirtualMachine virtualMachine)
        {
            await Clients.All.SendAsync("ReceiveVirtualMachineUpdate", virtualMachine);
        }
    }
}
