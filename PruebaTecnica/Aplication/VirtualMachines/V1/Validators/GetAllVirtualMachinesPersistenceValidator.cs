using PruebaTecnica.Aplication.VirtualMachines.V1.Queries;
using PruebaTecnica.Application.Exceptions.Interfaces;
using PruebaTecnica.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PruebaTecnica.Aplication.VirtualMachines.V1.Validators
{
    public class GetAllVirtualMachinesPersistenceValidator : ICustomValidator<GetAllVirtualMachinesQuery>
    {
        public IEnumerable<KeyValuePair<string, string>> Failures { get; private set; } = new List<KeyValuePair<string, string>>();
        private readonly IVirtualMachineRepository virtualMachineRepository;

        public GetAllVirtualMachinesPersistenceValidator(IVirtualMachineRepository virtualMachineRepository)
        {
            this.virtualMachineRepository = virtualMachineRepository;
        }

        public async ValueTask<bool> Validate(GetAllVirtualMachinesQuery instanceToValidate)
        {
            var virtualMachines = await virtualMachineRepository.GetAllAsync();

            if(!virtualMachines.Any() || virtualMachines.TrueForAll(x => x.IsActive == false))
                Failures = new List<KeyValuePair<string, string>>() { new ("VirtualMachines", "No hay maquinas virtuales registradas")};

            return !Failures.Any();
        }
    }
}
