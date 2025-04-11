using FluentValidation;
using PruebaTecnica.Aplication.Features.VirtualMachines.V1.Queries;
using PruebaTecnica.Application.Exceptions.Wrappers;

namespace PruebaTecnica.Aplication.Features.VirtualMachines.V1.Validators
{
    public class GetVirtualMachineByIdQueryValidator : ValidationWrapper<GetVirtualMachineByIdQuery>
    {
        public GetVirtualMachineByIdQueryValidator()
        {
            RuleFor(x => x.VirtualMachineId)
                .NotEmpty().NotNull().WithMessage("El id de la máquina virtual no puede estar vacío.");
        }
    }
}
