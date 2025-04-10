using System;

namespace PruebaTecnica.Aplication.VirtualMachines.V1.DTOs
{
    public record struct CreateVirtualMachineResponse
    (
        int Id,
        DateTime CreatedAt,
        DateTime? UpdatedAt
    );
}
