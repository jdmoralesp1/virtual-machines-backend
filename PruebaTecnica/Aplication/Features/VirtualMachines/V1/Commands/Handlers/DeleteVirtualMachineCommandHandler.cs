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
    public class DeleteVirtualMachineCommandHandler : IRequestHandler<DeleteVirtualMachineCommand, Response<string>>
    {
        private readonly IVirtualMachineRepository virtualMachineRepository;
        private readonly IValidationService<DeleteVirtualMachineCommand> validationService;
        private readonly IAuditService auditService;

        public DeleteVirtualMachineCommandHandler(IVirtualMachineRepository virtualMachineRepository, IValidationService<DeleteVirtualMachineCommand> validationService, IAuditService auditService)
        {
            this.virtualMachineRepository = virtualMachineRepository;
            this.validationService = validationService;
            this.auditService = auditService;
        }

        public async Task<Response<string>> Handle(DeleteVirtualMachineCommand request, CancellationToken cancellationToken)
        {
            await validationService.ExecuteValidationGuard(request);

            var virtualMachine = await virtualMachineRepository.GetByIdAsync(request.VirtualMachineId);

            virtualMachine.IsActive = false;
            virtualMachine.UpdatedAt = TimeUtil.ObtenerFechaYHoraZonaHorariaBogota();
            virtualMachine.UserModifierId = Guid.Parse(auditService.GetUserId());

            await virtualMachineRepository.SaveChangesAsync();

            return new Response<string>(data: $"Se ha eliminado correctamente la maquina virtual con id {request.VirtualMachineId}.");
        }
    }
}
