using MediatR;
using PruebaTecnica.Aplication.VirtualMachines.V1.DTOs;
using PruebaTecnica.Domain.Wrappers;

namespace PruebaTecnica.Aplication.VirtualMachines.V1.Queries
{
    public record struct GetVirtualMachineByIdQuery (int VirtualMachineId) : IRequest<Response<GetVirtualMachineByIdResponse>>;
}
