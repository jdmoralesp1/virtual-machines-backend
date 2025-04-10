using PruebaTecnica.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace PruebaTecnica.Domain.Models
{
    public class VirtualMachine
    {
        public int Id { get; set; }

        public int Cores { get; set; }

        /// <summary>
        /// Cantidad de RAM en GB
        /// </summary>
        public int RAM { get; set; }

        /// <summary>
        /// Cantidad de disco en GB
        /// </summary>
        public int Disc { get; set; }

        public OperatingSystems OperatingSystem { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public Guid UserId { get; set; }

        public Guid? UserModifierId { get; set; }
    }
}
