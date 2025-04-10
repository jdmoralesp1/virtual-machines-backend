using PruebaTecnica.Domain.Models;

namespace PruebaTecnica.Domain.Interfaces
{
    public interface IVirtualMachineRepository
    {
        public Task<VirtualMachine?> GetByIdAsync(int id);
        public VirtualMachine Add(VirtualMachine virtualMachine);
        public Task<List<VirtualMachine>> GetAllAsync();
        public VirtualMachine Update(VirtualMachine virtualMachine);
        public void Delete(VirtualMachine virtualMachine);
        public Task SaveChangesAsync();

    }
}
