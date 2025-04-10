using MediatR;
using PruebaTecnica.Aplication.VirtualMachines.V1.DTOs;
using PruebaTecnica.Application.Exceptions.Interfaces;
using PruebaTecnica.Domain.Enums;
using PruebaTecnica.Domain.Interfaces;
using PruebaTecnica.Domain.Utils;
using PruebaTecnica.Domain.Wrappers;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PruebaTecnica.Aplication.VirtualMachines.V1.Queries.Handlers
{
    public class GetAllVirtualMachinesQueryHandler : IRequestHandler<GetAllVirtualMachinesQuery, Response<List<GetAllVirtualMachinesResponse>>>
    {
        private readonly IVirtualMachineRepository virtualMachineRepository;
        private readonly IValidationService<GetAllVirtualMachinesQuery> validationService;

        public GetAllVirtualMachinesQueryHandler(IVirtualMachineRepository virtualMachineRepository, IValidationService<GetAllVirtualMachinesQuery> validationService)
        {
            this.virtualMachineRepository = virtualMachineRepository;
            this.validationService = validationService;
        }

        public async Task<Response<List<GetAllVirtualMachinesResponse>>> Handle(GetAllVirtualMachinesQuery request, CancellationToken cancellationToken)
        {
            await validationService.ExecuteValidationGuard(request);

            var virtualMachines = await virtualMachineRepository.GetAllAsync();

            return new Response<List<GetAllVirtualMachinesResponse>>
            (
                virtualMachines.Select(x => new GetAllVirtualMachinesResponse
                {
                    Id = x.Id,
                    Cores = x.Cores,
                    RAM = x.RAM,
                    Disc = x.Disc,
                    OperatingSystem = ((OperatingSystems)x.OperatingSystem).ToString(),
                    CreatedAt = TimeUtil.GetDateInFormatYYYYmmddHHmm(x.CreatedAt),
                    UpdatedAt = x.UpdatedAt is null ? null : TimeUtil.GetDateInFormatYYYYmmddHHmm(x.UpdatedAt.Value)
                }).ToList()
            );
        }
    }
}
