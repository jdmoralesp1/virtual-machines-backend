using MediatR;
using PruebaTecnica.Aplication.Features.VirtualMachines.V1.Commands;
using PruebaTecnica.Application.Exceptions.Interfaces;
using PruebaTecnica.Application.Interfaces;
using PruebaTecnica.Domain.Interfaces;
using PruebaTecnica.Domain.Utils;
using PruebaTecnica.Domain.Wrappers;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PruebaTecnica.Aplication.Features.VirtualMachines.V1.Commands.Handlers
{
    public class UpdateVirtualMachineCommandHandler : IRequestHandler<UpdateVirtualMachineCommand, Response<string>>
    {
        private readonly IVirtualMachineRepository virtualMachineRepository;
        private readonly IValidationService<UpdateVirtualMachineCommand> validationService;
        private readonly IAuditService auditService;

        public UpdateVirtualMachineCommandHandler(IVirtualMachineRepository virtualMachineRepository, IValidationService<UpdateVirtualMachineCommand> validationService, IAuditService auditService)
        {
            this.virtualMachineRepository = virtualMachineRepository;
            this.validationService = validationService;
            this.auditService = auditService;
        }

        public async Task<Response<string>> Handle(UpdateVirtualMachineCommand request, CancellationToken cancellationToken)
        {
            await validationService.ExecuteValidationGuard(request);

            var virtualMachine = await virtualMachineRepository.GetByIdAsync(request.VirtualMachineId.Value);

            virtualMachine.Cores = request.Cores;
            virtualMachine.RAM = request.RAM;
            virtualMachine.Disc = request.Disc;
            virtualMachine.OperatingSystem = request.OperatingSystem;
            virtualMachine.UpdatedAt = TimeUtil.ObtenerFechaYHoraZonaHorariaBogota();
            virtualMachine.UserModifierId = Guid.Parse(auditService.GetUserId());

            await virtualMachineRepository.Update(virtualMachine);

            return new Response<string>(data: $"Se ha actualizado correctamente la maquina virtual con id {request.VirtualMachineId}");
        }
    }
}
