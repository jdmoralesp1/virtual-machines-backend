using Microsoft.Extensions.DependencyInjection;
using PruebaTecnica.Aplication.Features.VirtualMachines.V1.Commands;
using PruebaTecnica.Aplication.Features.VirtualMachines.V1.Queries;
using PruebaTecnica.Aplication.Features.VirtualMachines.V1.Validators;
using PruebaTecnica.Application.Exceptions.Interfaces;

namespace PruebaTecnica.Aplication.Features.VirtualMachines.V1
{
    public static class RegisterValidationsAndServicesVirtualMachines
    {
        public static IServiceCollection AddValidationsAndServicesVirtualMachine(this IServiceCollection services)
        {
            services.AddScoped<ICustomValidator<CreateVirtualMachineCommand>, CreateVirtualMachineCommandValidator>();
            services.AddScoped<ICustomValidator<GetAllVirtualMachinesQuery>, GetAllVirtualMachinesPersistenceValidator>();
            services.AddScoped<ICustomValidator<GetVirtualMachineByIdQuery>, GetVirtualMachineByIdQueryValidator>();
            services.AddScoped<ICustomValidator<GetVirtualMachineByIdQuery>, GetVirtualMachineByIdPersistenceValidator>();
            services.AddScoped<ICustomValidator<UpdateVirtualMachineCommand>, UpdateVirtualMachineCommandValidator>();
            services.AddScoped<ICustomValidator<UpdateVirtualMachineCommand>, UpdateVirtualMachinePersistenceValidator>();
            services.AddScoped<ICustomValidator<DeleteVirtualMachineCommand>, DeleteVirtualMachinePersistenceValidator>();

            return services;
        }
    }
}
