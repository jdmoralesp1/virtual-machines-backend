using MediatR;
using PruebaTecnica.Aplication.Features.VirtualMachines.V1.DTOs;
using PruebaTecnica.Aplication.Features.VirtualMachines.V1.Queries;
using PruebaTecnica.Application.Exceptions.Interfaces;
using PruebaTecnica.Domain.Enums;
using PruebaTecnica.Domain.Interfaces;
using PruebaTecnica.Domain.Utils;
using PruebaTecnica.Domain.Wrappers;
using System.Threading;
using System.Threading.Tasks;

namespace PruebaTecnica.Aplication.Features.VirtualMachines.V1.Queries.Handlers
{
    public class GetVirtualMachineByIdQueryHandler : IRequestHandler<GetVirtualMachineByIdQuery, Response<GetVirtualMachineByIdResponse>>
    {
        private readonly IVirtualMachineRepository virtualMachineRepository;
        private readonly IValidationService<GetVirtualMachineByIdQuery> validationService;

        public GetVirtualMachineByIdQueryHandler(IVirtualMachineRepository virtualMachineRepository, IValidationService<GetVirtualMachineByIdQuery> validationService)
        {
            this.virtualMachineRepository = virtualMachineRepository;
            this.validationService = validationService;
        }

        public async Task<Response<GetVirtualMachineByIdResponse>> Handle(GetVirtualMachineByIdQuery request, CancellationToken cancellationToken)
        {
            await validationService.ExecuteValidationGuard(request);

            var virtualMachine = await virtualMachineRepository.GetByIdAsync(request.VirtualMachineId);

            return new Response<GetVirtualMachineByIdResponse>(new GetVirtualMachineByIdResponse
            {
                Id = virtualMachine.Id,
                Cores = virtualMachine.Cores,
                RAM = virtualMachine.RAM,
                Disc = virtualMachine.Disc,
                OperatingSystem = virtualMachine.OperatingSystem.ToString(),
                CreatedAt = TimeUtil.GetDateInFormatYYYYmmddHHmm(virtualMachine.CreatedAt),
                UpdatedAt = virtualMachine.UpdatedAt is null ? null : TimeUtil.GetDateInFormatYYYYmmddHHmm(virtualMachine.UpdatedAt.Value)
            });
        }
    }
}
