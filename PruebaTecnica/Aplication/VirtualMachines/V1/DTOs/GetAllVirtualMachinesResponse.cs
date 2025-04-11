using PruebaTecnica.Domain.Enums;
using System;

namespace PruebaTecnica.Aplication.VirtualMachines.V1.DTOs
{
    public record struct GetAllVirtualMachinesResponse
    (
        int Id,
        int Cores,
        int RAM,
        int Disc,
        string OperatingSystem,
        string CreatedAt,
        string? UpdatedAt
    );
}
