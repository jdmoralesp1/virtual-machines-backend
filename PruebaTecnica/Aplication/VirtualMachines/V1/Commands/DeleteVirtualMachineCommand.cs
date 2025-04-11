using MediatR;
using PruebaTecnica.Domain.Wrappers;

namespace PruebaTecnica.Aplication.VirtualMachines.V1.Commands
{
    public record struct DeleteVirtualMachineCommand (int VirtualMachineId) : IRequest<Response<string>>;
}
