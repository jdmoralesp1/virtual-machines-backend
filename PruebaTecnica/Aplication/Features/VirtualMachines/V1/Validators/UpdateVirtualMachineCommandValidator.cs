using FluentValidation;
using PruebaTecnica.Aplication.Features.VirtualMachines.V1.Commands;
using PruebaTecnica.Application.Exceptions.Wrappers;

namespace PruebaTecnica.Aplication.Features.VirtualMachines.V1.Validators
{
    public class UpdateVirtualMachineCommandValidator : ValidationWrapper<UpdateVirtualMachineCommand>
    {
        public UpdateVirtualMachineCommandValidator()
        {
            RuleFor(x => x.VirtualMachineId)
                .NotEmpty().NotNull().WithMessage("El campo Id es obligatorio.")
                .GreaterThan(0).WithMessage("El campo Id debe ser mayor que cero.");

            RuleFor(x => x.Cores)
                .NotEmpty().NotNull().WithMessage("El campo Cores es obligatorio.")
                .GreaterThan(0).WithMessage("El campo Cores debe ser mayor que cero.");

            RuleFor(x => x.RAM)
                .NotEmpty().NotNull().WithMessage("El campo RAM es obligatorio.")
                .GreaterThan(0).WithMessage("El campo RAM debe ser mayor que cero.");

            RuleFor(x => x.Disc)
                .NotEmpty().NotNull().WithMessage("El campo Disc es obligatorio.")
                .GreaterThan(0).WithMessage("El campo Disc debe ser mayor que cero.");

            RuleFor(x => x.OperatingSystem)
                .NotNull().WithMessage("El campo OperatingSystem es obligatorio.")
                .IsInEnum().WithMessage("El campo OperatingSystem no es un valor válido.");
        }
    }
}
