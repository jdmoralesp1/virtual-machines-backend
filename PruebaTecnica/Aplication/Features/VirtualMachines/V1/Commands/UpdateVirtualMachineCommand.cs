using MediatR;
using PruebaTecnica.Domain.Enums;
using PruebaTecnica.Domain.Wrappers;

namespace PruebaTecnica.Aplication.Features.VirtualMachines.V1.Commands
{
    public record struct UpdateVirtualMachineCommand
    (
        int? VirtualMachineId,
        int Cores,
        int RAM,
        int Disc,
        OperatingSystems OperatingSystem
    ) : IRequest<Response<string>>;
}
