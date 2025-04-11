using PruebaTecnica.Domain.Enums;
using PruebaTecnica.Domain.Utils;

namespace PruebaTecnica.Domain.Models
{
    public class VirtualMachineHubModel
    {
        public int Id { get; set; }
        public int Cores { get; set; }
        public int RAM { get; set; }
        public int Disc { get; set; }
        public string OperatingSystem { get; set; }
        public string CreatedAt { get; set; }
        public string? UpdatedAt { get; set; }

        public VirtualMachineHubModel(VirtualMachine virtualMachine)
        {
            Id = virtualMachine.Id;
            Cores = virtualMachine.Cores;
            RAM = virtualMachine.RAM;
            Disc = virtualMachine.Disc;
            OperatingSystem = ((OperatingSystems)virtualMachine.OperatingSystem).ToString();
            CreatedAt = TimeUtil.GetDateInFormatYYYYmmddHHmm(virtualMachine.CreatedAt);
            UpdatedAt = virtualMachine.UpdatedAt is null ? null : TimeUtil.GetDateInFormatYYYYmmddHHmm(virtualMachine.UpdatedAt.Value);
        }
    }
}
