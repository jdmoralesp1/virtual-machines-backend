using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using PruebaTecnica.Application.Hubs;
using PruebaTecnica.Data.Context;
using PruebaTecnica.Domain.Interfaces;
using PruebaTecnica.Domain.Models;

namespace PruebaTecnica.Data.Repository
{
    public class VirtualMachineRepository : IVirtualMachineRepository
    {
        private readonly PruebaTecnicaContext context;
        private readonly IHubContext<VirtualMachineHub> hubContext;

        public VirtualMachineRepository(PruebaTecnicaContext context, IHubContext<VirtualMachineHub> hubContext)
        {
            this.context = context;
            this.hubContext = hubContext;
        }

        public async Task<VirtualMachine?> GetByIdAsync(int id)
        {
            return await context.VirtualMachines.FirstOrDefaultAsync(x => x.Id == id && x.IsActive == true);
        }

        public async Task<VirtualMachine> Add(VirtualMachine virtualMachine)
        {
            context.VirtualMachines.Add(virtualMachine);
            await context.SaveChangesAsync();
            await hubContext.Clients.All.SendAsync("ReceiveVirtualMachineUpdate", new VirtualMachineHubModel(virtualMachine));
            return virtualMachine;
        }

        public Task<List<VirtualMachine>> GetAllAsync()
        {
            if (context.VirtualMachines.Any())
                return context.VirtualMachines.Where(x => x.IsActive == true).ToListAsync();

            return Task.FromResult(new List<VirtualMachine>());
        }

        public async Task<VirtualMachine> Update(VirtualMachine virtualMachine)
        {
            context.VirtualMachines.Update(virtualMachine);
            await context.SaveChangesAsync();
            await hubContext.Clients.All.SendAsync("ReceiveVirtualMachineUpdate", new VirtualMachineHubModel(virtualMachine));
            return virtualMachine;
        }

        public void Delete(VirtualMachine virtualMachine)
        {
            context.VirtualMachines.Remove(virtualMachine);
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
