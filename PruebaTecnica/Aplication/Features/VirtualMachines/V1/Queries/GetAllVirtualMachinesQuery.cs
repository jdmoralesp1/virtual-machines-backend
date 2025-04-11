using MediatR;
using PruebaTecnica.Aplication.Features.VirtualMachines.V1.DTOs;
using PruebaTecnica.Domain.Wrappers;
using System.Collections.Generic;

namespace PruebaTecnica.Aplication.Features.VirtualMachines.V1.Queries
{
    public record struct GetAllVirtualMachinesQuery : IRequest<Response<List<GetAllVirtualMachinesResponse>>>;
}
