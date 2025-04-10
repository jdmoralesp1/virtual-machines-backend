using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PruebaTecnica.Domain.Models;

namespace PruebaTecnica.Data.Context
{
    public class PruebaTecnicaContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        public PruebaTecnicaContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<VirtualMachine> VirtualMachines { get; set; }
    }
}
