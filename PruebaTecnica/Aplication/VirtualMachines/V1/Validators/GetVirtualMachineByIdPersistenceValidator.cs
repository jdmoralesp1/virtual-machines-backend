using PruebaTecnica.Aplication.VirtualMachines.V1.Queries;
using PruebaTecnica.Application.Exceptions.Interfaces;
using PruebaTecnica.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PruebaTecnica.Aplication.VirtualMachines.V1.Validators
{
    public class GetVirtualMachineByIdPersistenceValidator : ICustomValidator<GetVirtualMachineByIdQuery>
    {
        public IEnumerable<KeyValuePair<string, string>> Failures { get; private set; } = new List<KeyValuePair<string, string>>();
        private readonly IVirtualMachineRepository virtualMachineRepository;

        public GetVirtualMachineByIdPersistenceValidator(IVirtualMachineRepository virtualMachineRepository)
        {
            this.virtualMachineRepository = virtualMachineRepository;
        }

        public async ValueTask<bool> Validate(GetVirtualMachineByIdQuery instanceToValidate)
        {
            var virtualMachine = await virtualMachineRepository.GetByIdAsync(instanceToValidate.VirtualMachineId);

            if(virtualMachine is null)
                Failures = new List<KeyValuePair<string, string>>() { new("VirtualMachineId", $"No existe una maquina virtual con el id {instanceToValidate.VirtualMachineId}") };

            return !Failures.Any();
        }
    }
}
