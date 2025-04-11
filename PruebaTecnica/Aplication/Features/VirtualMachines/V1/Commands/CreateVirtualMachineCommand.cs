using MediatR;
using PruebaTecnica.Aplication.Features.VirtualMachines.V1.DTOs;
using PruebaTecnica.Domain.Enums;
using PruebaTecnica.Domain.Wrappers;
using System;

namespace PruebaTecnica.Aplication.Features.VirtualMachines.V1.Commands
{
    public record struct CreateVirtualMachineCommand
    (
        int Cores,
        int RAM,
        int Disc,
        OperatingSystems OperatingSystem
    ) : IRequest<Response<CreateVirtualMachineResponse>>;
}
