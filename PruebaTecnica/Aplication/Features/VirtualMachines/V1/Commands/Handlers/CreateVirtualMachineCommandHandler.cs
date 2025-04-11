using MediatR;
using PruebaTecnica.Aplication.Features.VirtualMachines.V1.Commands;
using PruebaTecnica.Aplication.Features.VirtualMachines.V1.DTOs;
using PruebaTecnica.Application.Exceptions.Interfaces;
using PruebaTecnica.Application.Interfaces;
using PruebaTecnica.Domain.Interfaces;
using PruebaTecnica.Domain.Models;
using PruebaTecnica.Domain.Utils;
using PruebaTecnica.Domain.Wrappers;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PruebaTecnica.Aplication.Features.VirtualMachines.V1.Commands.Handlers
{
    public class CreateVirtualMachineCommandHandler : IRequestHandler<CreateVirtualMachineCommand, Response<CreateVirtualMachineResponse>>
    {
        private readonly IVirtualMachineRepository virtualMachineRepository;
        private readonly IValidationService<CreateVirtualMachineCommand> validationService;
        private readonly IAuditService auditService;

        public CreateVirtualMachineCommandHandler(IVirtualMachineRepository virtualMachineRepository, IValidationService<CreateVirtualMachineCommand> validationService, IAuditService auditService)
        {
            this.virtualMachineRepository = virtualMachineRepository;
            this.validationService = validationService;
            this.auditService = auditService;
        }

        public async Task<Response<CreateVirtualMachineResponse>> Handle(CreateVirtualMachineCommand request, CancellationToken cancellationToken)
        {
            await validationService.ExecuteValidationGuard(request);

            var virtualMachine = await virtualMachineRepository.Add(new VirtualMachine
            {
                Cores = request.Cores,
                RAM = request.RAM,
                Disc = request.Disc,
                OperatingSystem = request.OperatingSystem,
                IsActive = true,
                CreatedAt = TimeUtil.ObtenerFechaYHoraZonaHorariaBogota(),
                UserId = Guid.Parse(auditService.GetUserId()),
            });

            return new Response<CreateVirtualMachineResponse>(new CreateVirtualMachineResponse(virtualMachine.Id, virtualMachine.CreatedAt, virtualMachine.UpdatedAt));
        }
    }
}
