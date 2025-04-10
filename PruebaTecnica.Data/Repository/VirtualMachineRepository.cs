using Microsoft.EntityFrameworkCore;
using PruebaTecnica.Data.Context;
using PruebaTecnica.Domain.Interfaces;
using PruebaTecnica.Domain.Models;

namespace PruebaTecnica.Data.Repository
{
    public class VirtualMachineRepository : IVirtualMachineRepository
    {
        private readonly PruebaTecnicaContext context;

        public VirtualMachineRepository(PruebaTecnicaContext context)
        {
            this.context = context;
        }

        public async Task<VirtualMachine?> GetByIdAsync(int id)
        {
            return await context.VirtualMachines.FirstOrDefaultAsync(x => x.Id == id && x.IsActive == true);
        }

        public VirtualMachine Add(VirtualMachine virtualMachine)
        {
            context.VirtualMachines.Add(virtualMachine);
            return virtualMachine;
        }

        public Task<List<VirtualMachine>> GetAllAsync()
        {
            if (context.VirtualMachines.Any())
                return context.VirtualMachines.Where(x => x.IsActive == true).ToListAsync();

            return Task.FromResult(new List<VirtualMachine>());
        }

        public VirtualMachine Update(VirtualMachine virtualMachine)
        {
            context.VirtualMachines.Update(virtualMachine);
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
