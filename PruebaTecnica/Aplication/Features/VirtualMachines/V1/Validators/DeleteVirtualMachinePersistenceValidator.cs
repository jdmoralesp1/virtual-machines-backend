using PruebaTecnica.Aplication.Features.VirtualMachines.V1.Commands;
using PruebaTecnica.Application.Exceptions.Interfaces;
using PruebaTecnica.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PruebaTecnica.Aplication.Features.VirtualMachines.V1.Validators
{
    public class DeleteVirtualMachinePersistenceValidator : ICustomValidator<DeleteVirtualMachineCommand>
    {
        public IEnumerable<KeyValuePair<string, string>> Failures { get; private set; } = new List<KeyValuePair<string, string>>();
        private readonly IVirtualMachineRepository virtualMachineRepository;

        public DeleteVirtualMachinePersistenceValidator(IVirtualMachineRepository virtualMachineRepository)
        {
            this.virtualMachineRepository = virtualMachineRepository;
        }

        public async ValueTask<bool> Validate(DeleteVirtualMachineCommand instanceToValidate)
        {
            var virtualMachine = await virtualMachineRepository.GetByIdAsync(instanceToValidate.VirtualMachineId);

            if (virtualMachine == null)
                Failures = new List<KeyValuePair<string, string>>() { new("VirtualMachine", $"No existe la maquina virtual con id {instanceToValidate.VirtualMachineId}") };

            return !Failures.Any();
        }
    }
}
