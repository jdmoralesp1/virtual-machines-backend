namespace PruebaTecnica.Aplication.Features.VirtualMachines.V1.DTOs
{
    public record struct GetVirtualMachineByIdResponse
    (
        int Id,
        int Cores,
        int RAM,
        int Disc,
        string OperatingSystem,
        string CreatedAt,
        string UpdatedAt
    );
}
