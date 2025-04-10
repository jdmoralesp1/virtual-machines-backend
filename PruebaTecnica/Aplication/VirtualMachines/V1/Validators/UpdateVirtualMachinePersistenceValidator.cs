using PruebaTecnica.Aplication.VirtualMachines.V1.Commands;
using PruebaTecnica.Application.Exceptions.Interfaces;
using PruebaTecnica.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PruebaTecnica.Aplication.VirtualMachines.V1.Validators
{
    public class UpdateVirtualMachinePersistenceValidator : ICustomValidator<UpdateVirtualMachineCommand>
    {
        public IEnumerable<KeyValuePair<string, string>> Failures { get; private set; } = new List<KeyValuePair<string, string>>();
        private readonly IVirtualMachineRepository virtualMachineRepository;

        public UpdateVirtualMachinePersistenceValidator(IVirtualMachineRepository virtualMachineRepository)
        {
            this.virtualMachineRepository = virtualMachineRepository;
        }

        public async ValueTask<bool> Validate(UpdateVirtualMachineCommand instanceToValidate)
        {
            var virtualMachine = await virtualMachineRepository.GetByIdAsync(instanceToValidate.VirtualMachineId.Value);

            if (virtualMachine is null)
                Failures = new List<KeyValuePair<string, string>>() { new("VirtualMachineId", "No existe una maquina virtual con ese id") };

            return !Failures.Any();
        }
    }
}
